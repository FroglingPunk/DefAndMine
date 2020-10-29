public static class eDirectionExtensions
{
    static public EDirection Opposite(this EDirection direction)
    {
        return (int)direction < 4 ? (direction + 4) : (direction - 4);
    }

    static public int Offset(this EDirection dir_1, EDirection dir_2)
    {
        return (dir_1 > dir_2 ? dir_1 - dir_2 : dir_2 - dir_1);
    }

    static public EDirection Direction(this Cell from, Cell to)
    {
        if (from.ZId == to.ZId)
        {
            return (to.XId > from.XId ? EDirection.E : EDirection.W);
        }
        else if (from.XId == to.XId)
        {
            return (to.ZId > from.ZId ? EDirection.N : EDirection.S);
        }
        else
        {
            int xOffset = (to.XId > from.XId ? to.XId - from.XId : from.XId - to.XId);
            int zOffset = (to.ZId > from.ZId ? to.ZId - from.ZId : from.ZId - to.ZId);

            float magnitude = UnityEngine.Mathf.Sqrt(xOffset * xOffset + zOffset * zOffset);
            float sin = zOffset / magnitude;

            if (to.XId > from.XId)
            {
                if (sin <= 0.5f)
                {
                    return EDirection.E;
                }
                else if (sin <= 0.70f)
                {
                    return (to.ZId > from.ZId ? EDirection.NE : EDirection.SE);
                }
                else
                {
                    return (to.ZId > from.ZId ? EDirection.E : EDirection.S);
                }
            }
            else
            {
                if (sin <= 0.5f)
                {
                    return EDirection.W;
                }
                else if (sin <= 0.707f)
                {
                    return (to.ZId > from.ZId ? EDirection.NW : EDirection.SW);
                }
                else
                {
                    return (to.ZId > from.ZId ? EDirection.N : EDirection.S);
                }
            }
        }
    }

    static public EDirection Next(this EDirection direction)
    {
        return (direction == EDirection.NW ? EDirection.N : direction + 1);
    }

    static public EDirection Previous(this EDirection direction)
    {
        return (direction == EDirection.N ? EDirection.NW : direction - 1);
    }

    static public EDirection Next2(this EDirection direction)
    {
        return (direction > EDirection.SW ? direction - 6 : direction + 2);
    }

    static public EDirection Previous2(this EDirection direction)
    {
        return (direction < EDirection.E ? direction + 6 : direction - 2);
    }
}