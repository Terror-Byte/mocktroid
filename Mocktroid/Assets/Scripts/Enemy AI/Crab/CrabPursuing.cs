using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPursuing : AIState
{
    private CrabPathfindingInfo pathInfo;
    // private GameObject player;

    public CrabPursuing(CrabPathfindingInfo pathInfo)
    {
        this.pathInfo = pathInfo;
    }

    //public CrabPursuing(CrabPathfindingInfo pathInfo, GameObject player)
    //{
    //    this.PathInfo = pathInfo;
    //    this.player = player;
    //}

    public override void OnEnter()
    {
        Debug.Log("Entering Pursuing State.");

        // Set target to the player
        pathInfo.Target = pathInfo.Player.transform;
        if (StartUpdatePath == null)
        {
            Debug.LogError("CrabPursuing::OnEnter - StartUpdatePath delegate has not been assigned a function.");
            return;
        }
        StartUpdatePath();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Pursuing State.");
        
        Path = null;
        CurrentWaypoint = 0;
        pathIsEnded = false;

        if (StopUpdatePath == null)
        {
            Debug.LogError("CrabPursuin::OnExit - StopUpdatePath delegate has not been assigned a function.");
            return;
        }
        StopUpdatePath();
    }

    public override void Update()
    {
        float distToPlayer = (pathInfo.Player.transform.position - pathInfo.Rb.transform.position).magnitude;

        if (distToPlayer > pathInfo.PlayerPursuitThreshold)
        {
            // Player is outside of the pursuit range, transition to idle state
            TransitionTo("Idle");
            return;
        }

        /*if (distToPlayer < pathInfo.PlayerAttackThreshold)
        {
            // Player is within attack range, transition to attack state
            TransitionTo("Attacking");
            return;
        }*/
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

            //Debug.Log("End of path reached");
            pathIsEnded = true;
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
