using System;
public class TicketPurchase
{
    public long id;  
    public long ticket_id;
    public DateTime createdAt;
    public double price;
    public long customer_id;
    public string payment_way;
    public bool isCanceled;
    public long barSet_id;
    public TicketPurchase()
    {
        this.id = 0;
        this.ticket_id = 0;
    }

    public TicketPurchase(long ticket_id, string payment_way)
    {
        this.payment_way = payment_way;
        this.ticket_id = ticket_id;
    }

    public TicketPurchase(long order_id, long ticket_id, string payment_way)
    {
        this.ticket_id = ticket_id;
        this.payment_way = payment_way;

    }

    public override string ToString()
    {
        return string.Format($"({id})");
    }
}