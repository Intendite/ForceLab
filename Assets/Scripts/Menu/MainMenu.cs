using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the first set of instructions when "Play Instructions 1" button is clicked
    public void PlayInstructions1()
    {
        SceneManager.LoadScene(1);
    }

    // Load the second set of instructions when "Play Instructions 2" button is clicked
    public void PlayInstructions2()
    {
        SceneManager.LoadScene(4);
    }

    // Quit the game when the "Quit" button is clicked
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
