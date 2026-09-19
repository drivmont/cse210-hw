using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a scripture: a reference plus the words of its text.
/// Handles hiding random words and reporting progress, but knows
/// nothing about console input/output.
/// </summary>
public class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;
    private static readonly Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => new Word(word))
            .ToList();
    }

    /// <summary>
    /// Hides up to <paramref name="numberToHide"/> words that are not
    /// already hidden, chosen at random. Words that are already hidden
    /// are never re-selected, so every call makes real progress.
    /// </summary>
    public void HideRandomWords(int numberToHide)
    {
        List<int> eligibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                eligibleIndices.Add(i);
            }
        }

        List<int> indicesToHide = eligibleIndices
            .OrderBy(_ => _random.Next())
            .Take(numberToHide)
            .ToList();

        foreach (int index in indicesToHide)
        {
            _words[index].Hide();
        }
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }

    public int CountVisibleWords()
    {
        return _words.Count(word => !word.IsHidden());
    }

    public string GetDisplayText()
    {
        string text = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n{text}";
    }
}
