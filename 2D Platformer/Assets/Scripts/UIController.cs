using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerStats player;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI pointsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (healthText)
            {
                healthText.text = "Health: " + player.Health;
            }

            if (pointsText)
            {
                pointsText.text = "Points: " + player.Points;
            }
        }
    }
}
