using System;

// do additional methods ??

// конкретні декоратори
public class SpecialLightingDecorator : Decorator
{
    public string typeOfLightning;

    public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall is for film that require splecial effects and special lightning. Plese take into account its type {typeOfLightning}");
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        base.MakeSchedule(movieTheaterComponents);
        DateTime time1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0,0);
        DateTime time2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0,0);
        Console.WriteLine($"The responsible one should ask for a complete ligthning system observe at : \r\n{time1}\r\n{time2}");
    }
}

public class AdditionalAudioSystemDecorator : Decorator
{
    public string itsPlace;
    public int volumeMin;
    public int volumeMax;

    public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall is for film that require splecial effects and addtional audio components. Plese take into account its volume diapasone: {volumeMin} - {volumeMax}");
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        base.MakeSchedule(movieTheaterComponents);
        DateTime time1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0,0);
        DateTime time2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0,0);
        Console.WriteLine($"The responsible one should ask for a complete audio system observe at : \r\n{time1}\r\n{time2}");
    }
}

public class AdditionalSofaSeatsDecorator : Decorator
{
    public int seats_amount;
    public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall contains special seats. Their amount {seats_amount}");
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        base.MakeSchedule(movieTheaterComponents);
        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0,0);
        Console.WriteLine($"The responsible one should check the sofas quality at: \r\n{time}");
    }

}