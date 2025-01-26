using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSlot
{
    public int points { get; set; }
    public int quantity { get; set; }
    public Flavour flavour { get; set; }

    public ScoreSlot(int points, int quantity, Flavour flavour)
    {
        this.points = points;
        this.quantity = quantity;
        this.flavour = flavour;
    }
}

public class ScoreSlotEqualityComparer : IEqualityComparer<ScoreSlot>
{
    // Compare two ScoreSlot objects by their Flavour
    public bool Equals(ScoreSlot x, ScoreSlot y)
    {
        // Check for null values and compare only the Flavour property
        if (x == null || y == null) return false;
        return (x.flavour == y.flavour) && (x.quantity == y.quantity);
    }

    // Generate hash code based on Flavour property
    public int GetHashCode(ScoreSlot obj)
    {
        if (obj == null) return 0;
        return obj.flavour.GetHashCode();
    }
}
