abstract public class MovieBarSet
{
    private double price;

    protected MovieBarSet()
    {
    }

    public MovieBarSet(double price)
    {
        Price = price;
    }

    public abstract double Price { get ; set; }

    public abstract Beverage AddBeverage();
    public abstract Popcorn AddPopcorn();
    public abstract Glasses AddGlasses();
}
