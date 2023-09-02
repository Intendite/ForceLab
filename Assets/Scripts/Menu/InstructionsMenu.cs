using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public void playSim1()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(2);
    }
}
