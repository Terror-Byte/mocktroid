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

    public CrabIdle(CrabPathfindingInfo pathInfo, Transform patrolTargetLeft, Transform patrolTargetRight)
    {
        this.PathInfo = pathInfo;
        this.patrolTargetLeft = patrolTargetLeft;
        this.patrolTargetRight = patrolTargetRight;
    }

    public override void OnEnter()
    {
        // Begin pathfinding to right(?) node
        PathInfo.Target = patrolTargetRight;
        if (StartUpdatePath == null)
        {
            Debug.LogError("CrabIdle::OnEnter - StartUpdatePath delegate has not been assigned a function.");
            return;
        }
        StartUpdatePath();
    }

    public override void OnExit()
    {
        Path = null;
        CurrentWaypoint = 0;
        pathIsEnded = false;
        if (StopUpdatePath == null)
        {
            Debug.LogError("CrabIdle::OnExit - StopUpdatePath delegate has not been assigned a function.");
            return;
        }
        StopUpdatePath();
    }

    public override void Update()
    {
        // TODO: Add crab idle logic
    }

    public override void FixedUpdate()
    {
        if (PathInfo.Target == null)
            return;

        if (Path == null)
            return;

        if (CurrentWaypoint >= Path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached");
            pathIsEnded = true;

            // Set target to the other node
            if (PathInfo.Target == patrolTargetLeft)
                PathInfo.Target = patrolTargetRight;
            else if (PathInfo.Target == patrolTargetRight)
                PathInfo.Target = patrolTargetLeft;

            return;
        }
        pathIsEnded = false;

        Transform transform = PathInfo.Rb.transform;

        // Direction to the next waypoint
        Vector3 dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;

        if (dir.x < 0)
        {
            // Waypoint is to the left
            PathInfo.HorizontalMovement = -PathInfo.Speed;
        }
        else if (dir.x > 0)
        {
            // Waypoint is to the right
            PathInfo.HorizontalMovement = PathInfo.Speed;
        }
        else
        {
            PathInfo.HorizontalMovement = 0;
        }

        PathInfo.Animator.SetFloat("Speed", Mathf.Abs(PathInfo.HorizontalMovement));

        PathInfo.Controller.Move(PathInfo.HorizontalMovement * Time.fixedDeltaTime, false, false);

        // Check if enemy is at the next waypoint
        float dist = Vector3.Distance(transform.position, Path.vectorPath[CurrentWaypoint]);
        if (dist <= PathInfo.NextWaypointDistance)
        {
            CurrentWaypoint++;
            return;
        }
    }
}
