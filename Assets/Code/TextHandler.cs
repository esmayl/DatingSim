using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{

	Text char1Name;
	Text char2Name;
	Text textfield;


    public void Init()
    {
        char1Name = transform.Find("Char1Name").GetComponent<Text>();
        char2Name = transform.Find("Char2Name").GetComponent<Text>();
        textfield = transform.Find("DialogueBox").GetComponent<Text>();
    }

    public void SetCharName(int charNumber,Characters charName)
    {
        if(charNumber == 0)
        {
            char1Name.text = charName.ToString();
        }
        else
        {
            char2Name.text = charName.ToString();
        }
    }

    public void SetText(string textToSet)
    {

        if(textToSet == null) { throw new System.Exception(); }

        //Maybe fancy text appear
        textfield.text = textToSet;
    }

}
