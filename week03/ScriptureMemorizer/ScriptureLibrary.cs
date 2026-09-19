using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Holds a collection of scriptures to practice and can hand back a
/// random one. Scriptures are loaded from a text file when one is
/// available so new verses can be added without recompiling; if no
/// file is found, a small built-in set is used instead so the program
/// always has something to practice.
/// </summary>
public class ScriptureLibrary
{
    private static readonly Regex _referencePattern =
        new Regex(@"^(?<book>.+)\s+(?<chapter>\d+):(?<verse>\d+)(-(?<endVerse>\d+))?$");

    private readonly List<Scripture> _scriptures = new List<Scripture>();
    private static readonly Random _random = new Random();

    public ScriptureLibrary(string fileName)
    {
        string path = ResolvePath(fileName);
        if (path != null)
        {
            LoadFromFile(path);
        }

        if (_scriptures.Count == 0)
        {
            LoadDefaultScriptures();
        }
    }

    public int Count => _scriptures.Count;

    public Scripture GetRandomScripture()
    {
        int index = _random.Next(_scriptures.Count);
        return _scriptures[index];
    }

    // Looks for the scriptures file next to the current directory first,
    // then next to the running executable, so it is found whether the
    // program is started with "dotnet run" or by running the .exe directly.
    private static string ResolvePath(string fileName)
    {
        if (File.Exists(fileName))
        {
            return fileName;
        }

        string nextToExecutable = Path.Combine(AppContext.BaseDirectory, fileName);
        if (File.Exists(nextToExecutable))
        {
            return nextToExecutable;
        }

        return null;
    }

    // Each non-blank, non-comment line looks like:
    //   Book Chapter:Verse|Text
    //   Book Chapter:Verse-EndVerse|Text
    private void LoadFromFile(string path)
    {
        foreach (string line in File.ReadAllLines(path))
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.Length == 0 || trimmedLine.StartsWith("#"))
            {
                continue;
            }

            string[] parts = trimmedLine.Split('|', 2);
            if (parts.Length != 2)
            {
                continue;
            }

            Reference reference = ParseReference(parts[0].Trim());
            string text = parts[1].Trim();
            if (reference == null || text.Length == 0)
            {
                continue;
            }

            _scriptures.Add(new Scripture(reference, text));
        }
    }

    private static Reference ParseReference(string referenceText)
    {
        Match match = _referencePattern.Match(referenceText);
        if (!match.Success)
        {
            return null;
        }

        string book = match.Groups["book"].Value;
        int chapter = int.Parse(match.Groups["chapter"].Value);
        int verse = int.Parse(match.Groups["verse"].Value);

        if (match.Groups["endVerse"].Success)
        {
            int endVerse = int.Parse(match.Groups["endVerse"].Value);
            return new Reference(book, chapter, verse, endVerse);
        }

        return new Reference(book, chapter, verse);
    }

    private void LoadDefaultScriptures()
    {
        _scriptures.Add(new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; in all thy ways acknowledge him, and he shall direct thy paths."));

        _scriptures.Add(new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."));

        _scriptures.Add(new Scripture(
            new Reference("Joshua", 1, 9),
            "Have not I commanded thee? Be strong and of a good courage; be not afraid, neither be thou dismayed: for the Lord thy God is with thee whithersoever thou goest."));

        _scriptures.Add(new Scripture(
            new Reference("Philippians", 4, 13),
            "I can do all things through Christ which strengtheneth me."));

        _scriptures.Add(new Scripture(
            new Reference("1 Nephi", 3, 7),
            "I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."));
    }
}
