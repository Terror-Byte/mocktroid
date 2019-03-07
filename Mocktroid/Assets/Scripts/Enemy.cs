using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Guid EnemyID { get; private set; } // TODO: PUT IN A ENEMY SUPERCLASS
    protected int health = 100;
    public float AttackDamage { get; private set; } = 10f;
    public GameObject deathEffect; // prefab for death effect - spawn in place of self
    public System.Action<GameObject> removeFromGameControllerList;

    // Start is called before the first frame update
    void Start()
    {
        EnemyID = System.Guid.NewGuid();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        
        removeFromGameControllerList(gameObject);

        Destroy(gameObject);
    }
}
