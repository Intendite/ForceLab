using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    public void PlayButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
    }
}
