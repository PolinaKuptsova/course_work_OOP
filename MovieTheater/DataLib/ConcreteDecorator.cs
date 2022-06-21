using System;

// do additional methods ??

// конкретні декоратори
public class SpecialLightingDecorator : Decorator
{
    private string typeOfLightning;

    public SpecialLightingDecorator()
    {
    }

    public SpecialLightingDecorator(string typeOfLightning)
    {
        TypeOfLightning = typeOfLightning;
    }

    public string TypeOfLightning
    {
        get
        {
            return typeOfLightning;
        }
        set
        {
            if (!string.IsNullOrEmpty(value)) {typeOfLightning = value; }
        }
    }
    public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall is for film that require splecial effects and special lightning. Plese take into account its type {TypeOfLightning}");
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
    public string audioType;

    public AdditionalAudioSystemDecorator(string audioType)
    {
        AudioType = audioType;
    }

    public string AudioType
    {
        get
        {
            return audioType;
        }
        set
        {
            if (!string.IsNullOrEmpty(value)) {audioType = value; }
        }
    }
        public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall is for film that require splecial effects and addtional audio components. Tupe: {audioType}");
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        base.MakeSchedule(movieTheaterComponents);
        DateTime time1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0,0);
        DateTime time2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0,0);
        Console.WriteLine($"The responsible one should ask for a complete audio system observe at : \r\n{time1}\r\n{time2}");
    }
}

public class AdditionalSeatsDecorator : Decorator
{
    public int seats_amount;
    public AdditionalSeatsDecorator(int seats_amount)
    {
        Seats_amount = seats_amount;
    }

    public int Seats_amount
    {
        get
        {
            return seats_amount;
        }
        set
        {
            seats_amount = value; 
        }
    }
    public override void Show_Info()
    {
        base.Show_Info();
        Console.WriteLine($"This hall contains special seats. Their amount {seats_amount}");
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        base.MakeSchedule(movieTheaterComponents);
        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0,0);
        Console.WriteLine($"The responsible one should check the seats quality at: \r\n{time}");
    }

}