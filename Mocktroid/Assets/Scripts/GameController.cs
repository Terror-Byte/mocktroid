using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;
    public CameraController cameraController;

    // Story system
    public int StoryIndex { get; private set; } = 0;
    List<string> storyStuff;


    // Start is called before the first frame update
    void Start()
    {
        storyStuff = new List<string>();
        AddStory();

        if (player)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            if (cameraController)
            {
                if (playerMovement)
                    cameraController.SetTarget(player.transform, playerMovement.CameraOffset);
                else
                    cameraController.SetTarget(player.transform);
            }
        }

        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy")); // TODO: When an enemy dies, send a callback to remove it from the list? Whenever a new enemy is spawned, add to list.

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();

            if (!enemy)
                continue;

            enemy.removeFromGameControllerList += (obj) => { RemoveEnemyFromList(obj); };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            cameraController.SetTarget(player.transform, playerMovement.CameraOffset);
        }

        if (Input.GetKeyDown(KeyCode.V) && enemies.Count > 0)
            cameraController.SetTarget(enemies[0].transform);
    }

    private void RemoveEnemyFromList(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy)
            enemy.removeFromGameControllerList -= (obj) => { RemoveEnemyFromList(obj); };

        enemies.Remove(enemyObj);
    }

    private void AddStory()
    {
        if (storyStuff == null)
            return;

        storyStuff.Add("Greetings, adventurer! Your job is to acquire the macguffin! Fail, and your punishment will be worse than death. Good luck.");
        storyStuff.Add("This thing here is an enemy crab. Don't let him see you or he'll chase you to the ends of the earth.");
    }

    public bool AdvanceStory()
    {
        if (StoryIndex >= storyStuff.Count)
        {
            // Something's gone wrong and we're trying to access an element in storyStuff that isn't there.
            return false;
        }

        Debug.Log(storyStuff[StoryIndex]);
        StoryIndex++;

        return true;
    }
}
