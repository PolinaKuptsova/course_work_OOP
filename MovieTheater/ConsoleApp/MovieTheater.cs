using System;
using System.Collections.Generic;

public class MovieTheater : AbstrMovieTheater
{
    public MovieTheater(MovieTheaterComponents movieTheaterComponents) : base(movieTheaterComponents)
    {
        ShowInfoForUser();
        SetCommandInfo();
    }

    public override void ProcessLogOut()
    {
        try
        {
            if (this.User != null)
            {
                this.User = null;
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;
        }
    }

    public override void ProcessAddMovie()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    this.assistant.AddMovie(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;
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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

        }
    }

    public override void ProcessBuyTicket()
    {
        try
        {
            if (this.User != null)
            {
                Console.WriteLine("\r\nBILLBOARD");
                ProcessShowBillboard();
                this.User.ChooseTicket(this.movieTheaterComponents);
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

        }
    }

    public override void ProcessDeleteMyAccount()
    {
        try
        {
            if (this.User != null)
            {
                this.User.DeleteMyAccount(this.movieTheaterComponents);
                if (this.User.deleted)
                {
                    this.User = null;
                }
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;

        }
    }

    public override void ProcessExit()
    {
        Console.WriteLine("Goodbye");
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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

        }
    }

    public override void ProcessLogIn()
    {
        try
        {
            if (this.User == null)
            {
                this.User = MovieTheater.LogIn(this.movieTheaterComponents);

                if (this.User.accessLevel == "admin")
                {
                    ShowInfoForAdmin();
                }
                else if (this.User.accessLevel == "moderator")
                {
                    ShowInfoForAssist();
                }
                else
                {
                    ShowInfoForCustomer();
                }
            }
            else
            {
                throw new Exception("Yo have already loged in!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;

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
        throw new Exception($"username '{name}' or '{password}' is incorrect.");
    }

    public override void ProcessRegistrate()
    {
        try
        {
            if (this.User == null)
            {
                this.User = Registrate(this.movieTheaterComponents);
                Console.WriteLine("You have been successfully registrated!");
                ShowInfoForCustomer();
                return;
            }
            throw new Exception("You have already have an account!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;

        }
    }

    private Customer Registrate(MovieTheaterComponents movieTheater)
    {
        Console.WriteLine("Please, enter info about yourself:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

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

        Console.WriteLine("Do you want to stay inform about premier? 'Yes/No'");
        string subscribe = Console.ReadLine();

        Customer customer = new Customer(movieTheater.stateFeaturesRepository);
        customer.Name = name;
        customer.Password = password;
        customer.PhoneNumber = phoneNumber;
        customer.age = age;
        customer.accessLevel = "customer";
        if (subscribe == "Yes")
        {
            customer.isSubscribed = true;
            Customer subCustomer = customer;
            subCustomer.SubscribeForPremiereNotification(this.assistant);
        }

        long id = 0;
        id = movieTheater.userRepository.Insert(customer);
        if (id != 0) { return movieTheater.userRepository.GetById(id); }
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
            this.errorMessage = ex.Message;

        }

    }
    private static void ShowBillboard(MovieTheaterComponents movieTheaterComponents)
    {
        List<Movie> movies = movieTheaterComponents.movieRepository.GetAllAvalibleMovies();
        if (movies.Count != 0)
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine();
            return;
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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

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
            this.errorMessage = ex.Message;

        }
    }

    private void ShowInfoForUser()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-exit\r\n-registrate\r\nFor more,please, log in!");
    }

    private void ShowInfoForCustomer()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-log out\r\n-exit\r\n");
    }
    private void ShowInfoForAssist()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n-log out\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n-add hall\r\n");
    }
    private void ShowInfoForAdmin()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n-log out\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n-add hall\r\n");
        Console.WriteLine("-block user\r\n-add assist\r\n-delete assist");
    }

    public override void ProcessAddHall()
    {
        try
        {
            if (this.User != null)
            {
                if (this.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.User);
                    assist.AddMovieHall(movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.errorMessage = ex.Message;

        }
    }
}