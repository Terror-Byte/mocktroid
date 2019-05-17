using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPursuing : AIState
{
    public CrabPathfindingInfo PathInfo { get; private set; }
    private GameObject player;

    //private delegate void StartPathDelegate();
    //private delegate void StartUpdatePathDelegate();
    //private delegate void StopUpdatePathDelegate();
    //private StartPathDelegate StartPath;
    //private StartUpdatePathDelegate StartUpdatePath;
    //private StopUpdatePathDelegate StopUpdatePath;
    //private Path path;
    //private bool pathIsEnded = false;
    //private int currentWaypoint = 0; // Waypoint we are currently moving towards

    System.Action StartUpdatePath;
    System.Action StopUpdatePath;

    public CrabPursuing(CrabPathfindingInfo pathInfo)
    {
        this.PathInfo = pathInfo;
    }

    public CrabPursuing(CrabPathfindingInfo pathInfo, GameObject player)
    {
        this.PathInfo = PathInfo;
        this.player = player;
    }

    public void InitialiseStartUpdatePath(System.Action func)
    {
        StartUpdatePath = func;
    }

    public void InitialiseStopUpdatePath(System.Action func)
    {
        StopUpdatePath = func;
    }

    public override void OnEnter()
    {
        // Begin pathfinding to player
        // PathInfo.Seeker.StartPath(PathInfo.Rb.transform.position, PathInfo.Target.position, OnPathComplete);
        // StartCoroutine(UpdatePath());

        // Set target to the player
        PathInfo.Target = player.transform;
        if (StartUpdatePath == null)
        {
            Debug.LogError("CrabPursuing::OnEnter - StartUpdatePath delegate has not been assigned a function.");
            return;
        }
        StartUpdatePath();
    }

    public override void OnExit()
    {
        // Clear PathfindingInfo stuff? CurrentWaypoint, pathhasended, path, etc
        // Stop UpdatePath coroutine
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
        // TODO: Add crab pursuit logic
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
            return;
        }
        pathIsEnded = false;

        Transform transform = PathInfo.Rb.transform;

        // Direction to the next waypoint
        Vector3 dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;

        if (CurrentWaypoint == 0)
        {
            if (PathInfo.HorizontalMovement != 0)
            {
                // We're already moving

                Vector3 nextDir = (Path.vectorPath[CurrentWaypoint + 1] - transform.position).normalized;

                // If we are already moving LEFT, current waypoint is to the RIGHT but the next waypoint is LEFT
                if (PathInfo.HorizontalMovement == -PathInfo.Speed && dir.x > 0 && nextDir.x < 0)
                {
                    CurrentWaypoint++;
                    dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;
                }
                // If we are already moving RIGHT, currently waypoint is to the LEFT but the next waypoint is RIGHT.
                else if (PathInfo.HorizontalMovement == PathInfo.Speed && dir.x < 0 && nextDir.x > 0)
                {
                    CurrentWaypoint++;
                    dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;
                }
            }
        }

        Debug.Log(dir);

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
