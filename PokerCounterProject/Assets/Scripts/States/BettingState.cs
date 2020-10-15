using System;
using System.Linq;
using Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace States
{
    public class BettingState : State
    {
        private static int _numberOfBets;
        private static int _currentPlayerIndex;
        private BettingWindow _bettingWindow;
        private readonly Player _currentPlayer;

        private int ForbiddenBetCount
        {
            get
            {
                if (_numberOfBets != 3) return -1;

                var currentRoundMaxTricks = RoundController.Instance.CurrentRound.NumOfCardsInHand;
                var sumOfBets = GameController.Players.Sum(player => player.CurrentBet?.Count ?? 0);
                return currentRoundMaxTricks - sumOfBets;
            }
        }

        public BettingState(GameController gameController) : base(gameController)
        {
            // First betting state in a round
            if (_numberOfBets == 0)
            {
                _currentPlayerIndex = RoundController.Instance.CurrentRound.FirstPlayerIndex;
                var currentRound = RoundController.Instance.CurrentRound;
                if (currentRound.TypeOfRound == Round.RoundType.Regular)
                {
                    GameController.bettingRoundTitle.SetText(currentRound.NumOfCardsInHand.ToString());
                }
                else
                {
                    GameController.bettingRoundTitle.SetText(currentRound.TypeOfRound.ToString());
                }
                
                GameController.BettingPointsPanel.Initialize();
                GameController.bettingWindow.Initialize();
            }

            
            _currentPlayer = gameController.Players[_currentPlayerIndex];
        }

        public override void Enter()
        {
            GameController.BettingStateContent.SetActive(true);
            switch (RoundController.Instance.CurrentRound.TypeOfRound)
            {
                case Round.RoundType.Regular:
                case Round.RoundType.Blind:
                case Round.RoundType.Trumpless:
                    Debug.LogError("You are in a betting state!");
            
                    GameController.BettingPointsPanel.Highlight(_currentPlayerIndex);
                    
                    _bettingWindow = GameController.bettingWindow;
                    TrySetForbiddenBetCount(_bettingWindow);
                    _bettingWindow.OnConfirmed += OnBetConfirmed;
                    break;
                
                default:
                    Debug.LogError($"You are not supposed to be betting in " +
                                        $"{RoundController.Instance.CurrentRound.TypeOfRound}!");
                    _numberOfBets = GameController.NumberOfPlayers;
                    Exit();
                    break;
            }
        }

        private void TrySetForbiddenBetCount(BettingWindow bettingWindow)
        {
            if (ForbiddenBetCount >= 0)
            {
                bettingWindow.ForbiddenBet = ForbiddenBetCount;
            }
        }
        
        private void OnBetConfirmed(int betCount, bool isBlind)
        {
            _bettingWindow.OnConfirmed -= OnBetConfirmed;

            _currentPlayer.MakeBet(betCount, isBlind);
            
            GameController.BettingPointsPanel.UpdateBetInfo(_currentPlayerIndex);
            GameController.BettingPointsPanel.Unhighlight(_currentPlayerIndex);

            _numberOfBets++;
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= GameController.NumberOfPlayers)
                _currentPlayerIndex = 0;

            Exit();
        }

        public override void Exit()
        {
            // Last betting state in a round
            if (_numberOfBets >= GameController.NumberOfPlayers)
            {
                _numberOfBets = 0;
                Debug.LogError("No more bets!");
                GameController.BettingStateContent.SetActive(false);
                GameController.ChangeState(new GameState(GameController));
            }
            else
            {
                GameController.bettingWindow.ResetBettingWindow();
                GameController.ChangeState(new BettingState(GameController));
            }
        }
    }
}