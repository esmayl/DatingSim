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
    Button nextButton;

    int currentLine = 0;

    FileStream file;
    XmlSerializer xmlDocument;
    TemplateXML readData;
    Dialog[] currentDialog;

    public void Init()
    {
        char1Name = transform.Find("Char1Name").GetComponent<Text>();
        char2Name = transform.Find("Char2Name").GetComponent<Text>();
        textfield = transform.Find("DialogueBox").GetComponent<Text>();

        nextButton = transform.Find("NextButton").GetComponent<Button>();


        FileStream file = new FileStream(DialogHandler.fileLocation, FileMode.Open);

        xmlDocument = new XmlSerializer(typeof(TemplateXML));
        readData = (TemplateXML)xmlDocument.Deserialize(file);

        file.Close();

        nextButton.onClick.AddListener(NextLine);
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

    public void NextLine()
    {
        //Debug.Log(currentDialog.texts[currentLine]);
        //
        //string textToShow = "No Text Found!";
        //
        //if (currentLine < currentDialog.texts.Length)
        //{
        //    textToShow = currentDialog.texts[currentLine];
        //}
        //else
        //{
        //    Debug.Log("End array!");
        //    currentLine = 0;
        //}
        //
        //if (currentLine < currentDialog.texts.Length - 1)
        //{
        //    currentLine++;
        //}
        //
        //
        //SetText(textToShow);
    }

    public void SetText(string textToSet)
    {

        if(currentDialog == null) { throw new System.Exception(); }

        //Maybe fancy text appear
        textfield.text = textToSet;

        if(char1Name.text != currentDialog[currentLine].charName1.ToString())
        {
            char1Name.text = currentDialog[currentLine].charName1.ToString();
        }
    }

    public void SetCurrentDialog(int currentSceneNumber)
    {
        currentDialog = readData.GetDialog(currentSceneNumber);
    }
    
}
