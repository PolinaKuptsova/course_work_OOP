public class MovieTheaterComponents
{
    public MovieHallRepository assistantRepository;
    public MovieHallRepository movieHallRepository;
    public UserRepository userRepository;
    public MovieRepository movieRepository;
    public TicketRepository ticketRepository;
    public SessionRepository sessionRepository;
    public TicketPurchaseRepository ticketPurchaseRepository;

    public MovieTheaterComponents()
    { }

    public MovieTheaterComponents(MovieHallRepository assistantRepository, MovieHallRepository movieHallRepository, UserRepository userRepository, MovieRepository movieRepository, TicketRepository ticketRepository, SessionRepository sessionRepository, TicketPurchaseRepository ticketPurchaseRepository)
    {
        this.assistantRepository = assistantRepository;
        this.movieHallRepository = movieHallRepository;
        this.userRepository = userRepository;
        this.movieRepository = movieRepository;
        this.ticketRepository = ticketRepository;
        this.sessionRepository = sessionRepository;
        this.ticketPurchaseRepository = ticketPurchaseRepository;
    }
}