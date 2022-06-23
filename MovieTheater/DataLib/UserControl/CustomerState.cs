using System;

abstract public class CustomerState
{
    private Customer customer;
    private StateFeatures stateFeatures;
    private StateFeaturesRepository stateFeaturesRepository;

    public CustomerState()
    {
    }

    public CustomerState(Customer customer, StateFeaturesRepository stateFeaturesRepository)
    {
        this.Customer = customer;
        this.StateFeaturesRepository = stateFeaturesRepository;
    }


    public CustomerState(Customer customer, StateFeatures stateFeatures)
    {
        this.Customer = customer;
        this.StateFeatures = stateFeatures;
    }

    // Properties


    public Customer Customer
    {
        get { return customer; }
        set { customer = value; }
    }
    
    public StateFeatures StateFeatures
    {
        get => stateFeatures;
        set => stateFeatures = value;
    }
    public StateFeaturesRepository StateFeaturesRepository
    {
        get => stateFeaturesRepository;
        set => stateFeaturesRepository = value;
    }

    public abstract void PayForPurchase(double price);



}