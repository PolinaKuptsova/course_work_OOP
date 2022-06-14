using System;
using System.Collections.Generic;

public abstract class AbstrMovieTheater
{
    public Customer user;
    public MovieTheaterComponents movieTheaterComponents;
    public string[] allCommands;
    public List<Action> allProcesses;


    protected AbstrMovieTheater(MovieTheaterComponents movieTheaterComponents)
    {
        this.movieTheaterComponents = movieTheaterComponents;
        SetCommandInfo();
    }
    public void SetCommandInfo()
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
    public abstract void ResetCommandInfo();
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
    public abstract void ProcessRegistrate();
    public abstract void ProcessShowBillboard();
}