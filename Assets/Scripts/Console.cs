using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void log(string message)
    {
        text.text += "\n" + message;
    }

    public void DetectPointerEnter()
    {
        log("Pointer entered");
    }
}
