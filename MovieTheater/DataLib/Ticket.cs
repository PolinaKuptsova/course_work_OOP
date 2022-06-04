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

    public override string ToString()
    {
        return string.Format($"| #{ticketNumber} Hall #{hallNumber}\r\n| {movie.title} Time: {startMovie}");
    }
}
