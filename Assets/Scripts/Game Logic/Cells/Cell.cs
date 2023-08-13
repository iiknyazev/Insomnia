using System;
using UnityEngine;

public class Cell
{
    public readonly Color color;

    public Cell ()
    {
        color = Color.black;
    }

    public Cell(Color color)
    {
        this.color = color;
    }

    public virtual Cell Reaction(PlayerManager player) 
    {
        return this;
    }

    public override string ToString() => ".";
}
