using System;
public class Beverage : SetComponents
{
    public string type;
    public bool isCold;
    public string[] menu;



    public Beverage()
    {
        this.menu = new string[] { "juice", "mineral water", "coffee", "tea", "nonalcoholic beer" };
    }

    public Beverage(string setQuality, string movieThemes, bool isRecyclablePackage) : base(setQuality, movieThemes, isRecyclablePackage)
    {
    }

    public override string ToString()
    {
        return string.Format($"{type}. cold: {isCold}");
    }
    public void GetBevarageMenu()
    {
        Console.WriteLine("We have: \r\n(1)juice\r\n(2)mineral water\r\n(3)coffee\r\n(4)tea\r\n(5)nonalcoholic beer");
    }

    public Beverage ChooseBevarage()
    {
        Console.WriteLine("Please choose a drink");
        this.GetBevarageMenu();
        string drink = Console.ReadLine();
        foreach (string b in this.menu)
        {
            if (drink == b)
            {
                this.type = drink;
                Console.WriteLine("Do you want it cold: 'Yes/No'");
                string isCold = Console.ReadLine();
                if (isCold == "Yes")
                {
                    this.isCold = true;
                }
                else
                {
                    this.isCold = false;
                }
                return this;
            }
        }
        throw new Exception("No such drink! Try again later.");
    }
}