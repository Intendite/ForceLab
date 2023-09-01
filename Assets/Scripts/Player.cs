using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public XPData xpData;
    public GameManager gameManager;

    private float timeElapsed = 0f;

    private void Awake()
    {
        Debug.Log("Player Awake called in scene " + SceneManager.GetActiveScene().name);
        Debug.Log("XPData assigned: " + (xpData != null));

        // You don't need to check for duplicate players, as the GameManager handles it.

        // Assign the GameManager instance in the Inspector to the gameManager field.
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainXP(1);
        }

        // Update the time elapsed
        timeElapsed += Time.deltaTime;

        // Check to see if the Scene is in the Playground Scene
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Check if 5 seconds have passed
            if (timeElapsed >= 5f)
            {
                // Increment XP by 1
                GainXP(1);

                // Reset timeElapsed for the next interval
                timeElapsed -= 5f;
            }
        }

        if (xpData.currentXP >= 10)
        {
            LevelUp();
        }
    }

    public void GainXP(int XP)
    {
        xpData.currentXP += XP;

        // Update XP Bar through the GameManager
        gameManager.GainXP(XP);
    }

    void LevelUp()
    {
        xpData.currentXP = 0;
        xpData.currentLevel += 1;
    }
}
