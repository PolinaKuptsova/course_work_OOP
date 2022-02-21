
public abstract class Decorator : CinemaHall
{
    protected CinemaHall hall;

    public void SetPet(CinemaHall hall)
    {
        this.hall = hall;
    }
    
    // for example
    
    /*public override void GoToVet()
    {
        if (pet != null)
        {
            pet.GoToVet();
        }
    }*/
}