using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class AIState
{
    public Path Path { get; set; }
    private bool pathIsEnded = false;
    private int currentWaypoint = 0; // Waypoint we are currently moving towards

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void Update();

    public abstract void FixedUpdate();

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
