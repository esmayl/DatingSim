using System.Xml.Serialization;
using UnityEngine;

[XmlRoot]
public class TemplateXML
{

    [XmlArray("Scenes")]
    public Scene[] scenes;

    public void Add(int newSceneNumber , Dialog[] dialogsToAdd)
    {
        if(scenes == null)
        {
            scenes = new Scene[]
            {
                new Scene(newSceneNumber,dialogsToAdd)
            };

            return;
        }

        foreach (Scene s in scenes)
        {

            if (s != null)
            {
                if(s.sceneNumber == newSceneNumber)
                {
                    s.dialogs = dialogsToAdd;
                    break;
                }



            }        
        }

    }

    public void Add(int newSceneNumber , Dialog dialogToAdd)
    {
        if (scenes == null)
        {
            scenes = new Scene[] 
            {
                new Scene(newSceneNumber, new Dialog[] { dialogToAdd} )
            };
            return;
        }

        
    
        int p = 0;
        int d = 0;

        foreach(Scene s in scenes)
        {
            if (s != null)
            {
                if (s.sceneNumber == newSceneNumber)
                {
                    foreach (Dialog e in scenes[p].dialogs)
                    {

                        if (e.charName1 == dialogToAdd.charName1 && e.charName2 == dialogToAdd.charName2)
                        {
                            scenes[p].dialogs[d] = dialogToAdd;
                            return;
                        }

                        d++;
                    }

                    Dialog[] tempD = scenes[p].dialogs;
                    scenes[p].dialogs = new Dialog[tempD.Length + 1];

                    int i = 0;

                    foreach(Dialog t in tempD)
                    {
                        scenes[p].dialogs[i] = t;

                        i++;
                    }

                    scenes[p].dialogs[i] = dialogToAdd;

                    return;
                }
            }
            p++;
        }

        Scene[] temp = scenes;

        scenes = new Scene[temp.Length + 1];

        for (int i = 0; i < temp.Length; i++)
        {
            scenes[i] = temp[i];
        }

        Debug.Log(scenes.Length);

        Debug.Log(temp.Length);

        scenes[temp.Length] = new Scene(newSceneNumber, new Dialog[] { dialogToAdd });

    }

    public Dialog[] GetDialog(int sceneNumber)
    {
        Dialog[] foundDialog = new Dialog[1];

        foreach (Scene d in scenes)
        {
            if (d.sceneNumber == sceneNumber)
            {
                foundDialog = d.dialogs;
            }
        }

        return foundDialog;
    }

}

public class Scene
{
    [XmlElement("SceneNumber")]
    public int sceneNumber;

    [XmlArray("Dialogs")]
    public Dialog[] dialogs;

    public Scene()
    {
        dialogs = new Dialog[1];
        sceneNumber = 0;
    }

    public Scene(int newSceneNumber,Dialog[] newDialogs)
    {
        sceneNumber = newSceneNumber;
        dialogs = newDialogs;
    }

    public void Add(int sceneNumber,Dialog dialog)
    {
        this.sceneNumber = sceneNumber;
        
        if (dialogs == null)
        {
            dialogs = new Dialog[1];
            dialogs[0] = dialog;

            return;
        }

        Dialog[] temp = dialogs;

        dialogs = new Dialog[temp.Length + 1];

        for (int i = 0; i < temp.Length-1; i++)
        {
            dialogs[i] = temp[i];
        }

        dialogs[dialogs.Length - 1] = dialog;

    }
}

public class Dialog
{
    public Dialog()
    {
        dialogNumber = 0;
        texts = new string[] { "no text" };
        pictureName1 = "no picture";
        pictureName2 = "no picture";
        charName1 = Characters.Chicka;
        charName2 = Characters.Chicka;
    }

    [XmlElement("CharacterName1")]
    public Characters charName1;

    [XmlElement("CharacterName2")]
    public Characters charName2;

    [XmlElement("DialogNumber")]
    public int dialogNumber;

    [XmlArray("Texts")]
    public string[] texts;

    [XmlElement("PictureName1")]
    public string pictureName1;

    [XmlElement("PictureName2")]
    public string pictureName2;
}

public enum Characters
{
    John,
    Selena,
    Patrick,
    Chicka
}