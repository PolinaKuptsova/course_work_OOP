using System;
public class BasicCustomerState : CustomerState  
{
    // Constructor
    public BasicCustomerState(Customer customer, StateFeaturesRepository stateFeaturesRepository) : base(customer, stateFeaturesRepository)
    {
        this.Customer = customer;
        this.StateFeaturesRepository = stateFeaturesRepository;
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
        StateFeatures = this.StateFeaturesRepository.GetStateFeatures("basic");  
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