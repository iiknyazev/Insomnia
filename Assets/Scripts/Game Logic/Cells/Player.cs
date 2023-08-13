using System;
using UnityEngine;
public class Player : Cell
{
    public Player() : base(ColorPalette.Player) { }

    public override string ToString() => "@";
}