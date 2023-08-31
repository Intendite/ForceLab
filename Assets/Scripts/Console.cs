using UnityEngine;

public class Console : MonoBehaviour
{
    public TMPro.TMP_Text text;

    public void log(string message)
    {
        text.text += "\n" + message;
    }

    public void DetectPointerEnter()
    {
        log("Pointer entered");
    }
}
