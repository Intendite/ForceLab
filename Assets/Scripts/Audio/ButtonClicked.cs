using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    // This method is called when the button is clicked
    public void PlayButtonClickSound()
    {
        // Find the AudioManager in the scene and play the "ButtonClicked" sound
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
    }
}
