using UnityEngine;

namespace States
{
    public class ScoringState : State
    {
        private static int _numberOfPlayers;
        private static int _currentPlayerIndex;
        private static int _sumOfTricks;
        private readonly Player _currentPlayer;

        public ScoringState(GameController gameController) : base(gameController)
        {
            GameController.ScoringStateContent.SetActive(true);
            // First scoring state in a round
            if (_numberOfPlayers == 0)
            {
                _currentPlayerIndex = RoundController.Instance.CurrentRound.FirstPlayerIndex;

                var currentRound = RoundController.Instance.CurrentRound;
                if (currentRound.TypeOfRound == Round.RoundType.Regular)
                {
                    GameController.scoringRoundTitle.SetText(currentRound.NumOfCardsInHand.ToString());
                }
                else
                {
                    GameController.scoringRoundTitle.SetText(currentRound.TypeOfRound.ToString());
                }

                GameController.scoringPointsPanel.Initialize();
                GameController.scoringWindow.Initialize();
            }

            _currentPlayer = gameController.Players[_currentPlayerIndex];
        }

        public override void Enter()
        {
            GameController.scoringWindow.SumOfTricks = _sumOfTricks;
            GameController.scoringWindow.NumOfCalculatedPlayers = _numberOfPlayers;
            GameController.scoringWindow.ResetScoringWindow();
            GameController.scoringPointsPanel.Highlight(_currentPlayerIndex);
            GameController.scoringWindow.OnConfirmed += OnPointsConfirmed;
        }

        private void OnPointsConfirmed(int tricks)
        {
            GameController.scoringWindow.OnConfirmed -= OnPointsConfirmed;
            _sumOfTricks += tricks;
            _currentPlayer.CalculatePoints(tricks);
            GameController.scoringPointsPanel.UpdateBetInfo(_currentPlayerIndex);
            GameController.scoringPointsPanel.UpdatePointsInfo(_currentPlayerIndex);
            GameController.scoringPointsPanel.Unhighlight(_currentPlayerIndex);

            _numberOfPlayers++;
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= GameController.NumberOfPlayers)
                _currentPlayerIndex = 0;

            Exit();
        }

        public override void Exit()
        {
            // Last scoring state in a round
            if (_numberOfPlayers >= GameController.NumberOfPlayers)
            {
                _numberOfPlayers = 0;
                _sumOfTricks = 0;
                GameController.scoringPointsPanel.Reset();
                GameController.scoringWindow.Close();
                GameController.ScoringStateContent.SetActive(false);
                RoundController.Instance.SwitchToNextRound();

                if (RoundController.Instance.CurrentRound != null)
                {
                    GameController.ChangeState(new BettingState(GameController));
                }
                else
                {
                    Debug.LogError("GAME OVER!");
                    GameController.ChangeState(new GameState(GameController));
                }
            }
            else
            {
                GameController.ChangeState(new ScoringState(GameController));
            }
        }
    }
}