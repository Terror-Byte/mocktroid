using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    private void RemoveEnemyFromList(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy)
            enemy.removeFromGameControllerList -= (obj) => { RemoveEnemyFromList(obj); };

        enemies.Remove(enemyObj);
    }
}
