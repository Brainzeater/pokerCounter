using System;
using System.Collections.Generic;
using States;
using UnityEngine;
using Random = System.Random;

public class GameController : StateMachine
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PointsPanel pointsPanel;

    public const int NumberOfPlayers = 4;
    
    public static GameController Instance;

    public RectTransform Canvas => canvas;
    public PointsPanel PointsPanel => pointsPanel;
    public List<Player> Players { get; private set; }
    
    private Random _random;
    private int _firstPlayerIndex;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        Initialize();
        ChangeState(new NewRoundState(this));
    }

    private void Initialize()
    {
        _random = new Random();
        Players = new List<Player> {new Player("Kostya"), new Player("Stas"), new Player("Egor"), new Player("Sasha")};
        _firstPlayerIndex = _random.Next(NumberOfPlayers);

        var roundController = new RoundController(_firstPlayerIndex);
        roundController.Initialize();

        pointsPanel.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayRound();
        }
    }

    private void PlayRound()
    {
        RoundController.Instance.SwitchToNextRound();
        if (RoundController.Instance.CurrentRound != null)
        {
            Debug.LogError(RoundController.Instance.CurrentRound);
            Debug.LogError(RoundController.Instance.CurrentRound.Trump);
        }
        else
        {
            Debug.LogError("End of the game!");
        }
    }
}