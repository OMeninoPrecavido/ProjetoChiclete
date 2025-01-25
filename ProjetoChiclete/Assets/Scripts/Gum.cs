using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//Base class that represents a single
public class Gum
{
    public Difficulty difficulty { get; private set; } //Gum difficulty
    public Flavour flavour { get; private set; } //Gum flavour

    public List<KeyCode> arrowSequence { get; private set; } //Sequence of arrows vinculated to the gum

    public Gum()
    {
        //Constructor randomizes fields
        difficulty = (Difficulty)Random.Range(0, 3); //Random difficulty
        flavour = (Flavour)Random.Range(0, 3); //Random flavour

        //Random sequence of arrows
        arrowSequence = new List<KeyCode>();

        int numOfarrows = 0;

        switch (difficulty)
        {
            //Number of arrows varies based on difficulty of gum
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
            KeyCode arrow = RandomArrow();
            arrowSequence.Add(arrow);
        }
    }

    //Method that outputs a random KeyCode arrow.
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

public enum Difficulty //Enumerator for gum difficulties
{
    Easy,
    Normal,
    Hard
}

public enum Flavour //Enumerator for gum flavours
{
    Tutti,
    Mint,
    Blueberry
}
