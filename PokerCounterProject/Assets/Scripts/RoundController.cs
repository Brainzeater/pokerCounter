using System.Collections.Generic;
using UnityEngine;

public class RoundController
{
    public static RoundController Instance { get; private set; }

    private readonly int _firstPlayerIndex;

    private const int RoundsSet = GameController.NumberOfPlayers;
    private const int RoundSequenceLength = RoundsSet * 7;
    private const int NumberOfRegularRounds = RoundsSet * 3;
    private Queue<Round.RoundType> _orderOfRoundTypes;
    private Queue<Round> _roundSequence;

    public Round CurrentRound => _roundSequence.Count != 0 ? _roundSequence.Peek() : null;

    public RoundController(int firstPlayerIndex)
    {
        Instance = this;
        _firstPlayerIndex = firstPlayerIndex;
    }

    public void Initialize()
    {
        GenerateOrderOfRoundTypes();
        GenerateRoundSequence();
    }

    private void GenerateOrderOfRoundTypes()
    {
        _orderOfRoundTypes = new Queue<Round.RoundType>();
        _orderOfRoundTypes.Enqueue(Round.RoundType.Regular);
        _orderOfRoundTypes.Enqueue(Round.RoundType.Blind);
        _orderOfRoundTypes.Enqueue(Round.RoundType.Trumpless);
        _orderOfRoundTypes.Enqueue(Round.RoundType.Mizer);
        _orderOfRoundTypes.Enqueue(Round.RoundType.Gold);
    }

    private void GenerateRoundSequence()
    {
        _roundSequence = new Queue<Round>();

        var currentFirstPlayerIndex = _firstPlayerIndex;
        
        for (int roundOrder = 1; roundOrder <= RoundSequenceLength; roundOrder++)
        {
            Round round;
            // First 12 rounds
            if (roundOrder <= NumberOfRegularRounds)
            {
                round = new Round(roundOrder, _orderOfRoundTypes.Peek(), currentFirstPlayerIndex);
            }
            else
            {
                // Switch round type after first 12 rounds
                // and keep switching it each 4-th round
                if ((roundOrder - 1) % RoundsSet == 0)
                {
                    _orderOfRoundTypes.Dequeue();
                }

                round = new Round(roundOrder, _orderOfRoundTypes.Peek(), currentFirstPlayerIndex);
            }

            _roundSequence.Enqueue(round);

            currentFirstPlayerIndex++;
            if (currentFirstPlayerIndex >= GameController.NumberOfPlayers)
                currentFirstPlayerIndex = 0;
        }
    }

    public void SwitchToNextRound()
    {
        if (_roundSequence.Count != 0)
        {
            _roundSequence.Dequeue();
        }
    }

    private void PrintRoundSequence()
    {
        foreach (var round in _roundSequence)
        {
            Debug.LogError(round);
        }
    }
}