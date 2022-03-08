using System;
public class Customer : ICustomerBehavior
{
    private string name;
    private string password;
    private string phoneNumber;
    protected bool isBlocked;
    public int age;
    private CustomerState customerState;

    public Customer()
    {
    }

    public Customer(string phoneNumber, int age, string name, string password)
    {
        this.isBlocked = false;
        this.age = age;
        CustomerState = new BasicCustomerState(this , 0);
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }

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
    protected string PhoneNumber
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

    public CustomerState CustomerState { get => customerState; set => customerState = value; }

    public void BuyTicket()
    {
        throw new NotImplementedException();
    }

    public void ShowMyTicket()
    {
        throw new NotImplementedException();
    }

    public void LogOut()
    {
        throw new NotImplementedException();
    }

    public void ShowMyAccount()
    {
        throw new NotImplementedException();
    }

    public void UpdateMyAccount()
    {
        throw new NotImplementedException();
    }

    public void DeleteMyAccount()
    {
        throw new NotImplementedException();
    }

    public void ShowBillboard()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Registrate()
    {
        throw new NotImplementedException();
    }
}
