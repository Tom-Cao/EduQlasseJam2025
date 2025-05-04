using UnityEngine;

public class TeacherManager : MonoBehaviour
{
    private Dialogue dialogue;
    private PlayerManager player;
    
    public static TeacherManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        
        Destroy(this);
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
        player.HandleCorrectObjection(dialogueData);
    }

    private void HandleIncorrectObjection(DialogueData dialogueData)
    {
        Debug.Log($"[{nameof(TeacherManager)} - {nameof(HandleIncorrectObjection)}]");
        // do any animations and shit here
        player.HandleIncorrectObjection(dialogueData);
    }

    private void HandleMethodFailure()
    {
        Debug.LogError($"Failure occurred in {nameof(TeacherManager)}.");
    }
}
