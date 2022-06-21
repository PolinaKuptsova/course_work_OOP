using System;

public class SmallMovieBarSet : MovieBarSet
{
    public override double Price
    {
        get
        {
            return 100;
        }
        set
        {
            if (value > 0)
            {
                Price = value;
            }
        }
    }
    public override Beverage AddBeverage()
    {
        Beverage b = new Beverage();
        b.isRecyclablePackage = false;
        b.movieThemes = "Marvel";
        b.setQuality = "low";
        b.ChooseBevarage();
        return b;

    }

    public override Popcorn AddPopcorn()
    {
        Popcorn popcorn = new Popcorn();
        popcorn.isRecyclablePackage = false;
        popcorn.movieThemes = "Marvel";
        popcorn.setQuality = "low";
        popcorn.ChoosePopcorn();
        return popcorn;
    }

    public override Glasses AddGlasses()
    {
        Glasses glasses = new Glasses();
        glasses.isRecyclablePackage = false;
        glasses.movieThemes = "Marvel";
        glasses.setQuality = "low";
        glasses = glasses.ChooseGlasses(glasses);
        return glasses;
    }
}
