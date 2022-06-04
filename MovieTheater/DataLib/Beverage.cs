abstract public class Beverage : SetComponents
{
    public int amount;

    protected Beverage()
    {
    }

    protected Beverage(string flavor, int amount) : base(flavor)
    {
        this.amount = amount;
    }
}

public class MineralWater : Beverage
{
    public bool carbonated;

    public MineralWater()
    {
    }

    public MineralWater(string flavor, int amount, bool carbonated) : base(flavor, amount)
    {
        this.carbonated = carbonated;
    }
}

public class Juice : Beverage
{
    public string juiceFlavor;

    public Juice(string flavor, int amount, string juiceFlavor) : base(flavor, amount)
    {
        this.juiceFlavor = juiceFlavor;
    }
}

public class Tea : Beverage
{
    public string teaType;

    public Tea(string flavor, int amount, string teaType) : base(flavor, amount)
    {
        this.teaType = teaType;
    }

}
class DefaultBeverage : Beverage
{
    public DefaultBeverage(string flavor, int amount) : base(flavor, amount)
    {
    }

}
    class Creator
    {
        public Beverage ChooseBevarage(string beverage)
        {
            switch(beverage)
            {
                case"juice":
                {
                    // to do
                  //  return new Juice();
                  break;
                }
                case "mineral water":
                {
                    return new MineralWater();
                }
                case "tea":
                {
                 //   return new Tea();
                 break;
                }
                default:
                {
                    return new MineralWater();
                }
            }
                    return new MineralWater();
        }
    }
