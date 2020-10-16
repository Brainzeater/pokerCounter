using System.Linq;
using UnityEngine;

namespace States
{
    public class FinishGameState : State
    {
        public FinishGameState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            GameController.FinishGameStateContent.SetActive(true);
            GameController.finishPointsPanel.Initialize();
            var players = GameController.Players;
            var winnerIndex = -1;
            var maxPoints = -1;
            for (int i = 0; i < GameController.NumberOfPlayers; i++)
            {
                if (players[i].Points <= maxPoints) continue;
                maxPoints = players[i].Points;
                winnerIndex = i;
            }
            GameController.finishPointsPanel.Highlight(winnerIndex);
        }
    }
}
