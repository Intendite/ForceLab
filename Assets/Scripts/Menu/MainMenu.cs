using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playInstructions1()
    {
        SceneManager.LoadScene(1);
    }

    public void playInstructions2()
    {
        SceneManager.LoadScene(4);
    }

    public void quitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
