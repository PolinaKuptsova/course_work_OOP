using System;
public class Session
{
    public int id;
    public int movie_id;
    public int hall_id;
    public DateTime start;
    public bool has_avalibleSeats;
    public bool is_canceled;

    public Session(int movie_id, int hall_id, DateTime start, bool has_avalibleSeats, bool is_canceled)
    {
        this.movie_id = movie_id;
        this.hall_id = hall_id;
        this.start = start;
        this.has_avalibleSeats = has_avalibleSeats;
        this.is_canceled = is_canceled;
    }

    public Session()
    { }

    public override string ToString()
    {
        return string.Format($"(#{id}) Hall: {hall_id} Start: {start.ToString("d")} Avalible: {has_avalibleSeats}");
    }
}

// false - not free
// true - free