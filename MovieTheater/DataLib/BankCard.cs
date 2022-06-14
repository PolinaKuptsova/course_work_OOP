using System;
public class BankCard
{
    private string cardNumber;
    private DateTime expiredDate;
    private int cvv;

    public BankCard()
    {
    }

    public BankCard(string cardNumber, DateTime expiredDate, int cvv)
    {
        CardNumber = cardNumber;
        ExpiredDate = expiredDate;
        this.cvv = cvv;
    }

    public int CVV
    {
        get
        {
            return CVV;
        }
        set
        {
            if (value != 0)
            {
                CVV = value;
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
            if (value== null)
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
            if (string.IsNullOrEmpty(value))
            {
                cardNumber = value;
            }

        }
    }
}