using System;
using Npgsql;

namespace course_work_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Host=127.0.0.1;Username=postgres;Password=sofachair2003;Database=movietheatre";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            /*MovieHallRepository assistantRepository = new MovieHallRepository(); ;
            MovieHallRepository movieHallRepository;
            UserRepository userRepository = new UserRepository();
            MovieRepository movieRepository = new MovieRepository();
            TicketRepository ticketRepository = new TicketRepository();
            SessionRepository sessionRepository = new SessionRepository();
            TicketPurchaseRepository ticketPurchaseRepository = new TicketPurchaseRepository();

            MovieTheaterComponents components = new MovieTheaterComponents(assistantRepository = new MovieHallRepository(); ;
            movieHallRepository; userRepository, movieRepository, ticketRepository, sessionRepository,
            ticketPurchaseRepository);
            
            MovieTheater movieTheater = new MovieTheater(new Customer(), components);
            ConsoleApp.Run(movieTheater);
            movieTheater.SetCommandInfo(); */
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