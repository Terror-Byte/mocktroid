using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void Update();

    public abstract void FixedUpdate();
}
