using System;
public class Ticket
{
    public long id;
    public long movieId;
    public string ticketNumber;
    public int place;
    public int row;
    public DateTime startMovie;
    public long hallNumber;
    public Movie movie;

    public Ticket(long movieId, string ticketNumber, int place, int row, DateTime startMovie, long hallNumber)
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
        return string.Format($"| #{ticketNumber} Hall #{hallNumber}\r\n| {movie.title} Time: {startMovie}");
    }
}
