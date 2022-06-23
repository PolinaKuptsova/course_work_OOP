using System;
public class ProcessControl : AbstrMovieTheater
{
    public MovieTheater movieTheater;

    public ProcessControl(MovieTheater movieTheater, MovieTheaterComponents movieTheaterComponents) : base(movieTheaterComponents)
    {
        this.movieTheater = movieTheater;
    }

    public override void ProcessAddMovie()
    {
        movieTheater.ProcessAddMovie();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessAddMovieAssist()
    {
        movieTheater.ProcessAddMovieAssist();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessBlockUser()
    {
        movieTheater.ProcessBlockUser();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessBuyTicket()
    {
        movieTheater.ProcessBuyTicket();
        try
        {
            if (this.User == null) {; throw new Exception("First log in!"); }
            this.errorMessage = movieTheater.errorMessage;
            int position = movieTheater.User.tickets.Count - 1;
            if (position == -1) { position = 0; }
            Ticket ticket = movieTheater.User.tickets[position];
            movieTheater.User.SetCustomerState(movieTheaterComponents.stateFeaturesRepository);

            Console.WriteLine("Please, enter the following info\r\nCard number");
            string cardNumber = Console.ReadLine();
            Console.WriteLine("Expired date 'ex. : 05/22'");
            string expiredDateStr = Console.ReadLine();
            string[] date = expiredDateStr.Split('/');
            DateTime expiredDate = new DateTime(int.Parse(date[1]), int.Parse(date[0]), 1);
            Console.WriteLine("CVV:");
            string cvv = Console.ReadLine();

            BankCard card = new BankCard
            {
                CardNumber = cardNumber,
                ExpiredDate = expiredDate,
                CVV = int.Parse(cvv)
            };

            TicketPurchase ticketPurchase = new TicketPurchase
            {
                ticket_id = ticket.id,
                createdAt = DateTime.Now,
                price = 100,
                customer_id = movieTheater.User.id,
                payment_way = "credit card",
                isCanceled = false,
                session_id = ticket.session.id
            };

            Console.WriteLine("Do you want to add a snack to your order: (Yes/No)");
            string addSnack = Console.ReadLine();
            double barSetprice = 0;

            if (addSnack == "Yes")
            {
                barSetprice = movieTheater.User.AddSnackToOrder(movieTheaterComponents);
            }

            if (BankCardVerification(card, barSetprice + ticketPurchase.price) == 0)
            {
                Console.WriteLine($"We are sorry, but you cannot pay for a ticket! Try another method of payment!");
            }
            else
            {
                long purchase_id = movieTheaterComponents.ticketPurchaseRepository.Insert(ticketPurchase);
                movieTheater.User.PayForPurchase(ticketPurchase.price + barSetprice, movieTheaterComponents);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;
        }
    }

    private int BankCardVerification(BankCard bankCard, double price)
    {
        Random random = new Random();
        int bankResponse = 1;//random.Next(0, 2);

        if (bankResponse == 0)
        {
            Console.WriteLine($"Not enough money on bank card '{bankCard.CardNumber}' to pay {price}.");
            return bankResponse;
        }
        else
        {
            Console.WriteLine($"It is enough money on bank card '{bankCard.CardNumber}' to pay {price}.");
            return bankResponse;
        }
    }

    public override void ProcessCancelSession()
    {
        movieTheater.ProcessCancelSession();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessDeleteMovie()
    {
        movieTheater.ProcessDeleteMovie();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessDeleteMovieAssist()
    {
        movieTheater.ProcessDeleteMovieAssist();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessDeleteMyAccount()
    {
        movieTheater.ProcessDeleteMyAccount();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessExit()
    {
        movieTheater.ProcessExit();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessGetAllCustomers()
    {
        movieTheater.ProcessGetAllCustomers();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessGetAllMovies()
    {
        movieTheater.ProcessGetAllMovies();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessLogIn()
    {
        movieTheater.ProcessLogIn();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessRegistrate()
    {
        movieTheater.ProcessRegistrate();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessShowBillboard()
    {
        movieTheater.ProcessShowBillboard();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessShowMyAccount()
    {
        movieTheater.ProcessShowMyAccount();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessShowMyTickets()
    {
        movieTheater.ProcessShowMyTickets();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessUpdateMyAccount()
    {
        movieTheater.ProcessUpdateMyAccount();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessLogOut()
    {
        movieTheater.ProcessLogOut();
        this.errorMessage = movieTheater.errorMessage;
    }

    public override void ProcessAddHall()
    {
        movieTheater.ProcessAddHall();
        this.errorMessage = movieTheater.errorMessage;
    }
}
