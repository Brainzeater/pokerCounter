using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    [Header("State Machine")]
    [SerializeField] public GameObject StartingStateContent;
    [SerializeField] public GameObject BettingStateContent;
    [SerializeField] public GameObject GameStateContent;
    [SerializeField] public GameObject ScoringStateContent;
    [SerializeField] public GameObject FinishGameStateContent;
    [SerializeField] protected Button RestartButton;
    
    protected State CurrentState;

    public void ChangeState(State targetState)
    {
        // CurrentState?.Exit();

        CurrentState = targetState;

        CurrentState?.Enter();
    }
}