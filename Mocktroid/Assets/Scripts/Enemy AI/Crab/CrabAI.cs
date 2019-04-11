using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyCrab>();

        if (enemy == null)
        {
            Debug.LogError("CrabAI - GameObject does not have EnemyCrab script attached.");
        }

        idle = new CrabIdle();
        pursuing = new CrabPursuing();
        attacking = new CrabAttacking();
        postAttack = new CrabPostAttack();
        state = idle;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();

        // If player is within pursuit threshold - transition to pursuing
        // If player is within attack threshold - transition to attack
    }
}
