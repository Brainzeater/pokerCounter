namespace States
{
    public class GameState : State
    {
        public GameState(GameController gameController) : base(gameController)
        {
            GameController.GameStateContent.SetActive(true);
            var currentRound = RoundController.Instance.CurrentRound;
            if (currentRound.TypeOfRound == Round.RoundType.Regular)
            {
                GameController.bettingRoundTitle.SetText(currentRound.NumOfCardsInHand.ToString());
            }
            else
            {
                GameController.bettingRoundTitle.SetText(currentRound.TypeOfRound.ToString());
            }
        }
    }
}
