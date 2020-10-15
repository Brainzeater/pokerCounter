using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Transform> playerPositions;
    [SerializeField] private Transform trumpCardPosition;

    private UnityEngine.Object[] _cardSprites;

    private List<GameObject> _cards;

    public void Initialize()
    {
        _cards = new List<GameObject>();
        if (_cardSprites == null || _cardSprites.Length == 0)
        {
            _cardSprites = Resources.LoadAll("Sprites/Cards");
        }
    }

    public void SpawnPlayersCards(List<Player> players)
    {
        ClearTable();
        if (_cardSprites == null || _cardSprites.Length == 0)
        {
            throw new Exception("CardSpawner is not initialized!");
        }

        for (int i = 0; i < players.Count; i++)
        {
            foreach (var card in players[i].Hand)
            {
                SpawnCard(card, players[i], playerPositions[i]);
            }
        }
    }

    private void SpawnCard(Card card, Player holder, Transform position)
    {
        var cardSpriteName = (Sprite) _cardSprites.First(c => c.name.Equals(card.SpriteName));
        if (cardSpriteName == null)
        {
            throw new Exception("CardSpawner: there is no card with such a name");
        }

        var cardGO = Instantiate(cardPrefab, position);
        var cardComponent = cardGO.GetComponent<CardComponent>();
        cardComponent.Initialize(card);
        if (holder != null)
            cardComponent.Holder = holder;
        var cardImage = cardComponent.Image;
        cardImage.sprite = cardSpriteName;
        _cards.Add(cardGO);
    }

    public void SpawnTrumpCard(Card card)
    {
        SpawnCard(card, null, trumpCardPosition);
    }
    
    private void ClearTable()
    {
        foreach (var card in _cards)
        {
            Destroy(card.gameObject);
        }
    }
}