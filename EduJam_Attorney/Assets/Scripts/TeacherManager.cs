using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeacherManager : MonoBehaviour
{
    private Dialogue dialogue;
    private PlayerManager player;
    
    public static TeacherManager Instance;

    public List<int> IncorrectObjectionIndices { get; } = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        
        Destroy(this);
    }

    private void Start()
    {
        foreach (var dialogueData in dialogue.DialogueData)
        {
            if (dialogueData.IsStatementErroneous)
            {
                IncorrectObjectionIndices.Add(dialogue.DialogueData.IndexOf(dialogueData));
                Debug.Log("Index of Incorrect Objection: " + dialogue.DialogueData.IndexOf(dialogueData));
            }
            
        }
        Debug.Log("Incorrect Number of Statements: " + IncorrectObjectionIndices.Count);
    }

    public void Initialize(Dialogue dialogue, PlayerManager player)
    {
        this.dialogue = dialogue;
        this.player = player;
    }
    
    public void HandleObjection()
    {
        if (dialogue == null)
        {
            HandleMethodFailure();
            return;
        }

        int currentIndex = dialogue.CurrentLineIndex;
        if(IncorrectObjectionIndices.Contains(currentIndex))
        {
            IncorrectObjectionIndices.Remove(currentIndex);
        }
        
        var nullableDialogueData = dialogue.GetCurrentDialogueData();
        if (nullableDialogueData == null)
        {
            return;
        }
        
        var dialogueData = (DialogueData)nullableDialogueData;
        if (nullableDialogueData is { IsStatementErroneous: true })
        {
            
            HandleCorrectObjection(dialogueData);
        }
        else
        {
            HandleIncorrectObjection(dialogueData);
        }
    }
    
    private void HandleCorrectObjection(DialogueData dialogueData)
    {
        Debug.Log($"[{nameof(TeacherManager)} - {nameof(HandleCorrectObjection)}]");
        // do any animations and shit here

        HUDManager HUD = HUDManager.instance;
        HUD.ToggleReasonPanel();
        PlayerSettings.instance.AddScore(10);
        HUD.pointsText.text = PlayerSettings.instance.Score.ToString();
        player.HandleCorrectObjection(dialogueData);
    }

    private void HandleIncorrectObjection(DialogueData dialogueData)
    {
        Debug.Log($"[{nameof(TeacherManager)} - {nameof(HandleIncorrectObjection)}]");
        StartCoroutine(ErrorSoundDelay(2f));
        
        // do any animations and shit here
    }

    private IEnumerator ErrorSoundDelay(float delay){
        yield return new WaitForSeconds(delay);
        AudioManager audioManager = AudioManager.instance;
        HUDManager hUDManager = HUDManager.instance;
        audioManager.PlaySource(audioManager.errorSFX);
        PlayerSettings.instance.AddScore(-10);
        hUDManager.pointsText.text = PlayerSettings.instance.Score.ToString();
    }

    public void HandleObjectionEnd(){
        if(IncorrectObjectionIndices.Count <=0){
            Debug.Log("Game Over");
            // Quit Game
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    }

    private void HandleMethodFailure()
    {
        Debug.LogError($"Failure occurred in {nameof(TeacherManager)}.");
    }
}
