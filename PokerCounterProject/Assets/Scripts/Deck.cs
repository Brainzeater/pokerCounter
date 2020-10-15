using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    public List<Card> Cards { get; }

    public const int NumOfCardsInDeck = 36;
    private const int NumOfCardsInRegularSuit = 9;

    public Deck()
    {
        Cards = new List<Card>();

        GenerateDeck();
    }

    private void GenerateDeck()
    {
        // called 4 times: one time per suit
        foreach (var suit in Enum.GetValues(typeof(Card.Suit)).Cast<Card.Suit>())
        {
            var sameSuitCards = GenerateSameSuitCards(suit);
            Cards.AddRange(sameSuitCards);
        }

        if (Cards.Count != NumOfCardsInDeck)
        {
            throw new Exception("The Deck was not generated correctly!");
        }
    }

    private List<Card> GenerateSameSuitCards(Card.Suit suit)
    {
        var sameSuitCards = new List<Card>();
        switch (suit)
        {
            case Card.Suit.Spades:
            case Card.Suit.Clubs:
                var joker = new Card(100, suit, true);
                sameSuitCards.Add(joker);
                for (int i = 1; i < NumOfCardsInRegularSuit; i++)
                {
                    var card = new Card(i, suit, false);
                    sameSuitCards.Add(card);
                }

                break;
            case Card.Suit.Diamonds:
            case Card.Suit.Hearts:
                for (int i = 0; i < NumOfCardsInRegularSuit; i++)
                {
                    var card = new Card(i, suit, false);
                    sameSuitCards.Add(card);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(suit), suit, null);
        }

        return sameSuitCards;
    }

    public void PrintCards()
    {
        foreach (var card in Cards)
        {
            Debug.Log(card);
        }
    }
}