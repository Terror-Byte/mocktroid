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

    public CrabIdle(CrabPathfindingInfo pathInfo, Transform patrolTargetLeft, Transform patrolTargetRight)
    {
        this.pathInfo = pathInfo;
        this.patrolTargetLeft = patrolTargetLeft;
        this.patrolTargetRight = patrolTargetRight;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Idle State.");

        // Begin pathfinding to right(?) node
        // TODO: Maybe wait for a second or so so the transition isn't so jarring?
        pathInfo.Target = patrolTargetRight;
        if (StartUpdatePath == null)
        {
            Debug.LogError("CrabIdle::OnEnter - StartUpdatePath delegate has not been assigned a function.");
            return;
        }
        StartUpdatePath();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Idle State.");

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
        // If player is within radius, transition to pursuing state
        float distToPlayer = (pathInfo.Player.transform.position - pathInfo.Rb.transform.position).magnitude;

        if (distToPlayer < pathInfo.PlayerPursuitThreshold)
        {
            // Player is within the pursuit threshold, transition to pursuing state
            TransitionTo("Pursuing");
            return;
        }
    }

    public override void FixedUpdate()
    {
        if (pathInfo.Target == null)
            return;

        if (Path == null)
            return;

        if (CurrentWaypoint >= Path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            // Debug.Log("End of path reached");
            pathIsEnded = true;

            // Set target to the other node
            if (pathInfo.Target == patrolTargetLeft)
                pathInfo.Target = patrolTargetRight;
            else if (pathInfo.Target == patrolTargetRight)
                pathInfo.Target = patrolTargetLeft;

            return;
        }
        pathIsEnded = false;

        Transform transform = pathInfo.Rb.transform;

        // Direction to the next waypoint
        Vector3 dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;


        if (CurrentWaypoint == 0)
        {
            if (pathInfo.HorizontalMovement != 0)
            {
                // We're already moving

                Vector3 nextDir = (Path.vectorPath[CurrentWaypoint + 1] - transform.position).normalized;

                // If we are already moving LEFT, current waypoint is to the RIGHT but the next waypoint is LEFT
                if (pathInfo.HorizontalMovement == -pathInfo.Speed && dir.x > 0 && nextDir.x < 0)
                {
                    CurrentWaypoint++;
                    dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;
                }
                // If we are already moving RIGHT, currently waypoint is to the LEFT but the next waypoint is RIGHT.
                else if (pathInfo.HorizontalMovement == pathInfo.Speed && dir.x < 0 && nextDir.x > 0)
                {
                    CurrentWaypoint++;
                    dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;
                }
            }
        }


        if (dir.x < 0)
        {
            // Waypoint is to the left
            pathInfo.HorizontalMovement = -pathInfo.Speed;
        }
        else if (dir.x > 0)
        {
            // Waypoint is to the right
            pathInfo.HorizontalMovement = pathInfo.Speed;
        }
        else
        {
            pathInfo.HorizontalMovement = 0;
        }

        pathInfo.Animator.SetFloat("Speed", Mathf.Abs(pathInfo.HorizontalMovement));

        pathInfo.Controller.Move(pathInfo.HorizontalMovement * Time.fixedDeltaTime, false, false);

        // Check if enemy is at the next waypoint
        float dist = Vector3.Distance(transform.position, Path.vectorPath[CurrentWaypoint]);
        if (dist <= pathInfo.NextWaypointDistance)
        {
            CurrentWaypoint++;
            return;
        }
    }
}
