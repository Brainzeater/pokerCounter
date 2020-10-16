using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringPointsPanel : MonoBehaviour
{
    [SerializeField] private GameObject playerPointsCard;
    private List<PlayerPointsCard> _playerPointsCards;
    
    public void Initialize()
    {
        _playerPointsCards = new List<PlayerPointsCard>();
        foreach (var player in GameController.Instance.Players)
        {
            var playerCardGO = Instantiate(playerPointsCard, transform);
            var playerCardComponent = playerCardGO.GetComponent<PlayerPointsCard>();
            playerCardComponent.Name = $"{player.Name}:";
            playerCardComponent.Points = player.Points;
            playerCardComponent.ClearBetContent();
            playerCardComponent.BetCount = player.CurrentBet?.Count ?? 0;
            playerCardComponent.BetIsBlind = player.CurrentBet?.IsBlind ?? false;
            _playerPointsCards.Add(playerCardComponent);
        }
        
        // _playerPointsCards[RoundController.Instance.CurrentRound.FirstPlayerIndex].HighlightPanel();
    }
    
    public void Reset()
    {
        foreach (var pointsCard in _playerPointsCards)
        {
            Destroy(pointsCard.gameObject);
        }
        _playerPointsCards.Clear();
    }
    
    public void Highlight(int playerIndex)
    {
        _playerPointsCards[playerIndex].HighlightPanel();
    }
    
    public void Unhighlight(int playerIndex)
    {
        _playerPointsCards[playerIndex].UnhighlightPanel();
    }

    public void UpdateBetInfo(int playerIndex)
    {
        var pointsCard = _playerPointsCards[playerIndex];
        var player = GameController.Instance.Players[playerIndex];
        
        if (player.CurrentBet == null) return;
        pointsCard.BetCount = player.CurrentBet.Count;
        pointsCard.BetIsBlind = player.CurrentBet.Count != 0 && player.CurrentBet.IsBlind;
    }

    public void UpdatePointsInfo(int playerIndex)
    {
        var pointsCard = _playerPointsCards[playerIndex];
        var player = GameController.Instance.Players[playerIndex];
        pointsCard.Points = player.Points;
        pointsCard.ClearBetContent();
    }
}
