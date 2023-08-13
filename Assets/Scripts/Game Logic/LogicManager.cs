using System;
using System.Collections.Generic;
using System.Linq;

public enum DivMove
{
    up, down, left, right
}

public struct PortalLink
{
    public Position from;
    public Position to;
}

public class LogicManager
{
    public static Dictionary<Position, Position> PortalLinks = new Dictionary<Position, Position>();
    public PlayerManager Player { get; private set; }
    public Cell[,] Map { get; private set; }

    private Cell cellBuffer;
    public Cell CellBuffer {
        get
        {
            return cellBuffer;
        }
        private set
        {
            cellBuffer = value;
            switch (value.ToString())
            {
                case ".":
                    charBuffer = '.';
                    break;
                case "#":
                    charBuffer = '#';
                    break;
                case "$":
                    charBuffer = '$';
                    break;
                case "+":
                    charBuffer = '+';
                    break;
                case "~":
                    charBuffer = '~';
                    break;
                case "0":
                    charBuffer = '0';
                    break;
                default:
                    throw new ArgumentException();
            }
        } 
    }

    public char CharBuffer { 
        get
        {
            return charBuffer;
        }
        private set 
        {
            charBuffer = value;
            switch (value)
            {
                case '.':
                    cellBuffer = new Cell();
                    break;
                case '#':
                    cellBuffer = new Border();
                    break;
                case '$':
                    cellBuffer = new End();
                    break;
                case '+':
                    cellBuffer = new Passage();
                    break;
                case '~':
                    cellBuffer = new Hole();
                    break;
                case '0':
                    cellBuffer = new Portal();
                    break;
                default:
                    throw new ArgumentException();
            }
        } 
    }
    private char charBuffer;

    public LogicManager(char[,] map, char buffer = '.', int passages = 0, bool isWon = false)
    {
        Map = new Cell[map.GetLength(0), map.GetLength(1)];
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
            {
                switch (map[i, j])
                {
                    case '.':
                        Map[i, j] = new Cell();
                        break;
                    case '#':
                        Map[i, j] = new Border();
                        break;
                    case '$':
                        Map[i, j] = new End();
                        break;
                    case '+':
                        Map[i, j] = new Passage();
                        break;
                    case '~':
                        Map[i, j] = new Hole();
                        break;
                    case '@':
                        Map[i, j] = new Player();
                        Player = new PlayerManager(i, j, passages, isWon);
                        break;
                    case '0':
                        if (PortalLinks.ContainsKey(new Position(i, j)))
                        {
                            Position to = new Position(PortalLinks[new Position(i, j)]);
                            Map[i, j] = new Portal(to.x, to.y);
                        }
                        else
                            Map[i, j] = new Portal(i, j);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        CharBuffer = buffer;
    }

    public static void SetPortalsLink(string links)
    {
        var positions = links.Split(' ', '\n', '\r').Where(x => x.Length != 0).ToList();
        for (int i = 0; i < positions.Count; i += 4)
        {
            var xFrom = int.Parse(positions[i]);
            var yFrom = int.Parse(positions[i + 1]);
            var xTo = int.Parse(positions[i + 2]);
            var yTo = int.Parse(positions[i + 3]);
            PortalLinks.Add(
                new Position(xFrom, yFrom),
                new Position(xTo, yTo)
                );
            PortalLinks.Add(
                new Position(xTo, yTo),
                new Position(xFrom, yFrom)
                );
        }
    }

    public Cell[,] Move(DivMove div)
    {
        switch(div)
        {
            case DivMove.up:
                ShiftTo(-1, 0);
                break;
            case DivMove.down:
                ShiftTo(1, 0);
                break;
            case DivMove.left:
                ShiftTo(0, -1);
                break;
            case DivMove.right:
                ShiftTo(0, 1);
                break;
        }
        return Map;
    }

    private void ShiftTo(int xShift, int yShift)
    {
        var newX = (Player.Pos.x + xShift != -1 ? Player.Pos.x + xShift : Map.GetLength(0) - 1)
            % Map.GetLength(0);
        var newY = (Player.Pos.y + yShift != -1 ? Player.Pos.y + yShift : Map.GetLength(1) - 1)
            % Map.GetLength(1);

        MoveTo(newX, newY);
        Player.NewPosition(newX, newY);

        CellBuffer = CellBuffer.Reaction(Player);
        MoveTo(Player.Pos.x, Player.Pos.y);
    }

    private void MoveTo(int x, int y)
    {
        if (Map[x, y] is Player)
            return;

        var tmp = Map[x, y];
        Map[x, y] = Map[Player.PrevPos.x, Player.PrevPos.y];
        Map[Player.PrevPos.x, Player.PrevPos.y] = CellBuffer;
        CellBuffer = tmp;
    }

    public void Reset()
    {
        Player.NextLevel();
        PortalLinks = new Dictionary<Position, Position>();
    }
}