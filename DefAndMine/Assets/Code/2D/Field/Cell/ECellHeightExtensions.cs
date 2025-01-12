public static class ECellHeightExtensions
{
    public static bool IsUpper(this ECellHeight originHeight, ECellHeight comparedHeight)
    {
        return originHeight switch
        {
            ECellHeight.Ground => comparedHeight == ECellHeight.Crevice,
            ECellHeight.Elevation => comparedHeight != ECellHeight.Elevation,
            ECellHeight.Crevice => false
        };
    }
    
    public static bool IsLower(this ECellHeight originHeight, ECellHeight comparedHeight)
    {
        return originHeight switch
        {
            ECellHeight.Ground => comparedHeight == ECellHeight.Elevation,
            ECellHeight.Elevation => false,
            ECellHeight.Crevice => comparedHeight != ECellHeight.Crevice
        };
    }
}