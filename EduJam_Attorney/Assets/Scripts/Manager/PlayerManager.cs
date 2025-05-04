using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private TeacherManager teacher;
    
    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        
        Destroy(this);
    }
    
    public void Initialize(TeacherManager teacher)
    {
        this.teacher = teacher;
    }
    
    public void HandleCorrectObjection(DialogueData dialogueData)
    {
        Debug.Log($"[{nameof(PlayerManager)} - {nameof(HandleCorrectObjection)}]");
        // display options here
    }
    
    public void HandleIncorrectObjection(DialogueData dialogueData)
    {
        Debug.Log($"[{nameof(PlayerManager)} - {nameof(HandleIncorrectObjection)}]");
    }
    
    public void HandleAnswerSelected()
    {
        Debug.Log($"[{nameof(PlayerManager)} - {nameof(HandleAnswerSelected)}]");
    }
}
