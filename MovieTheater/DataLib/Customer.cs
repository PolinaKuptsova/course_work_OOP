using System;
using System.Collections.Generic;

public class Customer : User
{
    public Customer(string phoneNumber, int age, string name, string password) : base(phoneNumber, age, name, password)
    {
    }

    public Customer(string name, double balance, string password, string phoneNumber, CustomerState customerState)
    {
        Name = name;
        Balance = balance;
        Password = password;
        PhoneNumber = phoneNumber;
        CustomerState = customerState;
    }

    public Customer()
    {
    }

    public override string Name
    {
        get
        {
            return Name;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                Name = value;
            }
            else
            {
                throw new Exception("Incorrect input of name. Please try again!");
            }
        }
    }

    public override double Balance
    {
        get
        {
            return Balance;
        }
        set
        {
            Balance += value;
        }
    }

    public override string Password
    {
        get
        {
            return Password;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                Password = value;
            }
            else
            {
                throw new Exception("Incorrect input of password. Please try again!");
            }
        }
    }
    public override string PhoneNumber
    {
        get
        {
            return PhoneNumber;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                PhoneNumber = value;
            }
            else
            {
                throw new Exception("Incorrect input of phone number. Please try again!");
            }
        }
    }
    public override CustomerState CustomerState { get => CustomerState; set => CustomerState = value; }

    public override string ToString()
    {
        return string.Format($"Welcome, {Name}\r\nPhone number: {PhoneNumber}\r\nYour state: {CustomerState}\r\nAge: {age}");
    }

    public override void BuyTicket(MovieTheaterComponents movieTheaterComponents)
    {
        if(this.isBlocked){throw new Exception("Your account has been blocked!");}
        string today_str = DateTime.Now.ToString("d");

        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        long movie_id = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if(movie_id == 0){throw new Exception($"No such film '{title}'!");}
        
        Console.WriteLine("Enter the day 'ex. : 05/05/2022' :");
        DateTime chosenDay = DateTime.Parse(Console.ReadLine());
        
        List<Session> sessions = movieTheaterComponents.sessionRepository.GetMovieSessionsOnDay(movie_id, chosenDay);
        foreach (Session sn in sessions)
        {
            Console.WriteLine(sn);
        }
        Console.WriteLine("Enter the number of session:");
        string session_id = Console.ReadLine();

        Session session = movieTheaterComponents.sessionRepository.GetById(int.Parse(session_id));    
        List<Ticket> tickets = movieTheaterComponents.ticketRepository.GetPlacesForSession(session);
        MovieHall hall = movieTheaterComponents.movieHallRepository.GetById(session.hall_id);
        DisplayPlacesInMovieHall(session, tickets, hall);
        
        int row, place;
        while(true){
            Console.WriteLine("\r\nChoose a place:");
            Console.WriteLine("Row:");
            string rowStr = Console.ReadLine();
            Console.WriteLine("Place:");
            string placeStr = Console.ReadLine();
            row = int.Parse(rowStr);
            place = int.Parse(placeStr);

            foreach (Ticket t in tickets)
            {
                if (row == t.row && place == t.place)
                {
                    Console.WriteLine("This place is taken! Choose another");
                    continue;
                }
            }
            break;
        }

        Ticket newTicket = new Ticket{
            movieId = movie_id,
            ticketNumber = place.ToString() + row.ToString() + "AK",
            place = place,
            row = row,
            startMovie = session.start,
            hallNumber = hall.hall_id 
        };
        long ticket_id = movieTheaterComponents.ticketRepository.Insert(newTicket);

        TicketPurchase ticketPurchase = new TicketPurchase{
            ticket_id = ticket_id,
            createdAt = DateTime.Now,
            price = 100,
            customer_id = this.id,
            payment_way = "credit card",
            isCanceled = false
        };
        long purchase_id = movieTheaterComponents.ticketPurchaseRepository.Insert(ticketPurchase);
        this.PayForTicketPurchase(ticketPurchase, movieTheaterComponents);

        Console.WriteLine("Do you want to add a snack to your order: (Yes/No)");
        string addSnack = Console.ReadLine();
        if(addSnack == "Yes")
        {
            AddSnackToOrder(movieTheaterComponents);
        }
    }

    private void AddSnackToOrder(MovieTheaterComponents movieTheaterComponents)
    {
        // TO DO
    }

    private void PayForTicketPurchase(TicketPurchase ticketPurchase, MovieTheaterComponents movieTheaterComponents)
    {
        this.CustomerState.PayForTicketPurchase(ticketPurchase);
        movieTheaterComponents.userRepository.UpdateUserBalance(this.id, this.Balance += ticketPurchase.price);
        Console.WriteLine(" Balance on card = {0:C}", this.Balance);
        Console.WriteLine(" Status of card = {0}", this.CustomerState.GetType().Name);
    }

    private static void DisplayPlacesInMovieHall(Session session, List<Ticket> tickets, MovieHall hall)
    {
        for(int row = 0; row < hall.rowAmount; row++)
        {
            for(int place = 0; place < hall.placesInRowAmount; place++)
            {
                foreach(Ticket t in tickets)
                {
                    if (row == t.row && place == t.place)
                    {
                        Console.Write("+");
                        continue;
                    }
                    Console.Write("-");
                }
            }
            Console.WriteLine();
        }
    }

    //+
    public override void UpdateMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if(this.isBlocked){throw new Exception("Your account has been blocked!");}
        Console.WriteLine("Enter new name :");
        string newName = Console.ReadLine();
        Console.WriteLine("Enter new password :");
        string newPassword = Console.ReadLine();
        Console.WriteLine("Enter old password :");
        string oldPassword = Console.ReadLine();
        Console.WriteLine("Enter new age :");
        string newAge = Console.ReadLine();
        Console.WriteLine("Enter new phone number :");
        string newPhoneNumber = Console.ReadLine();
        
        bool hashVerification = Authentication.VerifyHash(oldPassword, this.Password);
        bool newPasswordComparision = (Authentication.GetHash(oldPassword) == Authentication.GetHash(newPassword)) ? true : false;
        
        bool newPasswordCheck = false;
        if(hashVerification == true && newPasswordComparision == false){newPasswordCheck = true;}
        
        Customer updateCustomer = new Customer{
            id = this.id,
            Name = newName,
            Password = newPasswordCheck == true ? Authentication.GetHash(newPassword) : throw new Exception("Your new password cannot be the same as an old one!"),
            PhoneNumber = newPhoneNumber,
            isBlocked = this.isBlocked,
            age = int.Parse(newAge),
            accessLevel = this.accessLevel,
            Balance = this.Balance,
            CustomerState = this.CustomerState}; 
        
        bool updated = movieTheaterComponents.userRepository.UpdateUserAccount(this.id, updateCustomer);
    }

    //+
    public override void DeleteMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if(this.isBlocked){throw new Exception("Your account has been blocked!");}
        this.tickets = movieTheaterComponents.ticketRepository.GetUserTickets(this.id);
        if (tickets != null)
        {
            foreach (Ticket ticket in tickets)
            {
                movieTheaterComponents.ticketRepository.DeleteById(ticket.id);
            }
        }
        int result = movieTheaterComponents.userRepository.DeleteById(this.id);
    }

    //+
    public override void ShowMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if(this.isBlocked){throw new Exception("Your account has been blocked!");}
        Console.WriteLine(this);
    }

    //+
    public override void ShowMyTickets(MovieTheaterComponents movieTheaterComponents)
    {
        if(this.isBlocked){throw new Exception("Your account has been blocked!");}
        List<Ticket> tickets = movieTheaterComponents.ticketRepository.GetCustomerTickets(this.id);
        if (tickets != null)
        {
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }
        }
        throw new Exception("No tickets yet!");
    }

}