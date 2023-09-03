using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject XPBarUI;
    public GameObject NotesMenuUI1;
    public GameObject NotesMenuUI2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (GameIsPaused)
            {
                StopNotes();
            }
            else
            {
                LoadNotes();
            }
        }


        if (SceneManager.GetActiveScene().buildIndex == 2 && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(3);
            Cursor.visible = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 5 && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(6);
            Cursor.visible = true;
        }
    }

    public void Resume()
    {
        XPBarUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        XPBarUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void StopNotes()
    {
        XPBarUI.SetActive(true);
        NotesMenuUI1.SetActive(false);
        NotesMenuUI2.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    void LoadNotes()
    {
        XPBarUI.SetActive(false);
        NotesMenuUI1.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING GAME");
        Application.Quit();
    }
}