using System;
public class PayControl : AbstrMovieTheater
{
    public MovieTheater movieTheater;

    public PayControl(MovieTheater movieTheater, Customer user, MovieTheaterComponents movieTheaterComponents) : base(user, movieTheaterComponents)
    {
        this.movieTheater = movieTheater;
    }

    public override Customer User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
        movieTheater.ProcessBuyTicket(); // check if a user could pay // get response from bank (imitate) //take bank info ??
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

    public override void SetCommandInfo()
    {
        movieTheater.SetCommandInfo();
    }
}
