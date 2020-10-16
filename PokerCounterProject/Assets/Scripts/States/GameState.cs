namespace States
{
    public class GameState : State
    {
        public GameState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            GameController.GameStateContent.SetActive(true);
            var currentRound = RoundController.Instance.CurrentRound;
            if (currentRound.TypeOfRound == Round.RoundType.Regular)
            {
                GameController.gameRoundTitle.SetText(currentRound.NumOfCardsInHand.ToString());
            }
            else
            {
                GameController.gameRoundTitle.SetText(currentRound.TypeOfRound.ToString());
            }

            GameController.idlePointsPanel.Initialize();
            GameController.finishRoundButton.onClick.AddListener(Exit);
        }

        public override void Exit()
        {
            GameController.idlePointsPanel.Reset();
            GameController.GameStateContent.SetActive(false);
            GameController.ChangeState(new ScoringState(GameController));
        }
    }
}