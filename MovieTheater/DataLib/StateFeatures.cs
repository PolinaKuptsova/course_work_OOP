using System;
public class StateFeatures
{
    public long id;
    private string customerTypeTitle;
    private double discount;
    private double upperLimit;
    public DateTime createdAt;

    public StateFeatures()
    {
    }

    public StateFeatures(long id, double discount, double upperLimit, DateTime createdAt)
    {
        this.id = id;
        Discount = discount;
        UpperLimit = upperLimit;
        this.createdAt = createdAt;
    }

    public double Discount { get => discount; set => discount = value; }
    public double UpperLimit { get => upperLimit; set => upperLimit = value; }
    public string CustomerTypeTitle { get => customerTypeTitle; set => customerTypeTitle = value; }
}