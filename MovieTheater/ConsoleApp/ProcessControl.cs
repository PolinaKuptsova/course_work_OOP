using System;
public class ProcessControl : AbstrMovieTheater
{
    // place -> row ??? 
    public MovieTheater movieTheater;

    public ProcessControl(MovieTheater movieTheater, MovieTheaterComponents movieTheaterComponents) : base(movieTheaterComponents)
    {
        this.movieTheater = movieTheater;
    }

    public override void ProcessAddMovie()
    {
        movieTheater.ProcessAddMovie();
    }

    public override void ProcessAddMovieAssist()
    {
        movieTheater.ProcessAddMovieAssist();
    }

    public override void ProcessBlockUser()
    {
        movieTheater.ProcessBlockUser();
    }

    public override void ProcessBuyTicket()
    {
        movieTheater.ProcessBuyTicket();
        try
        {
            int position = movieTheater.User.tickets.Count - 1;
            if(position == -1){position = 0;}
            long ticket_id = movieTheater.User.tickets[position].id;

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
                ticket_id = ticket_id,
                createdAt = DateTime.Now,
                price = 100,
                customer_id = movieTheater.User.id,
                payment_way = "credit card",
                isCanceled = false
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
                movieTheater.User.SubscribeForSessionCncelingNotification(movieTheaterComponents);

                movieTheater.User.PayForPurchase(ticketPurchase.price + barSetprice, movieTheaterComponents);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private int BankCardVerification(BankCard bankCard, double price)
    {
        Random random = new Random();
        int bankResponse = random.Next(0, 1);

        if (bankResponse == 0)
        {
            Console.WriteLine($"Not enough money on : bank card '{bankCard.CardNumber}' to pay {price}");
            return bankResponse;
        }
        else
        {
            Console.WriteLine($"It is enough money on : bank card '{bankCard.CardNumber}' to pay {price}");
            return bankResponse;
        }
    }

    public override void ProcessCancelSession()
    {
        movieTheater.ProcessCancelSession();
    }

    public override void ProcessDeleteMovie()
    {
        movieTheater.ProcessDeleteMovie();
    }

    public override void ProcessDeleteMovieAssist()
    {
        movieTheater.ProcessDeleteMovieAssist();
    }

    public override void ProcessDeleteMyAccount()
    {
        movieTheater.ProcessDeleteMyAccount();
    }

    public override void ProcessExit()
    {
        movieTheater.ProcessExit();
    }

    public override void ProcessGetAllCustomers()
    {
        movieTheater.ProcessGetAllCustomers();
    }

    public override void ProcessGetAllMovies()
    {
        movieTheater.ProcessGetAllMovies();
    }

    public override void ProcessLogIn()
    {
        movieTheater.ProcessLogIn();
    }

    public override void ProcessRegistrate()
    {
        movieTheater.ProcessRegistrate();
    }

    public override void ProcessShowBillboard()
    {
        movieTheater.ProcessShowBillboard();
    }

    public override void ProcessShowMyAccount()
    {
        movieTheater.ProcessShowMyAccount();
    }

    public override void ProcessShowMyTickets()
    {
        movieTheater.ProcessShowMyTickets();
    }

    public override void ProcessUpdateMyAccount()
    {
        movieTheater.ProcessUpdateMyAccount();
    }

    public override void ResetCommandInfo()
    {
        movieTheater.ResetCommandInfo();
    }
}