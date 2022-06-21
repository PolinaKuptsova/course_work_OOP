using Npgsql;

namespace course_work_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Host=127.0.0.1;Username=postgres;Password=sofachair2003;Database=movietheatre";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            MovieHallRepository movieHallRepository = new MovieHallRepository(conn);
            UserRepository userRepository = new UserRepository(conn);
            MovieRepository movieRepository = new MovieRepository(conn);
            TicketRepository ticketRepository = new TicketRepository(conn);
            SessionRepository sessionRepository = new SessionRepository(conn);
            TicketPurchaseRepository ticketPurchaseRepository = new TicketPurchaseRepository(conn);
            StateFeaturesRepository stateFeaturesRepository = new StateFeaturesRepository(conn);

            MovieTheaterComponents components = new MovieTheaterComponents(movieHallRepository, userRepository, movieRepository,
            ticketRepository, sessionRepository, ticketPurchaseRepository, stateFeaturesRepository);

            MovieTheater movieTheater = new MovieTheater(components);
            movieTheater.SetMovieAssistant();
            AbstrMovieTheater processControl = new ProcessControl(movieTheater, components);
            ConsoleApp.Run(processControl);

            conn.Close();

        }
    }
}



/*Welcome to the Movie Theater!
Please choose an operation:
-log in
-show billboard
-exit
For more,please, log in!*/