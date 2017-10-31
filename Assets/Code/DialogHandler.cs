using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DialogHandler : MonoBehaviour
{

    TemplateXML fileInfo;

    public int sceneNumber = 1;
    public TextHandler textCanvas;
    public PortraitHandler portraitCanvas;
    public static string fileLocation = "Assets/Dialog/dialog.xml";


    void Awake()
    {
        textCanvas.Init();
        textCanvas.SetCharName(0, Characters.Selena);
        textCanvas.SetCharName(1, Characters.Chicka);

        portraitCanvas.Init();
        portraitCanvas.SetCharPortrait(0, Characters.Selena);
        portraitCanvas.SetCharPortrait(1, Characters.Chicka);
    }


    public void NextLine()
    {
        textCanvas.NextLine();
    }
}
