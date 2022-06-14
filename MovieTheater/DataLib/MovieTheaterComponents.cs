public class MovieTheaterComponents
{
    public MovieHallRepository movieHallRepository;
    public UserRepository userRepository;
    public MovieRepository movieRepository;
    public TicketRepository ticketRepository;
    public SessionRepository sessionRepository;
    public TicketPurchaseRepository ticketPurchaseRepository;
    public StateFeaturesRepository stateFeaturesRepository;

    public MovieTheaterComponents()
    { }

    public MovieTheaterComponents( MovieHallRepository movieHallRepository, 
        UserRepository userRepository, 
        MovieRepository movieRepository, TicketRepository ticketRepository, 
        SessionRepository sessionRepository, TicketPurchaseRepository ticketPurchaseRepository,
        StateFeaturesRepository stateFeaturesRepository)
    {
        this.movieHallRepository = movieHallRepository;
        this.userRepository = userRepository;
        this.movieRepository = movieRepository;
        this.ticketRepository = ticketRepository;
        this.sessionRepository = sessionRepository;
        this.ticketPurchaseRepository = ticketPurchaseRepository;
        this.stateFeaturesRepository = stateFeaturesRepository;
    }
}