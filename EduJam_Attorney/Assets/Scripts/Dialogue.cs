using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance; // Singleton instance of the Dialogue class
    
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] string[] dialogueLines;
    [SerializeField] float textSpeed = 0.5f;

    //getter and setter for the currentLineIndex
    public int CurrentLineIndex
    {
        get { return currentLineIndex; }
        set { currentLineIndex = value; }
    }

    [SerializeField] int currentLineIndex = 0;



    void Awake(){
        if(instance == null){
            instance = this; // Assign the instance if it's null
        }
        else{
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty; // Clear the text at the start
        StartDialogue(); // Start the dialogue
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            nextLineInput(); // Check for space key press
        }
    }

    public void NextLineInput(){
        if(dialogueText.text == dialogueLines[currentLineIndex]){
                NextLine(); // Go to the next line when space is pressed
            }
            else{
                StopAllCoroutines(); // Stop typing if space is pressed again
                dialogueText.text = dialogueLines[currentLineIndex]; // Show the full line
        }
    }

    void StartDialogue(){
        currentLineIndex = 0;
        StartCoroutine(TypeLetters());
    }

    IEnumerator TypeLetters(){
        foreach(char letter in dialogueLines[currentLineIndex].ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine(){
        if(currentLineIndex < dialogueLines.Length - 1){
            currentLineIndex++;
            dialogueText.text = string.Empty; // Clear the text for the next line
            StartCoroutine(TypeLetters());
        }
        else{
            gameObject.SetActive(false); // Deactivate the dialogue box when finished
        }
    }
}
