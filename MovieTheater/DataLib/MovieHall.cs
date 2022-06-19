using System;
using System.Collections.Generic;
// do decorator
public class MovieHall : Room
{
    public int hall_id;
    public string typeOfScreen;
    public int rowAmount;
    public int placesInRowAmount;
    public string [,] places;

    public MovieHall()
    {
    }

    public MovieHall(string typeOfScreen, int rowAmount, int placesInRowAmount)
    {
        this.typeOfScreen = typeOfScreen;
        this.rowAmount = rowAmount;
        this.placesInRowAmount = placesInRowAmount;
    }

    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        List<Customer> customers = movieTheaterComponents.userRepository.GetAllByAccessLevel("moderator");
        Console.WriteLine("Schedule for {DateTime.Now}");
        int hour = 0;        
        foreach(Customer c in customers){
            if(!c.isBlocked)
            {
                DateTime d = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,hour,0,0);
                Console.WriteLine($"{c.Name} : shifts due to {d.ToString("d")}");
                hour+=2;
            }
        }
    }

    public override void Show_Info()
    {
        Console.WriteLine($"Hall #{hall_id}: Screen: {typeOfScreen}");
    }

    public override string ToString()
    {
        return string.Format($"#{hall_id} {rowAmount} x {placesInRowAmount}");
    }
}