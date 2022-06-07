public abstract class Room
{
    public int room_number;
    public double size;
    public bool is_Cleaned;
    public bool is_Heated;
    public int floor_number;

    public abstract void Show_Info();
    public abstract void MakeSchedule(MovieTheaterComponents movieTheaterComponents);

}