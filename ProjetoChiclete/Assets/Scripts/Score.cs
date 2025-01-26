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
    public static List<ScoreSlot> scoreSlots = new List<ScoreSlot>();

    public static int Calculate(List<Gum> gums, float breathMultiplier)
    {
        BuildScoreSlots(gums);

        MergeSlots();

        int finalScore = ProcessScoreSlots((int)(breathMultiplier * 10));

        scoreSlots.Clear();
        return finalScore;
    }

    public static void MergeSlots()
    {
        MergeTrios();
        MergeDuos();
    }

    public static void MergeDuos()
    {
        for (int i = 0; i <= scoreSlots.Count - 2; i++)
        {
            ScoreSlot current = scoreSlots[i];
            ScoreSlot next = scoreSlots[i + 1];
            if ((current.flavour == next.flavour) && (current.quantity == next.quantity))
            {
                scoreSlots[i].points += next.points;
                scoreSlots[i].points *= 2;
                scoreSlots[i].quantity += next.quantity;

                scoreSlots.RemoveAt(i+1);
                i++;
            }
        }
    }

    public static void MergeTrios()
    {
        for (int i = 0; i <= scoreSlots.Count - 3; i++)
        {
            List<ScoreSlot> window = scoreSlots.GetRange(i, 3);
            if (MergeDistinctTrios(window, i))
            {
                i += 2;
                continue;
            }
            else if (MergeEqualTrios(window, i))
            {
                i += 2;
                continue;
            }
        }
    }

    public static bool MergeEqualTrios(List<ScoreSlot> window, int index)
    {
        if (window.All(item => (item.flavour == window[0].flavour) && (item.quantity == window[0].quantity)))
        {
            scoreSlots[index].points += scoreSlots[index + 1].points + scoreSlots[index + 2].points;
            scoreSlots[index].points *= 3;
            scoreSlots[index].quantity += scoreSlots[index + 1].quantity + scoreSlots[index + 2].quantity;

            scoreSlots.RemoveAt(index + 2);
            scoreSlots.RemoveAt(index + 1);
            return true;
        }
        return false;
    }

    public static bool MergeDistinctTrios(List<ScoreSlot> window, int index)
    {
        if (window.Distinct(new ScoreSlotEqualityComparer()).Count() == window.Count)
        {
            scoreSlots[index].points += scoreSlots[index + 1].points + scoreSlots[index + 2].points;
            scoreSlots[index].points *= 4;
            scoreSlots[index].quantity += scoreSlots[index + 1].quantity + scoreSlots[index + 2].quantity;

            scoreSlots.RemoveAt(index + 2);
            scoreSlots.RemoveAt(index + 1);
            return true;
        }
        return false;
    }

    public static int ProcessScoreSlots(int breathMultiplier)
    {
        int points = 0;
        int quantity = 0;
        foreach (ScoreSlot slot in scoreSlots)
        {
            points += slot.points;
            quantity += slot.quantity;
        }
        return points * quantity * breathMultiplier;
    }

    public static void BuildScoreSlots(List<Gum> gums)
    {
        foreach (Gum gum in gums)
        {
            ScoreSlot scoreSlot = new ScoreSlot(DifficultyModifier[gum.difficulty], 1, gum.flavour);
            scoreSlots.Add(scoreSlot);
        }
    }

}
