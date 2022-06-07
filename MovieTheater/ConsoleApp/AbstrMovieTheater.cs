using System;
using System.Collections.Generic;

public abstract class AbstrMovieTheater
{
    private Customer user;
    public MovieTheaterComponents movieTheaterComponents;
    public string[] allCommands;
    public List<Action> allProcesses;
    public abstract Customer User { get; set; }
    public abstract void SetCommandInfo();
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
    protected AbstrMovieTheater(Customer user, 
        MovieTheaterComponents movieTheaterComponents)
    {
        this.user = user;
        this.movieTheaterComponents = movieTheaterComponents;
    }
}