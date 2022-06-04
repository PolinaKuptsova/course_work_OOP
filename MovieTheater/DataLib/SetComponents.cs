public abstract class SetComponents
{
    public string flavor;
    
    protected SetComponents()
    {
    }

    protected SetComponents(string flavor)
    {
        this.flavor = flavor;
    }

    public override string ToString()
    {
        return string.Format($"Chosen flovor is: '{flavor}'");
    }

}