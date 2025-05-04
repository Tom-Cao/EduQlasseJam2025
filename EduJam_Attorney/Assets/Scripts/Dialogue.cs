using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance; // Singleton instance of the Dialogue class

    [SerializeField] private DialogueTool dialogueTool;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private bool useDialogueTool;
    [SerializeField] string[] dialogueLines;
    [SerializeField] float textSpeed = 0.5f;

    //getter and setter for the currentLineIndex
    public int CurrentLineIndex
    {
        get { return currentLineIndex; }
        set { currentLineIndex = value; }
    }

    //getter for the dialogueLines
    public string[] DialogueLines
    {
        get { return dialogueLines; }
    }

    [SerializeField] int currentLineIndex = 0;

    private List<DialogueData> _dialogueData;

    //getter for the dialogueData
    public List<DialogueData> DialogueData
    {
        get { return _dialogueData; }
    }
    private bool _useDialogueData;

    private void Awake(){
        if(instance == null){
            instance = this; // Assign the instance if it's null
            _dialogueData = dialogueTool.InitializeData();
            _useDialogueData = useDialogueTool && _dialogueData is { Count: > 0 };
        }
        else{
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    
    private void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue(); // Start the dialogue
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    
    public DialogueData? GetCurrentDialogueData()
    {
        return useDialogueTool ? _dialogueData[currentLineIndex] : null;
    }
    
    public void PreviousLineInput()
    {
        var statementText = _useDialogueData ? _dialogueData[currentLineIndex].Statement : dialogueLines[currentLineIndex];
        
        if(dialogueText.text == statementText){
            PreviousLine(); // Go to the previous line when left arrow is pressed
        }
        else{
            StopAllCoroutines(); // Stop typing if left arrow is pressed again
            dialogueText.text = statementText; // Show the full line
        }
    }

    public void NextLineInput()
    {
        var statementText = _useDialogueData ? _dialogueData[currentLineIndex].Statement : dialogueLines[currentLineIndex];
        
        if(dialogueText.text == statementText){
            NextLine(); // Go to the next line when space is pressed
        }
        else{
            StopAllCoroutines(); // Stop typing if space is pressed again
            dialogueText.text = statementText; // Show the full line
        }
    }

    private void StartDialogue(){
        currentLineIndex = 0;
        StartCoroutine(TypeLetters());
    }

    private IEnumerator TypeLetters(){
        var statementText = _useDialogueData ? _dialogueData[currentLineIndex].Statement : dialogueLines[currentLineIndex];
        
        foreach(var letter in statementText){
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        var statementsCount = _useDialogueData ? _dialogueData.Count : dialogueLines.Length;
        
        if(currentLineIndex < statementsCount - 1){
            currentLineIndex++;
            dialogueText.text = string.Empty; // Clear the text for the next line
            StartCoroutine(TypeLetters());
        }
    }

    void PreviousLine(){
        if(currentLineIndex > 0 ){
            currentLineIndex--;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLetters());
        }
    }
}
