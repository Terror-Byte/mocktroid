using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabAttacking : AIState
{
    private CrabPathfindingInfo pathInfo;

    public CrabAttacking(CrabPathfindingInfo pathInfo)
    {
        this.pathInfo = pathInfo;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        // TODO: Add crab attacking logic
    }

    public override void FixedUpdate()
    {

    }
}
