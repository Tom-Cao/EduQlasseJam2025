using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct DialogueData
{
    public string Statement;
    public bool IsStatementErroneous;
    public bool IsErrorCausedByForgetfulness;
    public string CorrectSubstring;
    public List<string> WordBankStrings;
    public int PointsScored;
}

public class DialogueTool : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextAsset adminDialogueFile;
    [SerializeField] private string erroneousStatementReplacementText;
    
    [Header("Indexing")]
    [SerializeField] private int statementStringColumnIndex;
    [SerializeField] private int erroneousBoolColumnIndex;
    [SerializeField] private int forgottenBoolColumnIndex;
    [SerializeField] private int incorrectSubstringColumnIndex;
    [SerializeField] private int correctSubstringColumnIndex;
    [SerializeField] private int wordBankStringsColumnIndex;
    [SerializeField] private int pointsScoredColumnIndex;
    
    private List<DialogueData> _dialogueData;
    
    public List<DialogueData> DialogueData => _dialogueData;
    
    public List<DialogueData> InitializeData()
    {
        var adminDialogueEntries = adminDialogueFile.text.Split(Environment.NewLine);
        
        foreach (var adminDialogueEntry in adminDialogueEntries)
        {
            _dialogueData.Add(ProcessDialogueData(adminDialogueEntry));
        }
        
        return _dialogueData;
    }
    
    private DialogueData ProcessDialogueData(string adminDialogueEntry)
    {
        var data = adminDialogueEntry.Split(";");
        
        var statementString = data[statementStringColumnIndex];
        var isStatementErroneous = Convert.ToBoolean(data[erroneousBoolColumnIndex]);
        var isErrorCausedByForgetfulness = Convert.ToBoolean(data[forgottenBoolColumnIndex]);
        var correctSubstring = data[correctSubstringColumnIndex];
        var wordBankStrings = data[wordBankStringsColumnIndex].Split(",").ToList();
        
        if (!wordBankStrings.Contains(correctSubstring))
        {
            wordBankStrings.Add(correctSubstring);
        }
        
        var dialogueData = new DialogueData
        {
            Statement = !isStatementErroneous ? statementString :
                statementString.Replace(correctSubstring, isErrorCausedByForgetfulness ? erroneousStatementReplacementText :
                    data[incorrectSubstringColumnIndex]),
            IsStatementErroneous = isStatementErroneous,
            IsErrorCausedByForgetfulness = isErrorCausedByForgetfulness,
            CorrectSubstring = correctSubstring,
            WordBankStrings = wordBankStrings,
            PointsScored = Convert.ToInt32(data[pointsScoredColumnIndex]),
        };
        
        return dialogueData;
    }
}