public class  Roosterreserveer
{
    public static int Zaal { get; set; }
    public Roosterreserveer(int zaal)
    {
        Zaal = zaal;
    }
    
    public static void Reser(bool user)
    {
        if (Zaal == 1)
        {
            Reservering.Reserveren(user);
        }
        else if (Zaal == 2)
        {
            ReserveringZaal2.Reserveren(user);
        }
        else if (Zaal == 3)
        {
            ReserveringZaal3.Reserveren(user);
        }
    }
}