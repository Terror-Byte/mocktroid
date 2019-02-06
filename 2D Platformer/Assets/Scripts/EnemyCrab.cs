using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : MonoBehaviour
{
    public int health = 100;
    public float attackDamage = 10f;
    public GameObject deathEffect; // prefab for death effect - spawn in place of self

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            player.TakeDamage(attackDamage);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerStats player = collision.collider.GetComponent<PlayerStats>();
            player.TakeDamage(attackDamage);
            player.PushBack(gameObject.transform.position);
            // Push player back a bit?
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Keep damaging the player if they're still touching the enemy
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStats player = collision.collider.GetComponent<PlayerStats>();
            // player.TakeDamage(attackDamage);
            // Push player back a bit?
            // StartCoroutine(DamageCooldown(player));
        }
    }

    /*IEnumerator DamageCooldown(PlayerStats player)
    {
        yield return new WaitForSeconds(2f);
        player.TakeDamage(attackDamage);
    }*/
}
