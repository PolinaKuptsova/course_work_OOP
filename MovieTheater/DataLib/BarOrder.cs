using System;
public class BarCustomer : Customer
{
    private Popcorn popcorn;
    private Beverage beverage;
    private Glasses glasses;

    public BarCustomer()
    {
    }

    public BarCustomer(MovieBarSet movieBarSet)
    {
        popcorn = movieBarSet.AddPopcorn();
        beverage = movieBarSet.AddBeverage();
        glasses = movieBarSet.AddGlasses();
    }

    public void MakeOrder(MovieBarSet barSet)
    {
        Console.WriteLine($"You order is:\r\n {popcorn}\r\nDrink{beverage}\r\nGlasses{glasses}");
    }

}
