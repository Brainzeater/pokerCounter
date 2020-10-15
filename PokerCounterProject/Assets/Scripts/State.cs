﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly GameController GameController;

    protected State(GameController gameController)
    {
        GameController = gameController;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}