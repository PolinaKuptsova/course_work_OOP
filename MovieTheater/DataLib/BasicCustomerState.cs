using System;
public class BasicCustomerState : CustomerState  
{
    // Constructor
    public BasicCustomerState(Customer customer) : base(customer)
    {
        this.Customer = customer;
        Initialize();
    }

    public BasicCustomerState(CustomerState state)
    {
        this.Customer = state.Customer;
        Initialize();
    }

    public BasicCustomerState()
    {
    }

    private void Initialize()
    {
        StateFeatures = stateFeaturesRepository.GetStateFeatures("basic");  
    }

    private void StateChangePrice()
    {
        if (Customer.Balance > StateFeatures.UpperLimit)
        {
            Customer.CustomerState = new VipCustomerState(this);
        }
    }
    public override void PayForPurchase(double price)
    {
        double newPrice = CalcCheck(price);
        this.Customer.Balance += newPrice;
        StateChangePrice();
    }

    private double CalcCheck(double price)
    {
        return (1 - StateFeatures.Discount) * price;
    }
}