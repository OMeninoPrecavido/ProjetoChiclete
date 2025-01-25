using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Dictionary<Difficulty, int> DifficultyModifier = new Dictionary<Difficulty, int>
    {
        {Difficulty.Easy, 1},
        {Difficulty.Normal, 3},
        {Difficulty.Hard, 5},
    };

    public static int Calculate(List<Gum> gums)
    {
        List<Difficulty> difficulties = gums.Select(item => item.difficulty).ToList();
        List<Flavour> flavours = gums.Select(item => item.flavour).ToList();
        int finalScore = 0;
        //Calculate scores based on the individual difficulty
        foreach (Difficulty difficulty in difficulties)
        {
            finalScore += DifficultyModifier[difficulty];
        }


        //Getting sliding window to calculate for groups of 3
        for (int i = 0; i <= flavours.Count - 3; i++) {
            List<Flavour> window = flavours.GetRange(i, 3);
            Debug.Log(string.Join(" ", window));
            //Check if we have a all different trio
            if(window.Distinct().Count() == window.Count) 
            {
                i += 2;
                continue;
            }
            //Check if we have a trio in a list
            if(window.All(item => item == window[0]))
            {
                i += 2;
                continue;
            }
        }

        return finalScore;
    }
}
