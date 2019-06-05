using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePointRunning;
    public Transform firePointStill;
    public GameObject bulletPrefab;
    public Animator animator;
    /*[SerializeField]
    private float firePointRunningY;*/

    // Update is called once per frame
    void Update()
    {     
        if (Input.GetButtonDown("Fire1"))
        {
            // TODO: Shooting cooldown so player can't spam
            animator.SetTrigger("IsShooting");
            Shoot();
        }
    }

    void Shoot()
    {
        Transform firePoint;// = firePointRunning;
        float playerSpeed = animator.GetFloat("Speed");

        if (playerSpeed > 0)
            firePoint = firePointRunning;
        else
            firePoint = firePointStill;

        Instantiate(bulletPrefab, firePoint.position, firePointRunning.rotation);
        AudioManager.instance.Play("PlayerShot");
        // StartCoroutine(ShootDelay());
        // animator.SetBool("IsShooting", false);
    }

    //IEnumerator ShootDelay()
    //{
    //    yield return new WaitForSeconds(0.25f);
    //    animator.SetBool("IsShooting", false);
    //}
}
