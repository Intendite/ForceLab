using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    // Public methods to load different scenes
    public void PlaySim1()
    {
        HideCursor();
        LoadScene(2);
    }

    public void PlaySim2()
    {
        HideCursor();
        LoadScene(5);
    }

    // Private method to load scenes
    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Private method to hide the cursor
    private void HideCursor()
    {
        Cursor.visible = false;
    }
}
