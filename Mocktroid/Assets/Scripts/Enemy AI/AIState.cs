using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class AIState
{
    public Path Path { get; set; }
    protected bool pathIsEnded = false;
    private int currentWaypoint = 0; // Waypoint we are currently moving towards
    protected System.Action StartUpdatePath;
    protected System.Action StopUpdatePath;

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void Update();

    public abstract void FixedUpdate();

    public void InitialiseStartUpdatePath(System.Action func)
    {
        StartUpdatePath = func;
    }

    public void InitialiseStopUpdatePath(System.Action func)
    {
        StopUpdatePath = func;
    }

    public int CurrentWaypoint
    {
        get
        {
            return currentWaypoint;
        }
        set
        {
            if (value >= 0)
                currentWaypoint = value;
        }
    }
}
