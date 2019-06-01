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
    public GameObject player;

    // State Machine
    private AIState state;
    private Dictionary<string, AIState> states;
    public float playerPursuitThreshold; // Distance at which the enemy will begin to persue the player
    public float playerAttackThreshold; // Distance at which the enemy will initiate an attack on the player

    // Pathfinding
    private Seeker seeker;
    private Rigidbody2D rb;
    private CrabPathfindingInfo pathInfo;
    public CharacterController2D controller;
    public Transform PatrolTargetLeft;
    public Transform PatrolTargetRight;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyCrab>();

        // Pathfinding initilisation
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pathInfo = new CrabPathfindingInfo(controller, animator, seeker, rb, playerPursuitThreshold, playerAttackThreshold, player);

        // State machine initilisation
        states = new Dictionary<string, AIState>();

        states.Add("Idle", new CrabIdle(pathInfo, PatrolTargetLeft, PatrolTargetRight));
        states.Add("Pursuing", new CrabPursuing(pathInfo));
        // states.Add("Attacking", new CrabAttacking(pathInfo));
        // states.Add("PostAttack", new CrabPostAttack(pathInfo));

        foreach (KeyValuePair<string, AIState> s in states)
        {
            s.Value.InitialiseStartUpdatePath(StartUpdatePath);
            s.Value.InitialiseStopUpdatePath(StopUpdatePath);
            s.Value.InitialiseTransitionTo(TransitionTo);
        }

        //state = stateContainer.idle;
        state = states["Idle"];
        state.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    private void OnPathComplete(Path p)
    {
        if (p.error)
            return;

        state.Path = p;
        state.CurrentWaypoint = 0;
    }

    // Every few seconds or so find a new path to target
    private IEnumerator UpdatePath()
    {
        if (pathInfo.Target == null)
        {
            Debug.LogError("CrabAI::UpdatePath - There is no Target to pathfind to.");
            yield return false;
        }

        seeker.StartPath(transform.position, pathInfo.Target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / pathInfo.UpdateRate);

        StartCoroutine(UpdatePath());
    }

    public void StartUpdatePath()
    {
        StartCoroutine(UpdatePath());
    }

    public void StopUpdatePath()
    {
        StopCoroutine(UpdatePath());
    }

    private void TransitionTo(string newState)
    {
        if (!states.ContainsKey(newState))
        {
            Debug.LogError("CrabAI::TransitionTo - Provided key does not exist in the states dictionary.");
            return;
        }

        state.OnExit();
        state = states[newState];
        state.OnEnter();
    }
}
