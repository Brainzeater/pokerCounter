using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Dealer
{
    public const int MaxNumOfCardsInHand = 9;
    public Card TrumpCard;
    private static readonly Random Random = new Random();

    private readonly Deck _deck;

    public Dealer()
    {
        _deck = new Deck();
    }

    public void ShuffleDeck()
    {
        Shuffle();
        
        if (RoundController.Instance.CurrentRound.NumOfCardsInHand < MaxNumOfCardsInHand)
        {
            var lastCardIndex = Deck.NumOfCardsInDeck - 1;
            var lastCardInDeck = _deck.Cards[lastCardIndex];
            while (lastCardInDeck.IsJoker || lastCardInDeck.Rank == Card.AceRank)
            {
                Shuffle();
                lastCardInDeck = _deck.Cards[lastCardIndex];
                // Debug.LogError(lastCardInDeck);
            }
        }

        // _deck.PrintCards();

        void Shuffle()
        {
            var n = Deck.NumOfCardsInDeck;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                var value = _deck.Cards[k];
                _deck.Cards[k] = _deck.Cards[n];
                _deck.Cards[n] = value;
            }
        }
    }

    public void DealCards(List<Player> players)
    {
        var numOfCardsInHand = RoundController.Instance.CurrentRound.NumOfCardsInHand;

        if (numOfCardsInHand > MaxNumOfCardsInHand)
        {
            throw new Exception("Dealer: you want me to deal too many cards!");
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].Hand = _deck.Cards.Skip(numOfCardsInHand * i).Take(numOfCardsInHand).ToList();
        }

        // Setting the trump for first 8 rounds
        if (numOfCardsInHand < MaxNumOfCardsInHand)
        {
            TrumpCard = _deck.Cards.Skip(numOfCardsInHand * GameController.NumberOfPlayers).Take(1).ToList()[0];

            RoundController.Instance.CurrentRound.Trump = TrumpCard.IsJoker ? (Card.Suit?) null : TrumpCard.SuitOfCard;
        }
    }
}