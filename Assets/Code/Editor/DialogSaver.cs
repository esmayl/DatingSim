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

                GUILayout.Label("Character name: ");
                selectedChar1 = GUILayout.SelectionGrid(selectedChar1, allCharacters, 1);

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

                GUILayout.Label("Scene number: ");
                sceneNumberString = GUILayout.TextField(sceneNumberString);

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
            Dialog temp = null;

            try
            {
                temp = readData.GetDialog(sceneNumber, (Characters)selectedChar1);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            if (temp != null)
            {
                Debug.Log("Readdata : " + temp.texts[0]);

                temp.charName = (Characters)selectedChar1;
                temp.dialogNumber++;
                temp.pictureName1 = ((Characters)selectedChar1).ToString();

                if (textFields.Length > 0)
                {
                    temp.texts = new string[textFields.Length];
                    temp.texts = textFields;
                }
            }
           


            if (temp == null)
            {
                Debug.Log("Creating new dialog");

                temp = new Dialog();
                temp.charName = (Characters)selectedChar1;
                temp.dialogNumber = 0;
                temp.pictureName1 = ((Characters)selectedChar1).ToString();

                if (textFields.Length > 0)
                {
                    temp.texts = new string[textFields.Length];
                    temp.texts = textFields;
                }
            }

            Debug.Log(temp.charName);


            Debug.Log(sceneNumber);


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


