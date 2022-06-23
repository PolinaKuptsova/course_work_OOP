using System;
public class Popcorn : SetComponents
{
    public string flavor;
    public bool extraCheese;
    public string[] popcornMenu;

    public Popcorn()
    {
        this.popcornMenu = new string[] { "cheese popcorn", "beacon popcorn", "caramel popcorn" };
    }

    public Popcorn(string setQuality, string movieThemes, bool isRecyclablePackage) : base(setQuality, movieThemes, isRecyclablePackage)
    {
    }

    public override string ToString()
    {
        return string.Format($"{flavor} with extra Cheese: {extraCheese}");
    }

    public void GetPopcornMenu()
    {
        Console.WriteLine("We have: \r\n(1)cheese popcorn\r\n(2)beacon popcorn\r\n(3)caramel popcorn");
    }

    public Popcorn ChoosePopcorn()
    {
        Console.WriteLine("Please, choose popcorn. ");
        this.GetPopcornMenu();
        string popcornStr = Console.ReadLine();

        foreach (string p in this.popcornMenu)
        {
            if (popcornStr == p)
            {
                this.flavor = popcornStr;
                Console.WriteLine("Do you want extra cheese: 'Yes/No'");
                string extraCheese = Console.ReadLine();
                if (extraCheese == "Yes")
                {
                    this.extraCheese = true;
                }
                else
                {
                    this.extraCheese = false;
                }
                return this;
            }
        }
        throw new Exception("No such popcorn! Try again later.");
    }
}