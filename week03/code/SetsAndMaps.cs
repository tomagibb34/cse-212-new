// CSE 212 Lesson Week 3 Problem 1,2,3,5.
// Developer - Tom Gibb
//
// This code defines a class called SetsAndMaps.  The class contains static methods that
// use sets and maps to solve problems.  The problems are described in the comments above each
/// method.  The methods are called from the Main method in SetsAndMaps.cs.  The methods are
// called with the appropriate parameters and the return values are printed to the console.
// Do not make changes to the SetsAndMaps class.  You will be adding code to the methods to solve the problems.
// The problems are described in the comments above each method.  The methods are called from the Main method in SetsAndMaps.cs.
// The methods are called with the appropriate parameters and the return values are printed to the console.





using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Evaluate the contents of the 'words' array and return an array of strings that contain the symmetric pairs of words.
        // For example, if words was: [am, at, ma, if, fi], we would return : ["am & ma", "if & fi"]
        // The order of the array does not matter, nor does the order of the specific words in each string in the array.
        // at would not be returned because ta is not in the list of words.
        // Implement a test for null string
        // Implement code for triple or more words


        if (words == null) return Array.Empty<string>();

        var pairs = new List<string>();

        // build set ignoring any null entries
        
        var wordSet = new HashSet<string>(words.Where(w => w != null));
        var seen = new HashSet<string>();

        foreach (var word in wordSet)
        {
            if (seen.Contains(word)) continue;
            var reversed = new string(word.Reverse().ToArray());
            if (word != reversed && wordSet.Contains(reversed))
            {
                pairs.Add($"{word} & {reversed}");
                seen.Add(word);
                seen.Add(reversed);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // Expect degree in 4th column (index 3). Skip malformed lines.
            if (fields.Length < 4) continue;
            var degree = fields[3]?.Trim();
            if (string.IsNullOrEmpty(degree)) continue;

            if (degrees.ContainsKey(degree)) degrees[degree]++;
            else degrees[degree] = 1;
        }

        // Return dictionary ordered from most to least frequent (insertion order preserved)
        //var ordered = degrees
        //    .OrderByDescending(kv => kv.Value)
        //    .ThenBy(kv => kv.Key)
        //    .ToDictionary(kv => kv.Key, kv => kv.Value);

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1 == null || word2 == null) return false;
        
        // Normalize: remove spaces and convert to lowercase
        var normalized1 = word1.Replace(" ", "").ToLower();
        var normalized2 = word2.Replace(" ", "").ToLower();
        
        // If lengths differ, they can't be anagrams
        if (normalized1.Length != normalized2.Length) return false;
        
        // Count character frequencies in word1
        var charCount = new Dictionary<char, int>();
        foreach (var c in normalized1)
        {
            if (charCount.ContainsKey(c)) charCount[c]++;
            else charCount[c] = 1;
        }
        
        // Verify word2 has same character frequencies
        foreach (var c in normalized2)
        {
            if (!charCount.ContainsKey(c)) return false;
            charCount[c]--;
            if (charCount[c] < 0) return false;
        }
        
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        return [];
    }
}