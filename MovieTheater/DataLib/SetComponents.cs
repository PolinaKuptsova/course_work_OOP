public abstract class SetComponents
{  
    public string setQuality;
    public string movieThemes;
    public bool isRecyclablePackage;
    protected SetComponents()
    {
    }

    protected SetComponents(string setQuality, string movieThemes, bool isRecyclablePackage)
    {
        this.setQuality = setQuality;
        this.movieThemes = movieThemes;
        this.isRecyclablePackage = isRecyclablePackage;
    }

    public override string ToString()
    {
        return string.Format($" Movie themes: {movieThemes} Recyclable: '{isRecyclablePackage}' ");
    }
    // abstract public void ShowInfo();

}