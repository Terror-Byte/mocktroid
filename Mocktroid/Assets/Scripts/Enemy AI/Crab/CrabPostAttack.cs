using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPostAttack : AIState
{
    private CrabPathfindingInfo pathInfo;

    public CrabPostAttack(CrabPathfindingInfo pathInfo)
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
        // TODO: Add crab post-attack logic
    }

    public override void FixedUpdate()
    {

    }
}
