static public class EDirectionExtensions
{
    static public EDirection Opposite(this EDirection direction)
    {
        return direction < EDirection.S ? direction + 2 : direction - 2;
    }

    static public EDirection Next(this EDirection direction)
    {
        return direction == EDirection.W ? EDirection.N : direction + 1;
    }

    static public EDirection Previous(this EDirection direction)
    {
        return direction == EDirection.N ? EDirection.W : direction - 1;
    }
}