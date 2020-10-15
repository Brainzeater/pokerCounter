using System.Collections.Generic;
using UnityEngine;

public class PointsPanel : MonoBehaviour
{
    [SerializeField] private GameObject playerPointsCard;

    private List<PlayerPointsCard> _playerPointsCardsList;

    public void Initialize()
    {
        _playerPointsCardsList = new List<PlayerPointsCard>();

        foreach (var player in GameController.Instance.Players)
        {
            var playerCardGO = Instantiate(playerPointsCard, transform);
            var playerCardComponent = playerCardGO.GetComponent<PlayerPointsCard>();
            playerCardComponent.Name = $"{player.Name}:";
            playerCardComponent.Points = 0;
            playerCardComponent.ClearBetContent();
            _playerPointsCardsList.Add(playerCardComponent);
        }
    }

    public void Highlight(int playerIndex)
    {
        _playerPointsCardsList[playerIndex].HighlightPanel();
    }

    public void Unhighlight(int playerIndex)
    {
        _playerPointsCardsList[playerIndex].UnhighlightPanel();
    }

    public void UpdateBetInfo(int playerIndex)
    {
        var pointsCard = _playerPointsCardsList[playerIndex];
        var player = GameController.Instance.Players[playerIndex];
        pointsCard.BetCount = player.CurrentBet.Count;
        pointsCard.BetIsBlind = player.CurrentBet.Count != 0 && player.CurrentBet.IsBlind;
    }

    public void UpdatePointsInfo(int playerIndex)
    {
        var pointsCard = _playerPointsCardsList[playerIndex];
        var player = GameController.Instance.Players[playerIndex];
        pointsCard.Points = player.Points;
        pointsCard.ClearBetContent();
    }
}