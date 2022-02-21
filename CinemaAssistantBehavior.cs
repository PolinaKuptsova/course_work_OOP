public interface ICinemaAssistantBehavior : ICustomerBehavior
{
    void AddCinema();
    void DeleteCinema();
    void GetAllCustomers();
    void GetAllCinemas();
    void EscortCustomer(); // for bridge
}