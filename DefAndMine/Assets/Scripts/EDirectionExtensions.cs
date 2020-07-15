static public class EDirectionExtensions
{
    static public EDirection Opposite(this EDirection direction)
    {
        return direction < EDirection.S ? direction + 2 : direction - 2;
    }
}