using System;
using System.Collections.Generic;

public abstract class User
{
    public int id;
    private string name;
    private string password;
    private string phoneNumber;
    public bool isBlocked;
    public bool isSubscribed;
    public int age;
    public string accessLevel;
    private double balance;

    protected User()
    { }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                name = value;
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
            return balance;
        }
        set
        {
            balance += value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                password = value;
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
            return phoneNumber;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                phoneNumber = value;
            }
            else
            {
                throw new Exception("Incorrect input of phone number. Please try again!");
            }
        }
    }

    public abstract void ShowMyTickets(MovieTheaterComponents movieTheaterComponents);
    public abstract void ChooseTicket(MovieTheaterComponents movieTheaterComponents);
    public abstract void UpdateMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void DeleteMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void ShowMyAccount(MovieTheaterComponents movieTheaterComponents);
    public abstract void SubscribeForPremiereNotification(MovieAssistant assist, MovieTheaterComponents movieTheaterComponents);
    public abstract void SubscribeForSessionCancelingNotification(MovieAssistant assist, MovieTheaterComponents movieTheaterComponents);
}