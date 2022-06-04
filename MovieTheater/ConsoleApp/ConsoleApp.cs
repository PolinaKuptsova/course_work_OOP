using System;
using System.Collections.Generic;

/* all commands :
user -      ShowMyTickets
            BuyTicket
            UpdateMyAccount
            DeleteMyAccount
            ShowMyAccount
            Exit

assist -    AddMovie
            DeleteMovie
            CancelSession
            GetAllCustomers
            GetAllMovies

admin -     BlockUser
            AddCinemaAssistant // that exist change their accessLevel
            DeleteCinemaAssistant            
*/

public class CommandInfo
{
    private Customer user;
    public string[] allCommands;
    public List<Action<MovieTheater, CommandInfo>> allProcesses;

    public Customer User { get => user; set => user = value; }

    public CommandInfo()
    {
        this.User = null;
        allCommands = new string[] {"block user","add assist","delete assist", "delete movie","add movie", "cancel session", "get all customers",
            "get all movies","show my tickets","update my account", "show my account", "delete my account", "buy ticket", "exit", "log in",
            "registrate", "shw billboard"};
        allProcesses = new List<Action<MovieTheater, CommandInfo>>{ConsoleApp.ProcessBlockUser, ConsoleApp.ProcessAddMovieAssist, ConsoleApp.ProcessDeleteMovieAssist,
            ConsoleApp.ProcessDeleteMovie, ConsoleApp.ProcessAddMovie, ConsoleApp.ProcessCancelSession, ConsoleApp.ProcessGetAllCustomers,
            ConsoleApp.ProcessGetAllMovies, ConsoleApp.ProcessShowMyTickets, ConsoleApp.ProcessUpdateMyAccount, ConsoleApp.ProcessShowMyAccount,
            ConsoleApp.ProcessDeleteMyAccount, ConsoleApp.ProcessBuyTicket, ConsoleApp.ProcessExit,ConsoleApp.ProcessLogIn,
            ConsoleApp.ProcessRegistrate, ConsoleApp.ProcessShowBillboard};

    }

    public void ResetCommandInfo()
    {
        this.User = null;
    }
}

public static class ConsoleApp
{
    public static void ProcessBlockUser(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(commandInfo.User);
                    admin.BlockUser(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessAddMovieAssist(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin.AddMovieAssistant(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessDeleteMovieAssist(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel == "admin")
                {
                    Admin admin = new Admin();
                    admin = admin.SetAdmin(commandInfo.User);
                    admin.DeleteMovieAssistant(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessAddMovie(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(commandInfo.User);
                    assist.AddMovie(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessDeleteMovie(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(commandInfo.User);
                    assist.DeleteMovie(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessCancelSession(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(commandInfo.User);
                    assist.CancelSession(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessGetAllCustomers(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(commandInfo.User);
                    assist.GetAllCustomers(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessGetAllMovies(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                if (commandInfo.User.accessLevel != "customer")
                {
                    MovieAssistant assist = new MovieAssistant();
                    assist.SetMovieAssistant(commandInfo.User);
                    assist.GetAllMovies(movieTheater);
                    return;
                }
                throw new Exception($"Your acces level is not suitable, you are a {commandInfo.User.accessLevel}");
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessShowMyTickets(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                commandInfo.User.ShowMyTickets(movieTheater);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessBuyTicket(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                ConsoleApp.ProcessShowBillboard(movieTheater, commandInfo);
                commandInfo.User.BuyTicket(movieTheater);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessUpdateMyAccount(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                commandInfo.User.UpdateMyAccount(movieTheater);
                return;
            }
            throw new Exception("First log in!");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessDeleteMyAccount(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                commandInfo.User.DeleteMyAccount(movieTheater);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ProcessShowMyAccount(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User != null)
            {
                commandInfo.User.ShowMyAccount(movieTheater);
                return;
            }
            throw new Exception("First log in!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void ProcessExit(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            commandInfo.ResetCommandInfo();
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

    public static void ProcessLogIn(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User == null)
            {
                commandInfo.User = ConsoleApp.LogIn(movieTheater);

                if (commandInfo.User.accessLevel == "moderator")
                {
                    ConsoleApp.ShowInfoForAssist();
                }
                else if (commandInfo.User.accessLevel == "customer")
                {
                    ConsoleApp.ShowInfoForCustomer();
                }
                else
                {
                    ConsoleApp.ShowInfoForAdmin();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static Customer LogIn(MovieTheater movieTheater)
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


    public static void ProcessRegistrate(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            if (commandInfo.User == null)
            {
                commandInfo.User = ConsoleApp.Registrate(movieTheater);
                ConsoleApp.ShowInfoForCustomer();
                return;
            }
            throw new Exception("You have already have an account!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static Customer Registrate(MovieTheater movieTheater)
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

    public static void ProcessShowBillboard(MovieTheater movieTheater,
        CommandInfo commandInfo)
    {
        try
        {
            ConsoleApp.ShowBillboard(movieTheater);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private static void ShowBillboard(MovieTheater movieTheater)
    {
        List<Movie> movies = movieTheater.movieRepository.GetAllAvalibleMovies();
        if (movies != null)
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
        throw new Exception($"Unfortunatelly, there is no movies on avalible now.");
    }

    public static void Run(MovieTheater movieTheater)
    {
        ShowInfoForUser();
        CommandInfo commandInfo = new CommandInfo();

        while (true)
        {
            Console.WriteLine("Enter command: ");
            string command = Console.ReadLine();
            for (int i = 0; i < commandInfo.allCommands.Length; i++)
            {
                if (command == commandInfo.allCommands[i])
                {
                    commandInfo.allProcesses[i].Invoke(movieTheater, commandInfo);
                }
                else{
                    Console.WriteLine("Incorrect command. Try again!");
                }
            }
        }
    }

    private static void ShowInfoForUser()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-exit\r\n-redistrate\r\nFor more,please, log in!");
    }

    private static void ShowInfoForCustomer()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
    }
    private static void ShowInfoForAssist()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n");
    }
    private static void ShowInfoForAdmin()
    {
        Console.WriteLine("Now you can choose an operation: \r\n-show my tickets\r\n-buy ticket\r\n-show my account\r\n-update my account\r\n-delete my account\r\n-exit\r\n");
        Console.WriteLine("-add movie\r\n-delete movie\r\n-cancel session\r\n-get all customers\r\n-get all movies\r\n");
        Console.WriteLine("-block user\r\n-add assist\r\n-delete assist");
    }
}