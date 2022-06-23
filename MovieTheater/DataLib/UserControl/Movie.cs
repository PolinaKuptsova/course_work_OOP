using System;
public class Movie
{
    public int id;
    public string title;
    public string genre;
    public string director;
    public double duration;
    public DateTime premiere;
    public DateTime lastDayOnScreen;
    public string description;
    public int ageRange;

    public Movie(string title, string genre, string director, double duration, DateTime premiere, DateTime lastDayOnScreen, string description, int ageRange)
    {
        this.title = title;
        this.genre = genre;
        this.director = director;
        this.duration = duration;
        this.premiere = premiere;
        this.lastDayOnScreen = lastDayOnScreen;
        this.description = description;
        this.ageRange = ageRange;
    }

    public Movie()
    { }

    public override string ToString()
    {
        return string.Format($"#{id} Title: '{title}' Genre: '{genre}' Director: {director}");
    }
}
