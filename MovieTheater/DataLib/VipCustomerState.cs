using System;
public class VipCustomerState : CustomerState 
{
    public VipCustomerState(CustomerState state)
    {
        this.balance = state.Balance;
        this.customer = state.Customer;
        Initialize();
    }

    private void Initialize()
    {
        // Should come from a datasource   ???
        discount = 0.15;
        upperLimit = 100000;
    }

    public override void PayForTicketPurchase(TicketPurchase ticketPurchase)
    {
        double price = CalcCheck(ticketPurchase.price);        
        balance += price;
    }

    private double CalcCheck(double price)
    {
        return (1 - discount) * price;
    }
}