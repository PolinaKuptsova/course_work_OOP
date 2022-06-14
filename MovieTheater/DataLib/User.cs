using System;
using System.Collections.Generic;

public abstract class User 
{
    public long id;
    private string name;
    string password;
    string phoneNumber;
    public bool isBlocked;
    public int age;
    public string accessLevel;
    double balance;
    public List<Ticket> tickets;

    public User()
    {
    }

    public User(string name, string password, string phoneNumber, bool isBlocked, int age, string accessLevel, double balance)
    {
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
        this.isBlocked = isBlocked;
        this.age = age;
        this.accessLevel = accessLevel;
        Balance = balance;
    }

    public abstract void ShowMyTickets(MovieTheaterComponents movieTheaterComponents);
    public abstract void ChooseTicket(MovieTheaterComponents movieTheaterComponents);
    public abstract void UpdateMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void DeleteMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void ShowMyAccount(MovieTheaterComponents movieTheaterComponents);
    public string Name
    {
        get
        {
            return Name;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                Name = value;
            }
            else
            {
                throw new Exception("Incorrect input of name. Please try again!");
            }
        }
    }

    public double Balance
    {
        get
        {
            return Balance;
        }
        set
        {
            Balance += value;
        }
    }

    public string Password
    {
        get
        {
            return Password;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                Password = value;
            }
            else
            {
                throw new Exception("Incorrect input of password. Please try again!");
            }
        }
    }
    public string PhoneNumber
    {
        get
        {
            return PhoneNumber;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                PhoneNumber = value;
            }
            else
            {
                throw new Exception("Incorrect input of phone number. Please try again!");
            }
        }
    }

    public abstract void SubscribeForPremiereNotification(MovieTheaterComponents movieTheaterComponents);
    public abstract void SubscribeForSessionCncelingNotification(MovieTheaterComponents movieTheaterComponents);
}