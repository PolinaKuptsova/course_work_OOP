public abstract class Room
{
    public int room_number;
    public double size;
    public bool is_Cleaned;
    public bool is_Heated;
    public int floor_number;

    protected Room()
    {
    }

    protected Room(int room_number, double size, bool is_Cleaned, bool is_Heated, int floor_number)
    {
        this.room_number = room_number;
        this.size = size;
        this.is_Cleaned = is_Cleaned;
        this.is_Heated = is_Heated;
        this.floor_number = floor_number;
    }

    public abstract void Show_Info();
    public abstract void MakeSchedule(MovieTheaterComponents movieTheaterComponents);

}