using System;
using System.Collections.Generic;

public abstract class User 
{
    public long id;
    private string name;
    private string password;
    private string phoneNumber;
    public bool isBlocked;
    public int age;
    public string accessLevel;
    private double balance; 
    private CustomerState customerState; // to bd ???
    public List<Ticket> tickets; 

    public User()
    {
    }

    public User(string phoneNumber, int age, string name, string password)
    {
        this.isBlocked = false;
        this.balance = 0.0;
        this.age = age;
        //CustomerState = new BasicUserState(this , 0);
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }
    public abstract void ShowMyTickets(MovieTheaterComponents movieTheaterComponents);
    public abstract void BuyTicket(MovieTheaterComponents movieTheaterComponents);
    public abstract void UpdateMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void DeleteMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void ShowMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract string Name { get; set;}
    public abstract string PhoneNumber { get; set; }
    public abstract double Balance { get; set; }
    public abstract string Password { get; set; }
    public abstract CustomerState CustomerState { get; set; }


}