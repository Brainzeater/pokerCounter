using System;

public class Round
{
    public int Order { get; }
    public RoundType TypeOfRound { get; private set; }
    public int FirstPlayerIndex { get; }
    public Player FirstPlayer { get; }
    public int NumOfCardsInHand { get; private set; }
    public int BetCost { get; private set; }
    public int PassCost { get; private set; }

    public Card.Suit? Trump
    {
        get => _trump;
        set
        {
            if (value == null)
                TypeOfRound = RoundType.Trumpless;

            _trump = value;
        }
    }

    private Card.Suit? _trump;

    private const int NumberOfFirstRounds = 2 * GameController.NumberOfPlayers;

    public enum RoundType
    {
        Regular,
        Blind,
        Trumpless,
        Mizer,
        Gold
    }

    public Round(int order, RoundType typeOfRound, int firstPlayerIndex)
    {
        Order = order;
        TypeOfRound = typeOfRound;
        FirstPlayerIndex = firstPlayerIndex;
        FirstPlayer = GameController.Instance.Players[FirstPlayerIndex];
        SetNumOfCardsInHand();
        SetPointsCosts();
    }

    private void SetNumOfCardsInHand()
    {
        if (TypeOfRound == RoundType.Regular && Order <= NumberOfFirstRounds)
        {
            NumOfCardsInHand = Order;
        }
        else
        {
            NumOfCardsInHand = Dealer.MaxNumOfCardsInHand;
        }
    }

    private void SetPointsCosts()
    {
        switch (TypeOfRound)
        {
            case RoundType.Regular:
            case RoundType.Blind:
            case RoundType.Trumpless:
                BetCost = 10;
                PassCost = 5;
                break;
            case RoundType.Mizer:
                BetCost = -10;
                PassCost = 20;
                break;
            case RoundType.Gold:
                BetCost = 20;
                PassCost = 10;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override string ToString()
    {
        return $"{Order}. {NumOfCardsInHand} - {TypeOfRound}. {FirstPlayer}";
    }
}