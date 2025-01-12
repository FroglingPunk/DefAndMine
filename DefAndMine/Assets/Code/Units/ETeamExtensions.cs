public static class ETeamExtensions 
{
    public static ETeam Opposite(this ETeam team)
    {
        return team == ETeam.Player ? ETeam.Enemy : ETeam.Player;
    }
}