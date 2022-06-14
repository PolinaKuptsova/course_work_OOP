using System;
using System.Collections.Generic;

public class MovieTheater : AbstrMovieTheater
{
    private MovieTheaterComponents components;

    public MovieTheater(MovieTheaterComponents movieTheaterComponents) : base(movieTheaterComponents)
    {
        ShowInfoForUser();
        SetCommandInfo();
    }

    public override void ProcessAddMovie()
    {
        try
        {
            if (this.user != null)
            {
                if (this.user.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.user);
                    assist.AddMovie(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                if (this.user.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin.AddMovieAssistant(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                if (this.user.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(this.user);
                    admin.BlockUser(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                ProcessShowBillboard();
                this.user.ChooseTicket(this.movieTheaterComponents);
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
            if (this.user != null)
            {
                if (this.user.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.user);
                    assist.CancelSession(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                if (this.user.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.user);
                    assist.DeleteMovie(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                if (this.user.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(this.user);
                    admin.DeleteMovieAssistant(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                this.user.DeleteMyAccount(this.movieTheaterComponents);
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
            if (this.user != null)
            {
                if (this.user.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.user);
                    assist.GetAllCustomers(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user != null)
            {
                if (this.user.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(this.user);
                    assist.GetAllMovies(this.movieTheaterComponents);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {this.user.accessLevel}");
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
            if (this.user == null)
            {
                this.user = MovieTheater.LogIn(this.movieTheaterComponents);

                if (this.user.accessLevel == "moderator")
                {
                    ShowInfoForAssist();
                }
                else if (this.user.accessLevel == "customer")
                {
                    ShowInfoForCustomer();
                }
                else
                {
                    ShowInfoForAdmin();
                }
            }
            throw new Exception("Yo have already loged in!");
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
        throw new Exception($"username {name} or {password} is incorrect.");
    }


    public override void ProcessRegistrate()
    {
        try
        {
            if (this.user == null)
            {
                this.user = MovieTheater.Registrate(this.movieTheaterComponents);
                Console.WriteLine("You have been successfully registrated!\r\nDo you want to stay inform about premier? 'Yes/No'");   
                string subscribe = Console.ReadLine();
                if(subscribe == "Yes"){
                    this.user.SubscribeForPremiereNotification(this.movieTheaterComponents);
                }
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
        string name = "Polina";

        User user = Authentication.ValidateUserName(name, movieTheater.userRepository);
        if (user != null)
        {
            throw new Exception("Such username '{name}' is already exists!");
        }

        Console.Write("\r\nPassword: ");
        string password = "123";

        Console.Write("\r\nPhone number: ");
        string phoneNumber = "1234569";

        Console.Write("\r\nAge: ");
        string ageInput = "19";

        if (!Int32.TryParse(ageInput, out int i))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }
        int age = int.Parse(ageInput);
        if (age < 0 || ageInput.StartsWith('-'))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }

        Customer customer = new Customer();
        customer.Name = name;
        customer.Password = password;
        customer.PhoneNumber = phoneNumber;
        customer.age = age;
        customer.accessLevel = "customer";

        long id = 0 ;
        try {  id = movieTheater.userRepository.Insert(customer); }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

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
            if (this.user != null)
            {
                this.user.ShowMyAccount(this.movieTheaterComponents);
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
            if (this.user != null)
            {
                this.user.ShowMyTickets(this.movieTheaterComponents);
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
            if (this.user != null)
            {
                this.user.UpdateMyAccount(this.movieTheaterComponents);
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
        this.user = null;
    }
    private void ShowInfoForUser()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-exit\r\n-registrate\r\nFor more,please, log in!");
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