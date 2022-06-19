using System;
public class StateFeatures
{
    public int id;
    private string customerTypeTitle;
    private double discount;
    private int upperLimit;
    public DateTime createdAt;

    public StateFeatures()
    {
    }

    public double Discount
    {
        get => discount;
        set => discount = value;
    }
    public int UpperLimit
    {
        get => upperLimit;
        set => upperLimit = value;
    }
    public string CustomerTypeTitle
    {
        get => customerTypeTitle;
        set => customerTypeTitle = value;
    }
}