using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPursuing : AIState
{
    public CrabPathfindingInfo PathInfo { get; private set; }
    private Path path;
    private bool pathIsEnded = false;
    private int currentWaypoint = 0; // Waypoint we are currently moving towards

    public CrabPursuing(CrabPathfindingInfo pathInfo)
    {
        this.PathInfo = pathInfo;
    }

    public override void OnEnter()
    {
        // Begin pathfinding to player
    }

    public override void OnExit()
    {
        // Clear PathfindingInfo stuff? CurrentWaypoint, pathhasended, path, etc
        // Stop UpdatePath coroutine
    }

    public override void Update()
    {
        // TODO: Add crab pursuit logic
    }

    public override void FixedUpdate()
    {
        //if (target == null)
        //    return;

        //if (path == null)
        //    return;

        //if (currentWaypoint >= path.vectorPath.Count)
        //{
        //    if (pathIsEnded)
        //        return;

        //    Debug.Log("End of path reached");
        //    pathIsEnded = true;
        //    return;
        //}
        //pathIsEnded = false;

        //// Direction to the next waypoint
        //Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        //if (currentWaypoint == 0)
        //{
        //    if (horizontalMovement != 0)
        //    {
        //        // We're already moving

        //        Vector3 nextDir = (path.vectorPath[currentWaypoint + 1] - transform.position).normalized;

        //        // If we are already moving LEFT, current waypoint is to the RIGHT but the next waypoint is LEFT
        //        if (horizontalMovement == -speed && dir.x > 0 && nextDir.x < 0)
        //        {
        //            currentWaypoint++;
        //            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        //        }
        //        // If we are already moving RIGHT, currently waypoint is to the LEFT but the next waypoint is RIGHT.
        //        else if (horizontalMovement == speed && dir.x < 0 && nextDir.x > 0)
        //        {
        //            currentWaypoint++;
        //            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        //        }
        //    }
        //}

        //Debug.Log(dir);

        //if (dir.x < 0)
        //{
        //    // Waypoint is to the left
        //    horizontalMovement = -speed;
        //}
        //else if (dir.x > 0)
        //{
        //    // Waypoint is to the right
        //    horizontalMovement = speed;
        //}
        //else
        //{
        //    horizontalMovement = 0;
        //}

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        //controller.Move(horizontalMovement * Time.fixedDeltaTime, false, false);

        //// Check if enemy is at the next waypoint
        //float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        //if (dist <= nextWaypointDistance)
        //{
        //    currentWaypoint++;
        //    return;
        //}
    }

    //private void OnPathComplete(Path p)
    //{
    //    // Debug.Log("Path Error: " + p.error);
    //    if (p.error)
    //        return;

    //    path = p;
    //    currentWaypoint = 0;
    //}

    // Every few seconds or so find a new path to target
    //private IEnumerator UpdatePath()
    //{
    //    if (target == null)
    //    {
    //        // TODO: Insert player search here (THIS IS JUST A BRACKEYS THING. PROBS WON'T NEED TO WORRY ABOUT THIS)
    //        yield return false;
    //    }

    //    pathInfo.Seeker.StartPath(pathInfo.Rb.transform.position, target.position, OnPathComplete);

    //    yield return new WaitForSeconds(1f / pathInfo.UpdateRate);

    //    StartCoroutine(UpdatePath());
    //}
}
