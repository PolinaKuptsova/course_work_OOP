using System;
public class BasicUserState : CustomerState  
{
    // Constructor
    public BasicUserState(Customer customer) : base(customer)
    {
        this.balance = customer.Balance;
        this.customer = customer;
        Initialize();
    }

    public BasicUserState(CustomerState state)
    {
        this.balance = state.Balance;
        this.customer = state.Customer;
        Initialize();
    }

    private void Initialize()
    {
        // Should come from a datasource ???
        discount = 0.05;
        upperLimit = 10000;
    }

    private void StateChangePrice()
    {
        if (balance > upperLimit)
        {
            customer.CustomerState = new VipCustomerState(this);
        }
    }

    public override void PayForTicketPurchase(TicketPurchase ticketPurchase)
    {
        double price = CalcPrice(ticketPurchase.price);
        balance += price;
        StateChangePrice();
    }

    private double CalcPrice(double price)
    {
        return (1 - discount) * price;
    }
}