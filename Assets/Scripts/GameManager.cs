using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public XPData xpData;
    public Slider sliderXP;
    public TMPro.TMP_Text levelText;

    private float timeElapsed = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // You can find the AudioManager here or assign it in the Inspector
        // FindObjectOfType<AudioManager>().Play("Theme");
    }

    private void Start()
    {
        // Example: Play a sound when the game starts
        // AudioManager.Instance.Play("GameStart");
    }

    void Update()
    {
        /*// Input handling
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainXP(1);
        }*/

        // Update XP Bar
        sliderXP.value = xpData.currentXP;
        levelText.text = xpData.currentLevel.ToString();

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

        // Check to see if the Scene is in the Playground Scene
        if (SceneManager.GetActiveScene().buildIndex == 5)
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
    }

    public void LevelUp()
    {
        xpData.currentXP = 0;
        xpData.currentLevel += 1;
        FindObjectOfType<AudioManager>().Play("LevelUp");
    }
}
