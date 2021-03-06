﻿using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogHandler : MonoBehaviour
{

    TemplateXML fileInfo;
    XmlSerializer xmlDocument;

    int currentDialogNumber = 0;

    Dialog[] currentDialog;

    Button nextButton;

    public int sceneNumber = 1;
    public TextHandler textCanvas;
    public PortraitHandler portraitCanvas;
    public static string fileLocation = "Assets/Dialog/dialog.xml";


    void Awake()
    {
        FileStream file = new FileStream(DialogHandler.fileLocation, FileMode.Open);

        xmlDocument = new XmlSerializer(typeof(TemplateXML));
        fileInfo = (TemplateXML)xmlDocument.Deserialize(file);


        file.Close();

        nextButton = transform.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(NextLine);


        currentDialog = fileInfo.GetDialog(sceneNumber);

        textCanvas.Init();
        textCanvas.SetText(currentDialog[currentDialogNumber].line);
        textCanvas.SetCharName(0, currentDialog[currentDialogNumber].charName1);
        textCanvas.SetCharName(1, currentDialog[currentDialogNumber].charName2);


        portraitCanvas.Init();
        portraitCanvas.SetCharPortrait(0, currentDialog[currentDialogNumber].charName1);
        portraitCanvas.SetCharPortrait(1, currentDialog[currentDialogNumber].charName2);

    }


    public void NextLine()
    {

        if (currentDialogNumber < currentDialog.Length-1)
        {
            currentDialogNumber++;
        }
        else
        {

            //GoToNewScene()
            sceneNumber++;
            currentDialogNumber = 0;
        }

        textCanvas.SetText(currentDialog[currentDialogNumber].line);
        textCanvas.SetCharName(0, currentDialog[currentDialogNumber].charName1);
        textCanvas.SetCharName(1, currentDialog[currentDialogNumber].charName2);

        portraitCanvas.SetCharPortrait(0, currentDialog[currentDialogNumber].charName1);
        portraitCanvas.SetCharPortrait(1, currentDialog[currentDialogNumber].charName2);
        
    }
}
