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
    public bool[,] places; // availibility в констркторе присврить колличество мест в зале

    public Session()
    {
        this.places = new bool[10, 10];
        // to do 
    }

    public override string ToString()
    {
        return string.Format($"(#{id}) Hall: {hall_id} Start: {start} Avalible: {has_avalibleSeats}");
    }
}

// false - not free
// true - free
