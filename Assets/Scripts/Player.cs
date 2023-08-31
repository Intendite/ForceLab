using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public XPData xpData;
    public XPBar xpBar;

    private float timeElapsed = 0f;

    private void Awake()
    {
        Debug.Log("Player Awake called in scene " + SceneManager.GetActiveScene().name);
        Debug.Log("XPData assigned: " + (xpData != null));

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Retain the object across scenes
        }
        else
        {
            Debug.Log("Destroying duplicate Player object in scene " + SceneManager.GetActiveScene().name);
            Destroy(gameObject); // Destroy duplicates
        }
    }


    // Update is called once per frame
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

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //if (momentsQuiz.)
        }

        if (xpData.currentXP >= 10)
        {
            LevelUp();
        }
    }

    public void GainXP(int XP)
    {
        Debug.Log("Gaining XP: " + XP);
        xpData.currentXP += XP;
        xpBar.SetXP(xpData.currentXP);
    }

    void LevelUp()
    {
        Debug.Log("Leveling up");
        xpData.currentXP = 0;
        xpData.currentLevel += 1;
    }

}
