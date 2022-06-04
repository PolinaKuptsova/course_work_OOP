abstract public class CustomerState
{
    protected Customer customer;
    protected double balance;
    protected double discount;
    protected double upperLimit;

    protected CustomerState()
    {
    }

    public CustomerState(Customer customer)
    {
        this.balance = customer.Balance;
        this.Customer = customer;
    }
    protected CustomerState(Customer customer, double discount, double upperLimit)
    {
        this.Customer = customer;
        this.Balance = customer.Balance;
        this.discount = discount;
        this.upperLimit = upperLimit;
    }

    // Properties

    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    public Customer Customer
    {
        get { return Customer; }
        set { Customer = value; }
    }

    public abstract void PayForTicketPurchase(TicketPurchase ticketPurchase);

}