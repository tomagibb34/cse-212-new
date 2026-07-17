/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            
            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        //Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");
        // Create an array of players and sort it by points in descending order

        var sortedPlayers = players.ToArray();

        Array.Sort(sortedPlayers, (p1, p2) => p2.Value - p1.Value);

        // Display only the top 20 players
        
        Console.WriteLine();
        Console.WriteLine("Top 20 NBA Players by Career Points");
        Console.WriteLine("===================================");
        Console.WriteLine("Rank | Player ID | Points");
        Console.WriteLine("-----|-----------|-------");
        Console.WriteLine();
        Console.WriteLine($"{sortedPlayers.Length} players found in the data.");

        for (int i = 0; i < 20 && i < sortedPlayers.Length; i++) {
            var player = sortedPlayers[i];
            Console.WriteLine($"{i + 1}. Player ID: {player.Key}, Points: {player.Value}");
        }
    }
}