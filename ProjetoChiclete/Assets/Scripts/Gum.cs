using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Gum
{
    public Difficulty difficulty { get; private set; }
    public Flavour flavour { get; private set; }

    public List<KeyCode> arrowSequence {  get; private set; }

    public Gum()
    {
        difficulty = (Difficulty)Random.Range(0, 3);
        flavour = (Flavour)Random.Range(0, 3);

        int numOfarrows = 0;

        switch (difficulty)
        {
            case Difficulty.Easy:
                numOfarrows = Random.Range(3, 6);
                break;
            case Difficulty.Normal:
                numOfarrows = Random.Range(6, 8);
                break;
            case Difficulty.Hard:
                numOfarrows = Random.Range(8, 10);
                break;
        }

        for (int i = 0; i < numOfarrows; i++)
        {
            arrowSequence.Add(RandomArrow());
        }
    }

    private KeyCode RandomArrow()
    {
        int rand = Random.Range(0, 4);
        KeyCode arrow = KeyCode.UpArrow;

        switch (rand)
        {
            case 0:
                arrow = KeyCode.DownArrow;
                break;
            case 1:
                arrow = KeyCode.LeftArrow;
                break;
            case 2:
                arrow = KeyCode.RightArrow;
                break;
            case 3:
                arrow = KeyCode.UpArrow;
                break;

        }
        return arrow;
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public enum Flavour
{
    Tutti,
    Mint,
    Blueberry
}