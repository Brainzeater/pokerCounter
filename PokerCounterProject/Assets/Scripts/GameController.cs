using System;
using System.Collections.Generic;
using Windows;
using States;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class GameController : StateMachine
{
    [Header("Game Controller")]
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PointsPanel bettingPointsPanel;
    [SerializeField] public TextMeshProUGUI bettingRoundTitle;
    [SerializeField] public TextMeshProUGUI gameRoundTitle;

    [Header("Windows")]
    [SerializeField] public NameInputWindow inputWindow;
    [SerializeField] public BettingWindow bettingWindow;

    public const int NumberOfPlayers = 4;
    
    public static GameController Instance;

    public RectTransform Canvas => canvas;
    public PointsPanel BettingPointsPanel => bettingPointsPanel;
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
        ChangeState(new StartingState(this));
        // ChangeState(new NewRoundState(this));
    }

    private void Initialize()
    {
        StartingStateContent.SetActive(false);
        BettingStateContent.SetActive(false);
        GameStateContent.SetActive(false);
        ScoringStateContent.SetActive(false);
        FinishGameStateContent.SetActive(false);
        // _random = new Random();
        // Players = new List<Player> {new Player("Kostya"), new Player("Stas"), new Player("Egor"), new Player("Sasha")};
        Players = new List<Player>();
        // _firstPlayerIndex = _random.Next(NumberOfPlayers);
        //
        // var roundController = new RoundController(_firstPlayerIndex);
        // roundController.Initialize();

        // pointsPanel.Initialize();
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