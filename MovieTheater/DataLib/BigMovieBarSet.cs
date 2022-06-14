public class BigMovieBarSet : MovieBarSet
{
    public override double Price
    {
        get
        {
            return 200;
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
        b.movieThemes = "60th movie";
        b.setQuality = "high";
        b.ChooseBevarage();
        return b;
    }

    public override Popcorn AddPopcorn()
    {
        Popcorn popcorn = new Popcorn();
        popcorn.isRecyclablePackage = true;
        popcorn.movieThemes = "60th movie";
        popcorn.setQuality = "high";
        popcorn.ChoosePopcorn();
        return popcorn;
    }

    public override Glasses AddGlasses()
    {
        Glasses glasses = new Glasses();
        glasses.isRecyclablePackage = true;
        glasses.movieThemes = "60th movie";
        glasses.setQuality = "high";
        glasses = glasses.ChooseGlasses(glasses);
        return glasses;
    }
}

