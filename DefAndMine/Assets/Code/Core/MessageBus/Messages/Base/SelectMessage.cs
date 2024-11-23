public class SelectMessage<T> : IMessage
{
    public readonly T Selected;


    public SelectMessage(T selected)
    {
        Selected = selected;
    }
}