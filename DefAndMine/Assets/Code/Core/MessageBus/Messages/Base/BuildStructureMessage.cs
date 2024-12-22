public class BuildStructureMessage : IMessage
{
    public readonly StructureBase Structure;


    public BuildStructureMessage(StructureBase structure)
    {
        Structure = structure;
    }
}