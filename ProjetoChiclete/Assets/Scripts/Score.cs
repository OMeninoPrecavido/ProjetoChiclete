using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float Calculate(List<Gum> gums) {
        Debug.Log("Calculating score:");
        float finalScore = 0.0f;
        foreach (Gum gum in gums)
        {
            Debug.Log(gum.flavour + " " + gum.difficulty);
        }
        return 0.0f;
    }
}
