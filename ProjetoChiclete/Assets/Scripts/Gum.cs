using UnityEngine;

public class Gum
{
    public Difficulty difficulty { get; private set; }
    public Flavour flavour { get; private set; }

    public Gum()
    {
        difficulty = (Difficulty)Random.Range(0, 2);
        flavour = (Flavour)Random.Range(0, 2);
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
    Strawberry
}