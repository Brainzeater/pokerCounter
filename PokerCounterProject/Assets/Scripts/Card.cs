public class Card
{
    public int Rank { get; }
    public Suit SuitOfCard { get; }
    public bool IsJoker { get; }
    public const int AceRank = 8;
    
    private string _spriteName;

    public string SpriteName => _spriteName;

    public enum Suit
    {
        Spades,
        Clubs,
        Diamonds,
        Hearts
    }

    public Card(int rank, Suit suitOfCard, bool isJoker)
    {
        Rank = rank;
        SuitOfCard = suitOfCard;
        IsJoker = isJoker;
        SetSpriteName();
    }

    private void SetSpriteName()
    {
        if (IsJoker)
        {
            _spriteName = "joker" + SuitOfCard.ToString().ToLower()[0];
            return;
        }

        switch (Rank)
        {
            case 0:
                _spriteName = "6";
                break;
            case 1:
                _spriteName = "7";
                break;
            case 2:
                _spriteName = "8";
                break;
            case 3:
                _spriteName = "9";
                break;
            case 4:
                _spriteName = "10";
                break;
            case 5:
                _spriteName = "j";
                break;
            case 6:
                _spriteName = "q";
                break;
            case 7:
                _spriteName = "k";
                break;
            case 8:
                _spriteName = "a";
                break;
        }

        _spriteName += SuitOfCard.ToString().ToLower()[0];
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override string ToString()
    {
        if (IsJoker)
        {
            return "Joker of " + SuitOfCard;
        }

        switch (Rank)
        {
            case 0:
                return "Six of " + SuitOfCard;
            case 1:
                return "Seven of " + SuitOfCard;
            case 2:
                return "Eight of " + SuitOfCard;
            case 3:
                return "Nine of " + SuitOfCard;
            case 4:
                return "Ten of " + SuitOfCard;
            case 5:
                return "Jack of " + SuitOfCard;
            case 6:
                return "Queen of " + SuitOfCard;
            case 7:
                return "King of " + SuitOfCard;
            case 8:
                return "Ace of " + SuitOfCard;
            default:
                return "WTF CARD";
        }
    }
}