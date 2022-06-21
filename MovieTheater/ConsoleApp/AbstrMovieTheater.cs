using System;
using System.Collections.Generic;

public abstract class AbstrMovieTheater
{
    private Customer user;
    public MovieAssistant assistant;
    public MovieTheaterComponents movieTheaterComponents;
    public string[] allCommands;
    public List<Action> allProcesses;

    public Customer User
    {
        get
        {
            return user;
        }
        set
        {
            user = value;
        }
    }

    protected AbstrMovieTheater(MovieTheaterComponents movieTheaterComponents)
    {
        this.movieTheaterComponents = movieTheaterComponents;
        SetCommandInfo();
    }

    public void SetCommandInfo()
    {
        this.allCommands = new string[] {"block user","add assist","delete assist", "delete movie","add movie", "cancel session", "get all customers",
            "get all movies","show my tickets","update my account", "show my account", "delete my account", "buy ticket", "exit", "log in", "log out",
            "registrate", "show billboard", "add hall"};
        this.allProcesses = new List<Action>{ProcessBlockUser, ProcessAddMovieAssist, ProcessDeleteMovieAssist,
            ProcessDeleteMovie, ProcessAddMovie, ProcessCancelSession, ProcessGetAllCustomers,
            ProcessGetAllMovies, ProcessShowMyTickets, ProcessUpdateMyAccount, ProcessShowMyAccount,
            ProcessDeleteMyAccount, ProcessBuyTicket, ProcessExit, ProcessLogIn, ProcessLogOut,
            ProcessRegistrate, ProcessShowBillboard, ProcessAddHall};
    }

    public void SetMovieAssistant()
    {
        List<Customer> allAssists= this.movieTheaterComponents.userRepository.GetAllByAccessLevel("moderator");
        this.assistant = new MovieAssistant().SetMovieAssistant(allAssists[0]);
    }

    public abstract void ProcessAddHall();
    public abstract void ProcessBlockUser();
    public abstract void ProcessAddMovieAssist();
    public abstract void ProcessDeleteMovieAssist();
    public abstract void ProcessAddMovie();
    public abstract void ProcessDeleteMovie();
    public abstract void ProcessCancelSession();
    public abstract void ProcessGetAllCustomers();
    public abstract void ProcessGetAllMovies();
    public abstract void ProcessShowMyTickets();
    public abstract void ProcessBuyTicket();
    public abstract void ProcessUpdateMyAccount();
    public abstract void ProcessDeleteMyAccount();
    public abstract void ProcessShowMyAccount();
    public abstract void ProcessExit();
    public abstract void ProcessLogIn();
    public abstract void ProcessLogOut();
    public abstract void ProcessRegistrate();
    public abstract void ProcessShowBillboard();
}