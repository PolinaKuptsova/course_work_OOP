using System;
public class VipCustomerState : CustomerState 
{
    public VipCustomerState(CustomerState state)
    {
        this.Customer = state.Customer;
        Initialize();
    }

    private void Initialize()
    {
        StateFeatures = stateFeaturesRepository.GetStateFeatures("vip");  
    }

    public override void PayForPurchase(double price)
    {
        double newPrice = CalcCheck(price);        
        this.Customer.Balance += newPrice;
    }

    private double CalcCheck(double price)
    {
        return (1 - StateFeatures.Discount) * price;
    }
}