using UnityEngine;

namespace States
{
    public class NewRoundState : State
    {
        public NewRoundState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            if (RoundController.Instance.CurrentRound != null)
            {
                Debug.LogError(RoundController.Instance.CurrentRound);

                // foreach (var player in Players)
                // {
                //     player.PrintHand();
                // }

                GameController.ChangeState(new BettingState(GameController));
            }
            else
            {
                Debug.LogError("End of the game!");
            }
        }
    }
}