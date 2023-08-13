using System;
using UnityEngine;

public class End : Cell
{
    public static AudioSource Sound;
    public End() : base(ColorPalette.End) { }

    public override Cell Reaction(PlayerManager player)
    {
        Sound.Play();
        player.Win();
        return this;
    }

    public override string ToString() => "$";
}
