using UnityEngine;

namespace States
{
    public class TurnState : State
    {
        private static int _numberOfTurns;
        private static int _numberOfTurnsInRound;
        private readonly Player _currentPlayer;
        private static int _currentPlayerIndex;
        private Round _currentRound;
        
        public TurnState(GameController gameController) : base(gameController)
        {
            _currentRound = RoundController.Instance.CurrentRound;
            // First turn in a round
            if (_numberOfTurns == 0)
            {
                _currentPlayerIndex = _currentRound.FirstPlayerIndex;
                _numberOfTurnsInRound = _currentRound.NumOfCardsInHand;
            }
            _currentPlayer = gameController.Players[_currentPlayerIndex];
        }

        public override void Enter()
        {
            Debug.LogError("You are in a turn state!");
            
        }
    }
}
