using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public struct DialogueData
{
    public string Statement;
    public bool IsStatementErroneous;
    public bool IsErrorCausedByForgetfulness;
    public string CorrectSubstring;
    public string IncorrectSubstring;
    public List<string> WordBankStrings;
    public int PointsScored;
}

public class DialogueTool : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextAsset adminDialogueFile;
    [SerializeField] private string entrySeparatorString = "\",\"";
    [SerializeField, Min(0)] private int linesToSkip;
    [SerializeField] private string erroneousStatementReplacementText = "...";
    [SerializeField] private string boolConfirmationText = "oui";
    [SerializeField] private string forgottenConfirmationText = "info manquante";
    
    [Header("Indexing")]
    [SerializeField] private int statementStringColumnIndex;
    [SerializeField] private int erroneousBoolColumnIndex;
    [SerializeField] private int forgottenBoolColumnIndex;
    [SerializeField] private int incorrectSubstringColumnIndex;
    [SerializeField] private int correctSubstringColumnIndex;
    [SerializeField] private int wordBankStringsColumnIndex;
    [SerializeField] private int pointsScoredColumnIndex;
    
    private Regex _validateNumberRegex;
    
    private readonly List<DialogueData> _dialogueData = new();
    
    public List<DialogueData> InitializeData()
    {
        if (adminDialogueFile == null)
        {
            return _dialogueData;
        }
        
        _validateNumberRegex = new Regex("^\\d+");
        
        var adminDialogueEntries = adminDialogueFile.text.Split(Environment.NewLine);
        var skipLineCounter = 0;
        
        foreach (var adminDialogueEntry in adminDialogueEntries)
        {
            if (skipLineCounter < linesToSkip)
            {
                skipLineCounter++;
                continue;
            }

            var dialogueData = ProcessDialogueData(adminDialogueEntry);
            
            if (dialogueData != null)
            {
                _dialogueData.Add((DialogueData)dialogueData);
            }
        }
        
        return _dialogueData;
    }
    
    private DialogueData? ProcessDialogueData(string adminDialogueEntry)
    {
        // Return if the entry is empty
        if (string.IsNullOrEmpty(adminDialogueEntry))
        {
            return null;
        }
        
        var data = adminDialogueEntry.Split(entrySeparatorString);
        
        // Return if the statement is empty
        var statementString = ProcessString(data[statementStringColumnIndex]);
        if (string.IsNullOrWhiteSpace(statementString))
        {
            return null;
        }
        
        // Return if the necessary information is empty
        var isStatementErroneous = data[erroneousBoolColumnIndex].ToLower().Contains(boolConfirmationText);
        var isErrorCausedByForgetfulness = data[forgottenBoolColumnIndex].ToLower().Contains(forgottenConfirmationText);
        var incorrectSubstring = isErrorCausedByForgetfulness ? erroneousStatementReplacementText : ProcessString(data[incorrectSubstringColumnIndex]);
        var correctSubstring = ProcessString(data[correctSubstringColumnIndex]);
        
        if (isStatementErroneous &&
            (string.IsNullOrWhiteSpace(incorrectSubstring) || string.IsNullOrWhiteSpace(correctSubstring)))
        {
            return null;
        }
    
        
        var wordBankStrings = ProcessString(data[wordBankStringsColumnIndex]).Split(",").ToList();
        var processedWordBankStrings = new List<string>();
        foreach (var wordBankString in wordBankStrings)
        {
            processedWordBankStrings.Add(wordBankString.Trim());
        }
        
        var pointsTextExtracted = string.Join("",
            _validateNumberRegex.Matches(data[pointsScoredColumnIndex]).Select(x => x.Value).ToArray());
        var pointsScored = pointsTextExtracted.Length == 0 ? 0 : Convert.ToInt32(pointsTextExtracted);
        
        // Process statement depending on whether the statement is erroneous or not
        var processedStatement = !isStatementErroneous
            ? statementString
            : statementString.Replace(correctSubstring, incorrectSubstring);
        
        // Check if the answer is within the options
        if (!processedWordBankStrings.Contains(correctSubstring))
        {
            processedWordBankStrings.Add(correctSubstring);
        }
        
        var dialogueData = new DialogueData
        {
            Statement = processedStatement,
            IsStatementErroneous = isStatementErroneous,
            IsErrorCausedByForgetfulness = isErrorCausedByForgetfulness,
            CorrectSubstring = correctSubstring,
            IncorrectSubstring = incorrectSubstring,
            WordBankStrings = processedWordBankStrings,
            PointsScored = pointsScored,
        };
        
        return dialogueData;
    }

    private string ProcessString(string stringToProcess)
    {
        if (string.IsNullOrWhiteSpace(stringToProcess) || stringToProcess.Length == 0)
        {
            return stringToProcess;
        }
        
        var processedString = stringToProcess;
        
        if (stringToProcess[..1].Equals("\""))
        {
            processedString = processedString[1..];
        }
        
        if (stringToProcess[^1..].Equals("\""))
        {
            processedString = processedString[..^1];
        }
        
        return processedString.Trim();
    }

    public void UpdateDialogueString(int index, string correctStatement)
    {
        var dataToUpdate = _dialogueData[index];
        dataToUpdate.Statement = correctStatement;
        _dialogueData[index] = dataToUpdate;
    }
}