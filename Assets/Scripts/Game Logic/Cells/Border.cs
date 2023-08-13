using System;
using UnityEngine;

public class Border : Cell
{
    public static AudioSource Sound;
    public Border() : base(ColorPalette.Border) { }

    public override Cell Reaction(PlayerManager player)
    {
        Sound.Play();
        player.NewPosition(player.PrevPos.x, player.PrevPos.y);
        return this;
    }

    public override string ToString() => "#";
}
