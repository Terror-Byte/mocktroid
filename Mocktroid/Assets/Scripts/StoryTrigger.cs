using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public int TriggerIndex;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerEnter()
    {
        if (gameController == null)
        {
            Debug.LogError("Game Controller instance is null.");
            return;
        }

        if (gameController.StoryIndex == TriggerIndex)
        {
            // The story is at the correct point for this trigger, run story and disable this gameObject.
            gameController.AdvanceStory();
            gameObject.SetActive(false);
        }
    }
}
