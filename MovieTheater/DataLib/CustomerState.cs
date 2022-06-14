abstract public class CustomerState
{
    private Customer customer;
    private StateFeatures stateFeatures;
    public StateFeaturesRepository stateFeaturesRepository;

    public CustomerState()
    {
    }

    public CustomerState(Customer customer)
    {
        this.Customer = customer;
    }


    public CustomerState(Customer customer, StateFeatures stateFeatures)
    {
        this.Customer = customer;
        this.StateFeatures = stateFeatures;
    }

    // Properties


    public Customer Customer
    {
        get { return Customer; }
        set { Customer = value; }
    }

    public StateFeatures StateFeatures 
    { get => stateFeatures; set => stateFeatures = value; }

    public abstract void PayForPurchase(double price);

}