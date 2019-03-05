﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public const float MaxHealth = 100f;
    public float Health { get; private set; }
    public int Points { get; private set; } = 0;
    public List<EnemyCrab> enemiesCollidingWith; // TODO: Replace with generic enemy type
    public GameObject deathEffect;
    public Renderer playerRenderer;

    // Invincibility Timer Stuff
    [SerializeField]
    private bool isInvincible = false;
    private float invincibilityTime = 1f;
    
    //private float invincibilityTimer = 0f;
    //private float invincibilityTimerTick = 1f;
    //private float invincibilityTimerThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        enemiesCollidingWith = new List<EnemyCrab>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (invincibilityTimerOn)
        {
            invincibilityTimer += invincibilityTimerTick * Time.deltaTime;

            // StartCoroutine("DamageSpriteFlash");

            if (invincibilityTimer > invincibilityTimerThreshold)
            {
                DisableInvincibilityTimer();
            }
        }*/

        // Check if the player is colliding with any enemies. If so, check if the damage cooldown is active. If so, do nothing. Else, damage the player.
        if (enemiesCollidingWith.Count > 0 && !isInvincible)
        {
            TakeDamage(enemiesCollidingWith[0].AttackDamage);
        }
    }

    public void AddPoints(int points)
    {
        this.Points += points;
    }

    public void AddHealth(float health)
    {
        this.Health += health;

        if (health > MaxHealth)
            health = MaxHealth;
    }

    private void TakeDamage(float damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }

        EnableInvincibility();

        // TODO: Pushback, flash effect
    }

    public void Die()
    {
        Debug.Log("Oh no you died."); // REMOVE THIS
        if (deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // TODO: Decouple stats from player game object so the game doesn't go wonky when you die?
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyCrab enemy = collision.gameObject.GetComponent<EnemyCrab>();

        if (enemy && !enemiesCollidingWith.Contains(enemy))
        {
            enemiesCollidingWith.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyCrab enemy = collision.gameObject.GetComponent<EnemyCrab>();

        if (enemy && enemiesCollidingWith.Contains(enemy))
        {
            enemiesCollidingWith.Remove(enemy);
        }
    }

    private void EnableInvincibility()
    {
        isInvincible = true;
        StartCoroutine("InvincibilityTimer");
        StartCoroutine("DamageSpriteFlash");
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
        StopCoroutine("DamageSpriteFlash");
        playerRenderer.material.color = Color.white;
    }

    IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSecondsRealtime(invincibilityTime);
        DisableInvincibility();
    }

    IEnumerator DamageSpriteFlash()
    {
        for (; ; )
        {
            if (!playerRenderer)
                yield return null;

            // Turn sprite off
            playerRenderer.material.color = Color.clear;
            yield return new WaitForSeconds(0.1f);

            // Turn sprite on
            playerRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /*public void PushBack(Vector3 pushBackOrigin)
    {
        float pushForce = 2f;
        Vector2 pushDir = gameObject.transform.position - pushBackOrigin;
        //Vector2 pushDir = (pushBackOrigin - gameObject.transform.position).normalized;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(pushDir);
    }*/
}
