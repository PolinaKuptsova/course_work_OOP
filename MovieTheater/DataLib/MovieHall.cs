using System;
using System.Collections.Generic;
// do decorator
public class MovieHall : Room
{
    public int hall_id;
    private string typeOfScreen;
    public int rowAmount;
    public int placesInRowAmount;
    public string lightningSystem;
    public string audioSystem;
    public int amountOfAdditionalSeats;
    public string[,] places;

    public string TypeOfScreen
    {
        get
        {
            return typeOfScreen;
        }
        set
        {
            if (!string.IsNullOrEmpty(value)) { typeOfScreen = value; }
        }
    }

    public MovieHall()
    {
        this.lightningSystem = "basic system";
        this.audioSystem = "basic system";
        this.amountOfAdditionalSeats = 0;
    }

    public MovieHall(string typeOfScreen, int rowAmount, int placesInRowAmount)
    {
        this.TypeOfScreen = typeOfScreen;
        this.rowAmount = rowAmount;
        this.placesInRowAmount = placesInRowAmount;
        this.lightningSystem = "basic system";
        this.audioSystem = "basic system";
        this.amountOfAdditionalSeats = 0;
    }

    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        List<Customer> customers = movieTheaterComponents.userRepository.GetAllByAccessLevel("moderator");
        Console.WriteLine("Schedule for {DateTime.Now}");
        int hour = 0;
        foreach (Customer c in customers)
        {
            if (!c.isBlocked)
            {
                DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
                Console.WriteLine($"{c.Name} : shifts due to {d.ToString("d")}");
                hour += 2;
            }
        }
    }

    public override void Show_Info()
    {
        Console.WriteLine($"Hall #{hall_id}: Screen: {TypeOfScreen}");
    }

    public override string ToString()
    {
        return string.Format($"#{hall_id} {rowAmount} x {placesInRowAmount}");
    }
}