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

        //parsing string 
        if (sceneNumberString.Length > 0)
        {
            sceneNumber = int.Parse(sceneNumberString);
        }

        if (dialogNumberString.Length > 0)
        {
            dialogNumber = int.Parse(dialogNumberString);
        }


        //try to load existing data
        try
        {
            readData = (TemplateXML)xmlDocument.Deserialize(file);
        }
        catch (Exception e)
        {
            //load failed , create new file
            Debug.Log(e.Message);

            firstData = new TemplateXML();

            for (int i = 0; i < textFields.Length; i++)
            {
                Dialog temp = CreateNewDialog(selectedChar1, selectedChar2, i, textFields[i]);

                firstData.Add(sceneNumber, temp);

            }

            xmlDocument.Serialize(file, firstData);

            file.Close();

            return;
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

                temp = new Dialog[textFields.Length];
                int i = 0;

                foreach (string item in textFields)
                {
                    temp[i] = CreateNewDialog(selectedChar1, selectedChar2, i, textFields[i]);
                    readData.Add(sceneNumber, temp);
                }

                xmlDocument.Serialize(file, readData);
                file.Close();
                return;
            }

            int p = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].dialogNumber == dialogNumber)
                {
                    temp[i] = CreateNewDialog(selectedChar1, selectedChar2, dialogNumber, textFields[0]);
                }
            }
            
            //replace exisiting dialog and save it

        }

        Debug.Log("Saving data");

        //if (firstData != null)
        //{
        //    xmlDocument.Serialize(file, firstData);
        //}
        //else
        //{
        //    xmlDocument.Serialize(file, readData);
        //}
        //file.Close();
        //
        //readData = null;
        //firstData = null;
        //
        saving = false;
    }

    private Dialog CreateNewDialog(int char1, int char2, int dialogNr,string line)
    {
        Dialog newDialog = new Dialog
        {
            charName1 = (Characters)char1,
            charName2 = (Characters)char2,
            dialogNumber = dialogNr,
            pictureName1 = ((Characters)char1).ToString(),
            pictureName2 = ((Characters)char2).ToString(),
            line = line
        };
        return newDialog;
    }
}


