using System;
using System.Collections.Generic;
using UnityEngine;


public class ColorChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public void ChangeBubbleColor(List<Gum> gums) {
        float redCount = 0;
        float blueCount = 0;
        float greenCount = 0;
        foreach(Gum gum in gums) {
            switch(gum.flavour) {
                case Flavour.Tutti:
                    redCount += 1.0f;
                    break;
                case Flavour.Mint:
                    greenCount += 1.0f;
                    break;
                case Flavour.Blueberry:
                    blueCount += 1.0f;
                    break;
            }
        }
        float maxCount = Mathf.Max(redCount, greenCount, blueCount);
        redCount /= maxCount;
        greenCount /= maxCount;
        blueCount /= maxCount;
        spriteRenderer.color = new Color(redCount, greenCount, blueCount, 0.68627451f);
    }
}
