public class Bet
{
    public int Count { get; }
    public bool IsBlind { get; }

    public Bet(int count, bool isBlind)
    {
        Count = count;
        IsBlind = isBlind;
    }
}