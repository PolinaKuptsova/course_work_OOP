using System;
public class BasicCustomerState : CustomerState  
{
    // Constructor
    public BasicCustomerState(Customer customer, double balance) : base(customer, balance)
    {
        this.balance = balance;
        this.customer = customer;
        Initialize();
    }

    public BasicCustomerState(CustomerState state)
    {
        this.balance = state.Balance;
        this.customer = state.Customer;
        Initialize();
    }

    private void Initialize()
    {
        // Should come from a datasource
        discount = 0.05;
        upperLimit = 5000;
    }

    private void StateChangePrice()
    {
        if (balance > upperLimit)
        {
            customer.CustomerState = new VipCustomerState(this);
        }
    }

    public override void PayForTicket(Ticket ticket)
    {
        double price = CalcPrice(ticket.price);
        balance += price;
        StateChangePrice();
    }

    private double CalcPrice(double price)
    {
        return (1 - discount) * price;
    }
}
