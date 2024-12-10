using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class virtualKeyboard : MonoBehaviour
{
    //string to hold the text written
    string writtenText;
    //attache any Text Mesh Pro Text here to view it
    public TMP_Text viewText;


    public void  KeySelected(string keyPressed)
    {
        writtenText += keyPressed;
        viewText.text = writtenText;
        Debug.Log(writtenText);
    }
}
