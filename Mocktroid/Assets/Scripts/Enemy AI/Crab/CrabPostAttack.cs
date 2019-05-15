using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPostAttack : AIState
{
    public CrabPathfindingInfo PathInfo { get; private set; }
    // May not be needed here
    private Path path;
    private bool pathIsEnded = false;
    private int currentWaypoint = 0; // Waypoint we are currently moving towards

    public CrabPostAttack(CrabPathfindingInfo pathInfo)
    {
        this.PathInfo = pathInfo;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        // TODO: Add crab post-attack logic
    }

    public override void FixedUpdate()
    {

    }
}
