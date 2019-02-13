using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    int damage = 25;
    public float perishTimer = 0f;
    float perishThreshold = 5f;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        perishTimer += 1 * Time.deltaTime;

        if (perishTimer > perishThreshold)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
            return;

        EnemyCrab enemy = collision.GetComponent<EnemyCrab>();
        if (enemy)
        {
            enemy.TakeDamage(25);
        }

        if (impactEffect)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
