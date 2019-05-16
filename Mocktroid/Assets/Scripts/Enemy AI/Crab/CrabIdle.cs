using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabIdle : AIState
{
    public CrabPathfindingInfo PathInfo { get; private set; }
    private Transform patrolTargetLeft;
    private Transform patrolTargetRight;
    private bool isPatrollingRight = true;

    //private Path path;
    //private bool pathIsEnded = false;
    //private int currentWaypoint = 0; // Waypoint we are currently moving towards

    public CrabIdle(CrabPathfindingInfo pathInfo)
    {
        this.PathInfo = pathInfo;
    }

    public override void OnEnter()
    {
        // Begin pathfinding to right(?) node
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        // TODO: Add crab idle logic
    }

    public override void FixedUpdate()
    {

    }
}
