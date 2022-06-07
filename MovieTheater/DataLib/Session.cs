using System;
public class Session
{
    public long id; 
    public long movie_id;
    public long hall_id;
    public DateTime start;
    public DateTime end;
    public bool has_avalibleSeats;
    public bool is_canceled;

    public Session(long movie_id, long hall_id, DateTime start, DateTime end, bool has_avalibleSeats, bool is_canceled)
    {
        this.movie_id = movie_id;
        this.hall_id = hall_id;
        this.start = start;
        this.end = end;
        this.has_avalibleSeats = has_avalibleSeats;
        this.is_canceled = is_canceled;
    }

    public Session()
    { }

    public override string ToString()
    {
        return string.Format($"(#{id}) Hall: {hall_id} Start: {start} Avalible: {has_avalibleSeats}");
    }
}

// false - not free
// true - free
