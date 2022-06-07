using System;
using System.Collections.Generic;

public class MovieTheater : AbstrMovieTheater
{
    public override Customer User { 
        get 
        {
            return this.User;
        }
        set 
        {
            this.User = value;
        }
    }

    public MovieTheater(Customer user, MovieTheaterComponents movieTheaterComponents) : base(user, movieTheaterComponents)
    {
        ShowInfoForUser();
        this.User = user;
        this.movieTheaterComponents = movieTheaterComponents;
    }

    public override void SetCommandInfo()
    {
        this.allCommands = new string[] {"block user","add assist","delete assist", "delete movie","add movie", "cancel session", "get all customers",
            "get all movies","show my tickets","update my account", "show my account", "delete my account", "buy ticket", "exit", "log in",
            "registrate", "shw billboard"};
        this.allProcesses = new List<Action>{ProcessBlockUser, ProcessAddMovieAssist, ProcessDeleteMovieAssist,
            ProcessDeleteMovie, ProcessAddMovie, ProcessCancelSession, ProcessGetAllCustomers,
            ProcessGetAllMovies, ProcessShowMyTickets, ProcessUpdateMyAccount, ProcessShowMyAccount,
            ProcessDeleteMyAccount, ProcessBuyTicket, ProcessExit, ProcessLogIn,
            ProcessRegistrate, ProcessShowBillboard};
    }

    public override void ProcessAddMovie()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.AddMovie(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessAddMovieAssist()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin.AddMovieAssistant(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessBlockUser()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(this.User);
                    admin.BlockUser(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessBuyTicket()
    {
        try
        {
            if (this.User != null)
            {
                ProcessShowBillboard();
                this.User.BuyTicket(this.movieTheaterComponents);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessCancelSession()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.CancelSession(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessDeleteMovie()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.DeleteMovie(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessDeleteMovieAssist()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(this.User);
                    admin.DeleteMovieAssistant(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessDeleteMyAccount()
    {
        try
        {
            if (this.User != null)
            {
                this.User.DeleteMyAccount(this.movieTheaterComponents);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessExit()
    {
        try
        {
            ResetCommandInfo();
            Exit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
        private static void Exit()
    {
        Console.WriteLine("Goodbye");
        Environment.Exit(1);
    }

    public override void ProcessGetAllCustomers()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.GetAllCustomers(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessGetAllMovies()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.GetAllMovies(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessLogIn()
    {
        try
        {
            if (this.User == null)
            {
                this.User = MovieTheater.LogIn(this.movieTheaterComponents);

                if (this.User.accessLevel == "moderator")
                {
                    ShowInfoForAssist();
                }
                else if (this.User.accessLevel == "customer")
                {
                    ShowInfoForCustomer();
                }
                else
                {
                    ShowInfoForAdmin();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
        private static Customer LogIn(MovieTheaterComponents movieTheater)
    {
        Console.WriteLine("Enter your name");
        string name = Console.ReadLine();
        Customer user = Authentication.ValidateUserName(name, movieTheater.userRepository);

        Console.WriteLine("Enter your password");
        string password = Console.ReadLine();
        bool hashVerified = Authentication.VerifyHash(password, user.Password);

        if (user != null && hashVerified)
        {
            return user;
        }
        throw new Exception($"Username {name} or {password} is incorrect.");
    }


    public override void ProcessRegistrate()
    {
        try
        {
            if (this.User == null)
            {
                this.User = MovieTheater.Registrate(this.movieTheaterComponents);
                ShowInfoForCustomer();
                return;
            }
            throw new Exception("You have already have an account!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static Customer Registrate(MovieTheaterComponents movieTheater)
    {
        Console.WriteLine("Please, enter info about yourself:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        User user = Authentication.ValidateUserName(name, movieTheater.userRepository);
        if (user != null)
        {
            throw new Exception("Such username '{name}' is already exists!");
        }

        Console.Write("\r\nPassword: ");
        string password = Console.ReadLine();

        Console.Write("\r\nPhone number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("\r\nAge: ");
        string ageInput = Console.ReadLine();

        if (!Int32.TryParse(ageInput, out int i))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }
        int age = int.Parse(ageInput);
        if (age < 0 || ageInput.StartsWith('-'))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }

        Customer customer = new Customer(phoneNumber, age, name, password);
        long id = movieTheater.userRepository.Insert(customer);

        if (id != 0)
        {
            return movieTheater.userRepository.GetById(id);
        }
        throw new Exception("Registration failed!");
    }

    public override void ProcessShowBillboard()
    {
        try
        {
            MovieTheater.ShowBillboard(this.movieTheaterComponents);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    private static void ShowBillboard(MovieTheaterComponents movieTheaterComponents)
    {
        List<Movie> movies = movieTheaterComponents.movieRepository.GetAllAvalibleMovies();
        if (movies != null)
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
        throw new Exception($"Unfortunatelly, there is no movies on avalible now.");
    }
    
    public override void ProcessShowMyAccount()
    {
                try
        {
            if (this.User != null)
            {
                this.User.ShowMyAccount(this.movieTheaterComponents);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessShowMyTickets()
    {

        try
        {
            if (this.User != null)
            {
                this.User.ShowMyTickets(this.movieTheaterComponents);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ProcessUpdateMyAccount()
    {
        try
        {
            if (this.User != null)
            {
                this.User.UpdateMyAccount(this.movieTheaterComponents);
                return;
            }
            throw new Exception("First log in!");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void ResetCommandInfo()
    {
        this.User = null;
    }
    private void ShowInfoForUser()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-exit\r\n-redistrate\r\nFor more,please, log in!");
    }

    private void ShowInfoForCustomer()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
    }
    private void ShowInfoForAssist()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n");
    }
    private void ShowInfoForAdmin()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n");
        Console.WriteLine("-block user\r\n-add assist\r\n-delete assist");
    }

}