using System;
using System.Collections.Generic;

public class Customer : User, IObserver
{
    public bool deleted;
    public List<Ticket> tickets;
    private CustomerState customerState;
    public CustomerState CustomerState
    {
        get => customerState;
        set => customerState = value;
    }
    public MovieAssistant movieAssist;

    public Customer()
    {

    }
    public Customer(StateFeaturesRepository stateFeaturesRepository)
    {
        this.CustomerState = new BasicCustomerState(this, stateFeaturesRepository);
    }

    public void SetCustomerState(StateFeaturesRepository stateFeaturesRepository)
    {
        this.CustomerState = new BasicCustomerState(this, stateFeaturesRepository);
    }

    public override string ToString()
    {
        return string.Format($"#{id} {Name}\r\nPhone number: {PhoneNumber}\r\nAge: {age}");
    }

    public override void ChooseTicket(MovieTheaterComponents movieTheaterComponents)
    {
        if (this.isBlocked) { throw new Exception("Your account has been blocked!"); }
        string today_str = DateTime.Now.ToString("d");

        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        Movie movie = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if (movie == null) { throw new Exception($"No such film '{title}'!"); }

        // add sessions automatically
        List<Session> sessions = movieTheaterComponents.sessionRepository.GetActualMovieSessions(movie.id);
        if (sessions.Count != 0)
        {
            foreach (Session sn in sessions)
            {
                Console.WriteLine(sn);
            }
        }
        else
        {
            throw new Exception($"No sessions for movie {movie.title}");
        }

        Console.WriteLine("Enter the number of session:");
        string session_id = Console.ReadLine();
        Session session = movieTheaterComponents.sessionRepository.GetById(int.Parse(session_id));

        List<Ticket> tickets = movieTheaterComponents.ticketRepository.GetPlacesForSession(session);
        MovieHall hall = movieTheaterComponents.movieHallRepository.GetById(session.hall_id);
        DisplayPlacesInMovieHall(session, tickets, hall);

        int row, place;
        while (true)
        {
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

        Ticket newTicket = new Ticket
        {
            movieId = movie.id,
            ticketNumber = place.ToString() + row.ToString() + "AK",
            place = place,
            row = row,
            startMovie = session.start,
            hallNumber = hall.hall_id,
            session = session
        };

        newTicket.id = movieTheaterComponents.ticketRepository.Insert(newTicket);

        this.tickets = new List<Ticket>();
        this.tickets = movieTheaterComponents.ticketRepository.GetCustomerTickets(this.id);
        this.tickets.Add(newTicket);
    }

    public double AddSnackToOrder(MovieTheaterComponents movieTheaterComponents)
    {
        BarCustomer bc = new BarCustomer();
        MovieBarSet barSet = null;
        Console.WriteLine("choose a set\r\nWe have:\r\n-small set\r\n-kid set\r\n-medium set\r\n-big set");
        string set = Console.ReadLine();
        if (set == "small set")
        {
            barSet = new SmallMovieBarSet();
            bc = new BarCustomer(barSet);
            bc.MakeOrder(barSet);
            return barSet.Price;
        }
        else if (set == "kid set")
        {
            barSet = new KidMovieBarSet();
            bc = new BarCustomer(barSet);
            bc.MakeOrder(barSet);
            return barSet.Price;
        }
        else if (set == "medium set")
        {
            barSet = new MediumMovieBarSet();
            bc = new BarCustomer(barSet);
            bc.MakeOrder(barSet);
            return barSet.Price;
        }
        else if (set == "big set")
        {
            barSet = new BigMovieBarSet();
            bc = new BarCustomer(barSet);
            bc.MakeOrder(barSet);
            return barSet.Price;
        }
        else
        {
            throw new Exception("No such bar set! try again");
        }
    }

    public void PayForPurchase(double price, MovieTheaterComponents movieTheaterComponents)
    {
        this.CustomerState.PayForPurchase(price);
        movieTheaterComponents.userRepository.UpdateUserBalance(this.id, this.Balance += price);
        Console.WriteLine(" Balance = {0:C}", this.Balance);
        Console.WriteLine(" Status of card = {0}", this.CustomerState.GetType().Name);
    }

    private static string[,] GetPlacesInMovieHall(List<Ticket> tickets, MovieHall hall)
    {
        hall.places = new string[hall.rowAmount, hall.placesInRowAmount];
        int row, place;
        foreach (Ticket t in tickets)
        {
            row = t.row - 1; place = t.place - 1;
            hall.places[row, place] = " +";
        }
        return hall.places;
    }

    private static void DisplayPlacesInMovieHall(Session session,
        List<Ticket> tickets, MovieHall hall)
    {
        hall.places = GetPlacesInMovieHall(tickets, hall);
        Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
        for (int row = 0; row < hall.places.GetLength(0); row++)
        {
            row = row + 1;
            if (row == hall.places.GetLength(0)) { Console.Write(row.ToString()); }
            else { Console.Write(row.ToString() + " "); }
            row = row - 1;
            for (int place = 0; place < hall.places.GetLength(1); place++)
            {
                if (hall.places[row, place] == null) { Console.Write(" -"); }
                else { Console.Write(hall.places[row, place]); }
            }
            Console.WriteLine();
        }
    }

    public override void UpdateMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if (this.isBlocked) { throw new Exception("Your account has been blocked!"); }
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
        if (hashVerification == true && newPasswordComparision == false) { newPasswordCheck = true; }

        Customer updateCustomer = new Customer
        {
            id = this.id,
            Name = newName,
            Password = newPasswordCheck == true ? Authentication.GetHash(newPassword) : throw new Exception("Your new password cannot be the same as an old one!"),
            PhoneNumber = newPhoneNumber,
            isBlocked = this.isBlocked,
            age = int.Parse(newAge),
            accessLevel = this.accessLevel,
            Balance = this.Balance,
            CustomerState = this.CustomerState
        };

        bool updated = movieTheaterComponents.userRepository.UpdateUserAccount(this.id, updateCustomer);
        if (updated)
        {
            this.Name = updateCustomer.Name;
            this.Password = updateCustomer.Password;
            this.PhoneNumber = updateCustomer.PhoneNumber;
            this.age = updateCustomer.age;
            Console.WriteLine("Updated!");
            return;
        }
        throw new Exception("Not updated!");

    }

    public override void DeleteMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if (this.isBlocked) { throw new Exception("Your account has been blocked!"); }
        this.tickets = movieTheaterComponents.ticketRepository.GetCustomerTickets(this.id);
        if (tickets.Count != 0)
        {
            foreach (Ticket ticket in tickets)
            {
                movieTheaterComponents.ticketRepository.DeleteById(ticket.id);
            }
        }
        int result = movieTheaterComponents.userRepository.DeleteById(this.id);
        if (result == 1)
        {
            Console.WriteLine("Succsessfully deleted!");
            this.deleted = true;
            return;
        }
        throw new Exception("Not deleted!");

    }

    public override void ShowMyAccount(MovieTheaterComponents movieTheaterComponents)
    {
        if (this.isBlocked) { throw new Exception("Your account has been blocked!"); }
        Console.WriteLine(this);
    }


    public override void ShowMyTickets(MovieTheaterComponents movieTheaterComponents)
    {
        if (this.isBlocked) { throw new Exception("Your account has been blocked!"); }
        List<Ticket> tickets = movieTheaterComponents.ticketRepository.GetCustomerTickets(this.id);
        if (tickets.Count != 0)
        {
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }
            return;
        }
        throw new Exception("No tickets yet!");
    }

    public override void SubscribeForPremiereNotification(MovieAssistant assistant)
    {
        this.movieAssist = assistant;
        this.movieAssist.NotifyMovieAdding += SendPremiereNotification;
    }

    public void SendPremiereNotification(Movie newMovie)
    {
        Console.WriteLine("New movie is on ours screens! Hurry up to buy a ticket" + newMovie);
    }
}