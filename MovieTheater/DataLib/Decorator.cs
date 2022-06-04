/*Patterns 
1.      decorator                    watch her video guide!!! (special lightning, addtional audio system, additional sofa seats)
2.      state (customer state)       (// Should come from a datasource   ???)
3.      abstract factory (bar)       (3 types of bar set)
4.      factory methoud (beverage)    
5.      proxy/observer ???
*/


public abstract class Decorator : MovieHall
{
    protected MovieHall hall;

    public void SetMovieHall(MovieHall hall)
    {
        this.hall = hall;
    }

}