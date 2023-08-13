using System;
using UnityEngine;

public class Hole : Cell
{
    public static AudioSource Sound;
    public Hole() : base(ColorPalette.Hole) { }

    public override Cell Reaction(PlayerManager player)
    {
        Sound.Play();
        if (player.Passages == 0)
        {
            player.NewPosition(PlayerManager.StartPos.x, PlayerManager.StartPos.y);
        }
        player.SetPassages(player.Passages - 1);
        return this;
    }

    public override string ToString() => "~";
}
