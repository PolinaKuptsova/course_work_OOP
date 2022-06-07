/*Patterns 
1.      decorator                    watch her video guide!!! (special lightning, addtional audio system, additional sofa seats)
2.      state (customer state)       (// Should come from a datasource   ???)
3.      abstract factory (bar)       (3 types of bar set)
4.      factory methoud (beverage)    
5.      proxy/observer ???
*/


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
        if(room != null){
            room.MakeSchedule(movieTheaterComponents);
        }
    }

}