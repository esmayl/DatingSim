using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEditor;
using System;

public class DialogSaver : EditorWindow
{

    int selectedChar1 = 0;
    int selectedChar2 = 0;
    int dialogNumber = 0;
    int sceneNumber = 0;

    int blocksOfText=1;
    string dialogText = "ghfghfgh";
    string blocksOfTextString = "1";
    string sceneNumberString = "0";
    string dialogNumberString = "0";

    bool saving = false;

    string[] allCharacters;

    string[] textFields;

    TemplateXML readData;
    XmlSerializer xmlDocument;
    string fileLocation = "Assets/Dialog/dialog.xml";

    [MenuItem("File/Create dialog")]
    static void Init()
    {
        DialogSaver window = (DialogSaver)EditorWindow.GetWindow(typeof(DialogSaver));
        window.Show();

        window.InitVariables();
    }

    void InitVariables()
    {
        int temp = sizeof(Characters);
        allCharacters = new string[sizeof(Characters)];
        textFields = new string[1];

        for (int i = 0; i < temp; i++)
        {
            allCharacters[i] = ((Characters)i).ToString();
        }

    }

    void OnGUI()
    {
        GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();

                GUILayout.Label("Character name 1: ");
                selectedChar1 = GUILayout.SelectionGrid(selectedChar1, allCharacters, 1);

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.Label("Character name 2: ");
            selectedChar2 = GUILayout.SelectionGrid(selectedChar2, allCharacters, 1);

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

                GUILayout.Label("Scene number: ");
                sceneNumberString = GUILayout.TextField(sceneNumberString);

                GUILayout.Label("Dialog number: ");
                dialogNumberString = GUILayout.TextField(dialogNumberString);

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            blocksOfTextString = GUILayout.TextField(blocksOfTextString);

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Save!"))
            {
                SaveDialog();
            }

            if (blocksOfTextString.Length > 0)
            {
                
                blocksOfText = int.Parse(blocksOfTextString);
                if(textFields.Length < blocksOfText)
                {
                    textFields = new string[blocksOfText];
                }
                if(textFields.Length> blocksOfText)
                {
                    textFields = new string[blocksOfText];
                }

                for (int i = 0; i < blocksOfText; i++)
                {
                    GUILayout.BeginHorizontal();
            
                        GUILayout.Label("Dialog text: ");

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                        textFields[i] = GUILayout.TextArea(textFields[i]);

                    GUILayout.EndHorizontal();
                }
            }

        GUILayout.EndVertical();
    }

    private void SaveDialog()
    {
        if (saving) { return; }

        saving = true;

        xmlDocument = new XmlSerializer(typeof(TemplateXML));
        FileStream file;
        TemplateXML readData = null;
        TemplateXML firstData = null;

        try
        {
            file = new FileStream(fileLocation, FileMode.Open); 

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);

            file = new FileStream(fileLocation, FileMode.OpenOrCreate);
        }

        if (sceneNumberString.Length > 0)
        {
            sceneNumber = int.Parse(sceneNumberString);
        }

        if(dialogNumberString.Length > 0)
        {
            dialogNumber = int.Parse(dialogNumberString);
        }

        try
        {
            readData = (TemplateXML)xmlDocument.Deserialize(file);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);

            readData = new TemplateXML();

            if (readData == null)
            {
                Debug.Log("Creating file");

                firstData = new TemplateXML();

                Dialog temp = new Dialog();
                temp.charName1 = (Characters)selectedChar1;
                temp.charName2 = (Characters)selectedChar2;
                temp.dialogNumber = 0;
                temp.pictureName1 = ((Characters)selectedChar1).ToString();
                temp.pictureName2 = ((Characters)selectedChar2).ToString();

                Debug.Log("Created dialog");

                if (textFields.Length > 0)
                {
                    temp.texts = new string[textFields.Length];
                    temp.texts = textFields;
                }
                else
                {
                    temp.texts = new string[] { "empty" };
                }

                firstData.Add(sceneNumber, temp);

                Debug.Log("Adding dialog");

            }

        }

        if (readData != null)
        {
            Dialog[] temp = null;

            try
            {
                temp = readData.GetDialog(sceneNumber);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            if (temp != null)
            {

                for (int i = 0; i < temp.Length; i++)
                {
                    
                    if (temp[i].charName1 == (Characters)selectedChar1 && temp[i].charName2 == (Characters)selectedChar2)
                    {

                        if (dialogNumber == temp[i].dialogNumber)
                        {

                            Debug.Log("Readdata : " + temp[i].texts[0]);

                            if (textFields.Length > 0)
                            {
                                temp[i].texts = new string[textFields.Length];
                                temp[i].texts = textFields;
                            }
                            break;
                        }
                        else
                        {
                            Dialog[] tempCopy = temp;
                            temp = new Dialog[tempCopy.Length + 1];

                            int p = 0;
                            foreach (Dialog d in tempCopy)
                            {
                                temp[p] = d;
                                p++;
                            }
                            Dialog newDialog = new Dialog();
                            newDialog.charName1 = ((Characters)selectedChar1);
                            newDialog.charName2 = ((Characters)selectedChar2);
                            newDialog.dialogNumber = dialogNumber;
                            newDialog.pictureName1 = ((Characters)selectedChar1).ToString();
                            newDialog.pictureName2 = ((Characters)selectedChar2).ToString();
                            newDialog.texts = textFields;

                            temp[p] = newDialog;

                        }
                    }
                }
            }
           


            if (temp == null)
            {
                Debug.Log("Creating new dialog");

                temp = new Dialog[]
                {
                    new Dialog()
                };


                temp[0].charName1 = (Characters)selectedChar1;
                temp[0].charName2 = (Characters)selectedChar2;
                temp[0].dialogNumber = 0;
                temp[0].pictureName1 = ((Characters)selectedChar1).ToString();
                temp[0].pictureName2 = ((Characters)selectedChar2).ToString();

                if (textFields.Length > 0)
                {
                    temp[0].texts = new string[textFields.Length];
                    temp[0].texts = textFields;
                }
            }

            Debug.Log(temp[0].charName1);


            file.SetLength(0);
            file.Flush();

            readData.Add(sceneNumber, temp);
        }
        else
        {
            Debug.Log("Cant use xmlreader");
        }

        Debug.Log("Saving data");

        if (firstData != null)
        {
            xmlDocument.Serialize(file, firstData);
        }
        else
        {
            xmlDocument.Serialize(file, readData);
        }
        file.Close();

        readData = null;
        firstData = null;

        saving = false;
    }

}


