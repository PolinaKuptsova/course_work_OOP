using System;
public class MediumMovieBarSet : MovieBarSet
{
    public override double Price
    {
        get
        {
            return 140;
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
        b.isRecyclablePackage = true;
        b.movieThemes = "Disnay";
        b.setQuality = "medium";
        b.ChooseBevarage();
        return b;
    }

    public override Popcorn AddPopcorn()
    {
        Popcorn popcorn = new Popcorn();
        popcorn.isRecyclablePackage = true;
        popcorn.movieThemes = "Disnay";
        popcorn.setQuality = "medium";

        popcorn.ChoosePopcorn();
        return popcorn;
    }

    public override Glasses AddGlasses()
    {
        Glasses glasses = new Glasses();
        glasses.isRecyclablePackage = true;
        glasses.movieThemes = "Disnay";
        glasses.setQuality = "medium";
        glasses = glasses.ChooseGlasses(glasses);
        return glasses;
    }
}