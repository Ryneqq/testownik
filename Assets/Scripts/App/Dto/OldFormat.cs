using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OldFormat {
    const string ERROR_MESSAGE_FILE_TO_SHORT            = "Uwaga!\nPlik zawierał tylko jedna linijke danych.\nZobacz jak tworzyć pytania w zakladce 'Pomoc'.";
    const string ERROR_MESSAGE_WRONG_FORMAT             = "Uwaga!\nPytanie powinno zaczynać się od 'X'.\nZobacz jak tworzyć pytania w zakładce 'Pomoc'.";
    const string ERROR_MESSAGE_WRONG_NUMBER_OF_ANWSERS  = "Uwaga!\nZła ilość pytań.\nZobacz jak tworzyć pytania w zakładce 'Pomoc'.";

    public static QuestionDto TryParse(string read) {
        var lines = OldFormat.RemoveTrailingNewLines(read.Split('\n'));

        if (lines[0][0] != 'X')
        {
            throw new System.FormatException(ERROR_MESSAGE_WRONG_FORMAT);
        }
        else if (lines.Length <= 2)
        {
            throw new System.FormatException(ERROR_MESSAGE_FILE_TO_SHORT);
        }
        else if (lines[0].Length != lines.Length - 1)
        {
            throw new System.FormatException(ERROR_MESSAGE_WRONG_NUMBER_OF_ANWSERS);
        }
        else
        {
            var correctness         = lines[0];
            var question            = lines[1];
            var correct_anwsers     = new List<string>();
            var incorrect_anwsers   = new List<string>();

            for (int i = 2; i < lines.Length; i++) {
                if (OldFormat.CharToBool(correctness[i-1]))
                {
                    correct_anwsers.Add(lines[i]);
                }
                else
                {
                    incorrect_anwsers.Add(lines[i]);
                }
            }

            return new QuestionDto(question, correct_anwsers.ToArray(), incorrect_anwsers.ToArray());
        }
    }

    private static bool CharToBool(char i)
    {
        return i == '1';
    }

    private static string[] RemoveTrailingNewLines(string[] lines) {
        var lines_list = new List<string>();
        var temp_line  = string.Empty;

        foreach (var line in lines) 
        {
            temp_line = line.Trim();
            if (temp_line != string.Empty)
            {
                lines_list.Add(temp_line);
            }
        }

        return lines_list.ToArray();
    }
}
