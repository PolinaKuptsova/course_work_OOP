abstract public class CustomerState
{
    protected Customer customer;
    protected double balance;
    protected double discount;
    protected double upperLimit;

    protected CustomerState()
    {
    }

    public CustomerState(Customer customer, double balance)
    {
        this.balance = balance;
        this.customer = customer;
    }
    protected CustomerState(Customer customer, double balance, double discount, double upperLimit)
    {
        this.Customer = customer;
        this.Balance = balance;
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
        get { return customer; }
        set { customer = value; }
    }

    public abstract void PayForTicket(Ticket ticket);

}