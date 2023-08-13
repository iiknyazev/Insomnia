using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CoreManager
{
    public readonly string GameState;
    public readonly char[,] Map;
    public readonly LogicManager LogicManager;

    public CoreManager() { }

    public CoreManager(string gameState)
    {
        if (gameState.Contains("!"))
        {
            string portalLinks = gameState.Substring(gameState.IndexOf("!") + 1);
            gameState = gameState.Substring(0, gameState.IndexOf("!"));

            // Проверка корректности portalLinks
            if (!IsCorrectPortalLinks(portalLinks, gameState))
                throw new ArgumentException("Incorrect portalLinks");

            LogicManager.SetPortalsLink(portalLinks);
        }

        // Проверка корректности наполнения уровня (символов)
        if (!IsCorrectGameState(gameState))
            throw new ArgumentException("Incorrect gameState");

        GameState = gameState;
        Map = ParseToCharArray(GameState);
        LogicManager = new LogicManager(Map);
            
        PlayerManager.SetStartPosition(LogicManager.Player.Pos);
    }

    private CoreManager(string gameState, char charBuffer = '.', int passages = 0, bool isWin = false)
    {
        GameState = gameState;
        Map = ParseToCharArray(GameState);
        LogicManager = new LogicManager(Map, charBuffer, passages, isWin);
    }

    public CoreManager Move(DivMove div)
    {
        var newMap = ParseToString(LogicManager.Move(div));

        return new CoreManager(
            newMap, 
            LogicManager.CharBuffer, 
            LogicManager.Player.Passages, 
            LogicManager.Player.IsWin
            );
    }

    private char[,] ParseToCharArray(string gameState)
    {
        var lines = stringTo2DArray(gameState);

        char[,] map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; ++i)
            for (int j = 0; j < lines[i].Length; ++j)
                map[i, j] = lines[i][j];
        return map;
    }

    public bool IsCorrectGameState(string gameState)
    {
        var lines = stringTo2DArray(gameState);

        // строго прямоугольная форма
        for (int i = 1; i < lines.Length; ++i)
            if (lines[i].Length != lines[0].Length)
                return false;

        // игрок только один
        if (gameState.LastIndexOf('@') != gameState.IndexOf('@') || !gameState.Contains('@'))
            return false;

        // только разрешенные символы
        for (int i = 0; i < lines.Length; ++i)
            for (int j = 0; j < lines[0].Length; ++j)
                if (lines[i][j] != '.' && lines[i][j] != '#' && lines[i][j] != '+' && lines[i][j] != '0' && lines[i][j] != '~' && lines[i][j] != '$' && lines[i][j] != '@')
                    return false;

        return true;
    }

    public bool IsCorrectPortalLinks(string links, string gameState)
    {
        var gameStateArray = stringTo2DArray(gameState);
        var linksArray = stringTo2DArray(links);

        List<int> linksList = new List<int>();
        foreach (string link in linksArray)
        {
            var posArray = link.Split(' ').Where(x => x.Length > 0).ToArray();
            // в каждой строчке ровно по четыре числа
            if (posArray.Length != 4) 
                return false;
            foreach (var pos in posArray)
            {
                // в качестве значений переданы именно числа
                if (!int.TryParse(pos, out var result))
                    return false;
                linksList.Add(result);
            }
        }

        var xList = new List<int>();
        var yList = new List<int>();

        for (int i = 0; i < linksList.Count; ++i)
        {
            if (i % 2 == 0)
                xList.Add(linksList[i]);
            else
                yList.Add(linksList[i]);
        }
        HashSet<Tuple<int, int>> portalPos = new HashSet<Tuple<int, int>>();
        for (int i = 0; i < xList.Count; ++i)
        {
            // координаты не выходят за пределы карты
            if (xList[i] < 0 || xList[i] > gameStateArray.Length
            ||  yList[i] < 0 || yList[i] > gameStateArray[0].Length)
                return false;
            var pos = new Tuple<int, int>(xList[i], yList[i]);
            // нет повторяющихся координат, т.е. нет порталов, которые связаны дважды
            if (portalPos.Contains(pos))
                return false;
            portalPos.Add(pos);
        }

        // на указаных координатах действительно стоят порталы
        foreach (var pos in portalPos)
            if (gameStateArray[pos.Item1][pos.Item2] != '0')
                return false;

        return true;
    }

    private string[] stringTo2DArray(string gameState)
    {
        return gameState.Split('\r', '\n').Where(x => x.Length > 0).ToArray();
    }

    private string ParseToString(Cell[,] map)
    {
        StringBuilder newGameState = new StringBuilder();

        for (int i = 0; i < map.GetLength(0); ++i)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
                newGameState.Append(map[i, j].ToString());
            newGameState.Append('\n');
        }

        return newGameState.ToString();
    }

    public Cell GetCellAt(int x, int y)
    {
        if (x < 0 || y < 0 || x > LogicManager.Map.GetLength(0) || y > LogicManager.Map.GetLength(1))
            throw new IndexOutOfRangeException();

        return LogicManager.Map[x, y];
    }

    public void Reset() => LogicManager.Reset();
}