public interface IObserver
{
    void SendPremiereNotification(Movie movie);
    void SendCancelSessionNotification(Session session);
}