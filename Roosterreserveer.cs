public class  Roosterreserveer
{
    public static int Zaal { get; set; }
    public Roosterreserveer(int zaal)
    {
        Zaal = zaal;
    }
    
    public static void Reserve(bool user)
    {
        if (Zaal == 1)
        {
            ReserveringsManager.Reserveren(user);
        }
        else if (Zaal == 2)
        {
            ReserveringsManagerZaal2.Reserveren(user);
        }
        else if (Zaal == 3)
        {
            ReserveringsManagerZaal3.Reserveren(user);
        }
    }
}