using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State CurrentState;

    public void ChangeState(State targetState)
    {
        // CurrentState?.Exit();

        CurrentState = targetState;

        CurrentState?.Enter();
    }
}