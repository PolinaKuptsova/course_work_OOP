using System;
public class BankCard
{
    private string cardNumber;
    private DateTime expiredDate;
    private int cvv;

    public BankCard()
    {
    }

    public int CVV
    {
        get
        {
            return cvv;
        }
        set
        {
            if (value != 0)
            {
                cvv = value;
            }

        }
    }
    public DateTime ExpiredDate
    {
        get
        {
            return expiredDate;
        }
        set
        {
            if (value != null)
            {
                expiredDate = value;
            }

        }
    }

    public string CardNumber
    {
        get
        {
            return cardNumber;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                cardNumber = value;
            }

        }
    }
}