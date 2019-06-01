using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CrabPathfindingInfo
{
    public CharacterController2D Controller { get; private set; }
    public Animator Animator { get; private set; }
    public Transform Target; // Target to pathfind to
    public float UpdateRate { get; private set; } = 5f; // How often the pathfinder refreshes the path to the player (only use in pursuit mode)
    public Seeker Seeker { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    // public Path path;
    public float Speed { get; private set; } = 30f; // Move speed
    // public bool pathIsEnded = false; // TODO: Make public in future? [HideInInspector]
    public float NextWaypointDistance { get; private set; } = 1; // How close the enemy needs to get to a waypoint for it to consider itself "at" that waypoint
    // public int currentWaypoint = 0; // Waypoint we are currently moving towards
    public float HorizontalMovement { get; set; } = 0f;
    public float PlayerPursuitThreshold { get; set; }
    public float PlayerAttackThreshold { get; set; }
    public GameObject Player { get; private set; }

    public CrabPathfindingInfo(CharacterController2D controller, Animator animator, Seeker seeker, Rigidbody2D rb, float playerPursuitThreshold, float playerAttackThreshold, GameObject player)
    {
        Controller = controller;
        Animator = animator;
        Seeker = seeker;
        Rb = rb;
        PlayerPursuitThreshold = playerPursuitThreshold;
        PlayerAttackThreshold = playerAttackThreshold;
        Player = player;
    }
}
