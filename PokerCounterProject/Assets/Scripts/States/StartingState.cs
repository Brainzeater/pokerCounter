using System;

namespace States
{
    public class StartingState : State
    {
        private Random _random;
        public StartingState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            GameController.StartingStateContent.SetActive(true);
            GameController.inputWindow.SetPlayerIndex(GameController.Players.Count);
            GameController.inputWindow.OnNameSubmitted += OnNameSubmitted;
        }

        private void OnNameSubmitted(string name)
        {
            GameController.inputWindow.OnNameSubmitted -= OnNameSubmitted;
            var player = new Player(name);
            GameController.Players.Add(player);
            Exit();
        }

        public override void Exit()
        {
            if (GameController.Players.Count < GameController.NumberOfPlayers)
            {
                GameController.ChangeState(new StartingState(GameController));
            }
            else
            {
                _random = new Random();
                var randomPlayer = _random.Next(GameController.NumberOfPlayers);
                new RoundController(randomPlayer);
                RoundController.Instance.Initialize();
            
                GameController.StartingStateContent.SetActive(false);
                GameController.ChangeState(new BettingState(GameController));
            }
        }
    }
}
