using System;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

public class Player
{
    public string Name { get; }

    public Bet CurrentBet { get; private set; }
    public int Points;

    public List<Tuple<Card, Card, Card, Card>> Tricks { get; }

    public List<Card> Hand
    {
        get => _hand;
        set
        {
            _hand = value;
            SortHand();
        }
    }

    private List<Card> _hand;

    public Player(string name)
    {
        Name = name;
        Tricks = new List<Tuple<Card, Card, Card, Card>>();
    }

    private void SortHand()
    {
        // Firstly, take out all the jokers
        var jokers = _hand.FindAll(card => card.IsJoker);
        _hand.RemoveAll(card => card.IsJoker);

        // Secondly, split the hand in sets of cards of the same suit
        var suitSets = new List<List<Card>>();
        foreach (var suit in Enum.GetValues(typeof(Card.Suit)).Cast<Card.Suit>())
        {
            suitSets.Add(_hand.FindAll(card => card.SuitOfCard == suit));
            _hand.RemoveAll(card => card.SuitOfCard == suit);
        }
        // At this point the hand is empty

        // Put the jokers into the hand
        _hand.AddRange(jokers);

        // Finally, sort each suit set and put back into the hand
        foreach (var suitSet in suitSets)
        {
            suitSet.Sort((x, y) => x.Rank.CompareTo(y.Rank));
            _hand.AddRange(suitSet);
        }
    }

    public void MakeBet(int amount, bool blind)
    {
        CurrentBet = new Bet(amount, blind);
    }

    public void AddTrick(Tuple<Card, Card, Card, Card> trick)
    {
        Tricks.Add(trick);
    }

    public void CalculatePoints(int tricks)
    {
        var gainedPoints = 0;

        var currentRound = RoundController.Instance.CurrentRound;
        // var tricksCount = Tricks.Count;
        var tricksCount = tricks;

        switch (currentRound.TypeOfRound)
        {
            case Round.RoundType.Regular:
            case Round.RoundType.Blind:
            case Round.RoundType.Trumpless:
                var difference = tricksCount - CurrentBet.Count;
                var blindCoefficient = currentRound.TypeOfRound != Round.RoundType.Blind && CurrentBet.IsBlind ? 2 : 1;

                if (difference == 0)
                {
                    if (CurrentBet.Count == 0)
                        gainedPoints = currentRound.PassCost;
                    else
                        gainedPoints = CurrentBet.Count * currentRound.BetCost * blindCoefficient;
                }
                // Surpluss
                else if (difference > 0)
                {
                    gainedPoints = tricksCount * blindCoefficient;
                }
                // Shortage
                else
                {
                    gainedPoints = difference * (currentRound.BetCost) * blindCoefficient;
                }

                break;

            case Round.RoundType.Mizer:
            case Round.RoundType.Gold:
                if (tricksCount == 0)
                    gainedPoints = currentRound.PassCost;
                else if (tricksCount > 0)
                    gainedPoints = tricksCount * currentRound.BetCost;
                else
                    throw new Exception($"{Name} has negative number of tricks ({tricksCount}) in {currentRound.TypeOfRound}!");
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (gainedPoints == 0)
            throw new Exception($"{Name} gained zero points!");

        Points += gainedPoints;
        
        // TODO: после этого нужно обнулять текущую ставку, чистить взятки
    }

    public override string ToString()
    {
        return Name;
    }

    #region HELPERS

    public void PrintHand()
    {
        var messageString = $"{this} has:\n";

        foreach (var card in Hand)
        {
            messageString += card + "\n";
        }

        Debug.Log(messageString);
    }

    #endregion
}