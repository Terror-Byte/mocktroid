using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(EnemyCrab))]
public class CrabAI : MonoBehaviour
{
    private EnemyCrab enemy;
    public Animator animator;

    // State Machine
    private AIState state;
    private CrabIdle idle;
    private CrabPursuing pursuing;
    private CrabAttacking attacking;
    private CrabPostAttack postAttack;

    // Pathfinding
    // TODO: Re-organise code into states if needed
    //public Transform target; // Target to pathfind to
    //public float updateRate = 10f; // How often the pathfinder refreshes the path to the player (only use in pursuit mode)
    private Seeker seeker;
    private Rigidbody2D rb;
    private CrabPathfindingInfo pathInfo;
    //public Path path;
    //private float speed = 30f; // Move speed
    //private bool pathIsEnded = false; // TODO: Make public in future? [HideInInspector]
    //private float nextWaypointDistance = 1; // How close the enemy needs to get to a waypoint for it to consider itself "at" that waypoint
    //public int currentWaypoint = 0; // Waypoint we are currently moving towards
    //float horizontalMovement = 0f;

    public CharacterController2D controller;
    

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyCrab>();

        // Pathfinding initilisation
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pathInfo = new CrabPathfindingInfo(controller, animator, seeker, rb);

        // State machine initilisation
        idle = new CrabIdle(pathInfo);
        pursuing = new CrabPursuing(pathInfo);
        attacking = new CrabAttacking(pathInfo);
        postAttack = new CrabPostAttack(pathInfo);
        state = idle;
        idle.OnEnter();

        // Start a new path to the target position and return the result to the OnPathComplete function
        // seeker.StartPath(transform.position, target.position, OnPathComplete);

        // StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
        // If player is within pursuit threshold - transition to pursuing
        // If player is within attack threshold - transition to attack
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();

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

    //    seeker.StartPath(transform.position, target.position, OnPathComplete);

    //    yield return new WaitForSeconds(1f / updateRate);

    //    StartCoroutine(UpdatePath());
    //}

    //private void Move(float move)
    //{
    //    float movementSmoothing = .0f;
    //    Vector3 velocity = Vector3.zero;

    //    Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
    //    // And then smoothing it out and applying it to the character
    //    // rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
    //    // rb.velocity = targetVelocity;

    //    transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
    //}
}
