abstract public class Beverage : SetComponents
{
    public int amount;
    public bool isRecyclablePackage;

    protected Beverage()
    {
    }

}

public class MineralWater : Beverage
{
    public bool carbonated;

    public MineralWater()
    {
    }

}

public class Juice : Beverage
{
    public string juiceFlavor;

}

public class Tea : Beverage
{
    public string teaType;
}

public class Coffee : Beverage
{
    public string coffeeType;
}

class DefaultBeverage : Beverage
{

}
    class Creator
    {
        
    }
