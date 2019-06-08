using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen = false;
    bool playerIsInside = false;
    private Collider2D doorCollider;
    public Animator animator;
    private float openTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: If player is within the door, do not allow it to try and close. Add a second end or allow pairing of two doors to act as one.
    }

    public void Open()
    {
        if (isOpen)
            return;

        isOpen = true;
        doorCollider.enabled = false;
        animator.SetTrigger("Open");
        StartCoroutine("OpenTimer");
        Debug.Log("Door Opening");
    }

    public void Close()
    {
        isOpen = false;
        doorCollider.enabled = true;
        animator.SetTrigger("Close");
        Debug.Log("Door Closing");
    }

    IEnumerator OpenTimer()
    {
        yield return new WaitForSecondsRealtime(openTime);
        Close();
    }

    public void OnPlayerEnter()
    {
        // Stop the door from closing when the player is inside
        if (isOpen && !playerIsInside)
        {
            Debug.Log("Player has entered door.");
            StopCoroutine("OpenTimer");
            playerIsInside = true;
        }
    }

    public void OnPlayerExit()
    {
        // Begin the door close timer
        if (isOpen && playerIsInside)
        {
            StartCoroutine("OpenTimer");
            Debug.Log("Player has exited door.");
            playerIsInside = false;
        }
    }
}
