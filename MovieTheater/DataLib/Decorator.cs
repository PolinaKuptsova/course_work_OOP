public abstract class Decorator : Room
{
    protected Room room;

    public void SetRoom(Room room)
    {
        this.room = room;
    }

    public override void Show_Info()
    {
        if (room != null)
        {
            room.Show_Info();
        }
    }
    public override void MakeSchedule(MovieTheaterComponents movieTheaterComponents)
    {
        if (room != null)
        {
            room.MakeSchedule(movieTheaterComponents);
        }
    }

}