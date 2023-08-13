using System;
using UnityEngine;

public class Passage : Cell
{
    public static AudioSource Sound;
    public Passage() : base(ColorPalette.Passage) { }

    public override Cell Reaction(PlayerManager player)
    {
        Sound.Play();
        player.SetPassages(player.Passages + 1);
        return new Cell();
    }

    public override string ToString() => "+";
}
