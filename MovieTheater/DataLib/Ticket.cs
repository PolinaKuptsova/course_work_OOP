using System;
public class Ticket
{
    public int id;
    public int movieId;
    public string ticketNumber;
    public int place;
    public int row;
    public DateTime startMovie;
    public int hallNumber;
    public Session session;

    public Ticket(int movieId, string ticketNumber, int place, int row, DateTime startMovie, int hallNumber)
    {
        this.movieId = movieId;
        this.ticketNumber = ticketNumber;
        this.place = place;
        this.row = row;
        this.startMovie = startMovie;
        this.hallNumber = hallNumber;
    }

    public Ticket()
    { }

    public override string ToString()
    {
        return string.Format($"#{ticketNumber} Hall #{hallNumber}\r\nTime: {startMovie.ToString("d")}");
    }
}