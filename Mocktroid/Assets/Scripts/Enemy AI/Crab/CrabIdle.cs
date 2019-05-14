using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabIdle : AIState
{
    private CrabPathfindingInfo pathInfo;
    private Transform patrolTargetLeft;
    private Transform patrolTargetRight;
    private bool isPatrollingRight = true;

    public CrabIdle(CrabPathfindingInfo pathInfo)
    {
        this.pathInfo = pathInfo;
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
