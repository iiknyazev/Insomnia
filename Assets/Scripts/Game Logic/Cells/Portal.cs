using System;
using UnityEngine;
public class Portal : Cell
{
    public static AudioSource Sound;
    public Portal() : base (ColorPalette.Portal) { }

    public Position TeleportTo { get; private set; }
    public Portal(int xTo = 0, int yTo = 0) : base(ColorPalette.Portal) 
    {
        TeleportTo = new Position(xTo, yTo);
    }

    public override Cell Reaction(PlayerManager player)
    {
        Sound.Play();
        player.NewPosition(TeleportTo.x, TeleportTo.y);
        return this;
    }

    public override string ToString() => "0";
}