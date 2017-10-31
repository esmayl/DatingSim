using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitHandler : MonoBehaviour
{

    Image char1;
    Image char2;

    Object[] temp;

    public void Init()
    {
        temp = Resources.LoadAll("portraits");

        char1 = transform.Find("Char1").GetComponent<Image>();

        char2 = transform.Find("Char2").GetComponent<Image>();
    }

    public void SetCharPortrait(int charNumber,Characters imageName)
    {
        Sprite tempSprite = new Sprite();

        for (int c = 0; c < temp.Length; c++)
        {

            if (temp[c].name == imageName.ToString())
            {
                tempSprite = Sprite.Create((Texture2D)temp[c], new Rect(0, 0, ((Texture2D)temp[c]).width, ((Texture2D)temp[c]).height), new Vector2(0.5f, 0.5f));
                break;
            }

        }


        if (charNumber == 0)
        {
            char1.sprite = tempSprite;
        }
        else
        {
            char2.sprite = tempSprite;
        }
    }
}
