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

    // State Machine
    private AIState state;
    private CrabIdle idle;
    private CrabPursuing pursuing;
    private CrabAttacking attacking;
    private CrabPostAttack postAttack;

    // Pathfinding
    // TODO: Re-organise code into states if needed
    public Transform target; // Target to pathfind to
    public float updateRate = 5f; // How often the pathfinder refreshes the path to the player (only use in pursuit mode)
    private Seeker seeker;
    private Rigidbody2D rb;
    public Path path;
    private float speed = 20f; // Move speed
    // public ForceMode2D fMode; // Determines how force is applied to ridigbody (whether force or impulse)
    private bool pathIsEnded = false; // TODO: Make public in future? [HideInInspector]
    private float nextWaypointDistance = 1; // How close the enemy needs to get to a waypoint for it to consider itself "at" that waypoint
    public int currentWaypoint = 0; // Waypoint we are current;y moving towards

    public CharacterController2D controller;
    float horizontalMovement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyCrab>();

        // State machine initilisation
        idle = new CrabIdle();
        pursuing = new CrabPursuing();
        attacking = new CrabAttacking();
        postAttack = new CrabPostAttack();
        state = idle;

        // Pathfinding initilisation
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        // fMode = ForceMode2D.Force;

        if (target == null)
            Debug.Log("No Target Set");

        // Start a new path to the target position and return the result to the OnPathComplete function
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
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
        // Put state physics calculations in FixedUpdate functions of their own ?

        if (target == null)
            return;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // Direction to the next waypoint
        //Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        //dir *= speed * Time.fixedDeltaTime;

        //// Move to waypoint
        //rb.AddForce(dir);

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        if (dir.x < 0)
        {
            // Waypoint is to the left
            horizontalMovement = -speed;
        }
        else if (dir.x > 0)
        {
            // Waypoint is to the right
            horizontalMovement = speed;
        }
        else
        {
            horizontalMovement = 0;
        }

        Debug.Log(horizontalMovement);

        controller.Move(horizontalMovement * Time.fixedDeltaTime, false, false);

        //Move(horizontalMovement * Time.fixedDeltaTime);

        // Check if enemy is at the next waypoint
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist <= nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private void OnPathComplete(Path p)
    {
        // Debug.Log("Path Error: " + p.error);

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Every few seconds or so find a new path to target
    private IEnumerator UpdatePath()
    {
        if (target == null)
        {
            // TODO: Insert player search here (THIS IS JUST A BRACKEYS THING. PROBS WON'T NEED TO WORRY ABOUT THIS)
            yield return false;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);

        StartCoroutine(UpdatePath());
    }

    private void Move(float move)
    {
        float movementSmoothing = .0f;
        Vector3 velocity = Vector3.zero;

        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        // rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        // rb.velocity = targetVelocity;

        transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
    }
}
