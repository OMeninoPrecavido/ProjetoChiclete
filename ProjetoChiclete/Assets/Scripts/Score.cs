using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Dictionary<Difficulty, int> DifficultyModifier = new Dictionary<Difficulty, int>
    {
        {Difficulty.Easy, 1},
        {Difficulty.Normal, 3},
        {Difficulty.Hard, 5},
    };

    public static int Calculate(List<Gum> gums) {
        int finalScore = 0;
        //Calculate scores based on the individual difficulty
        foreach (Gum gum in gums)
        {
            finalScore += DifficultyModifier[gum.difficulty];
        }
        return finalScore;
    }
}
