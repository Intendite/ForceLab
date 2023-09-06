using TMPro;
using UnityEngine;

public class Console : MonoBehaviour
{
    public TMP_Text text;

    public void Log(string message)
    {
        text.text += "\n" + message;
    }

    public void DetectPointerEnter()
    {
        Log("Pointer entered");
    }
}
