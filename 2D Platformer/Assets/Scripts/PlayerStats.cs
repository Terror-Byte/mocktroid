using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public const float MaxHealth = 100f;
    float health;
    int points = 0;

    // List<EnemyCrab> enemiesCollidingWith; // TODO: Replace with generic enemy type

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        // enemiesCollidingWith = new List<EnemyCrab>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void AddHealth(float health)
    {
        this.health += health;

        if (health > MaxHealth)
            health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Oh no you died.");
    }

    public void PushBack(Vector3 pushBackOrigin)
    {
        float pushForce = 2f;
        Vector2 pushDir = gameObject.transform.position - pushBackOrigin;
        //Vector2 pushDir = (pushBackOrigin - gameObject.transform.position).normalized;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(pushDir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public float Health
    {
        get
        {
            return health;
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
    }

    /*public List<EnemyCrab> EnemiesCollidingWith
    {
        get
        {
            return EnemiesCollidingWith;
        }

        set
        {
            EnemiesCollidingWith = value;
        }
    }*/
}
