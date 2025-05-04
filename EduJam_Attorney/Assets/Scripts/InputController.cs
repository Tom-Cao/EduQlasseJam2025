using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Grab input system
    //SimpleInput controls;

    // Main dialogue system
    Dialogue dialogue;

    AudioManager audioManager; // Reference to the AudioManager

    PlayerSettings playerSettings; // Reference to the PlayerSettings
    
    private TeacherManager teacherManager;
    private PlayerManager playerManager;
    
    private bool isInObjection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //controls = new SimpleInput();

        if(audioManager == null)
        {
            audioManager = AudioManager.instance; // Get the instance of the AudioManager class
            if (audioManager == null)
            {
                Debug.LogError("AudioManager instance not found in Input. Make sure the AudioManager script is attached to a GameObject in the scene.");
            }
        }

        if (dialogue == null)
        {
            dialogue = Dialogue.instance; // Get the instance of the Dialogue class
            if (dialogue == null)
            {
                Debug.LogError("Dialogue instance not found in Input. Make sure the Dialogue script is attached to a GameObject in the scene.");
            }
        }

        if(playerSettings == null)
        {
            playerSettings = PlayerSettings.instance; // Get the instance of the PlayerSettings class
            if (playerSettings == null)
            {
                Debug.LogError("PlayerSettings instance not found in Input. Make sure the PlayerSettings script is attached to a GameObject in the scene.");
            }
        }
        
        if (teacherManager == null)
        {
            teacherManager = TeacherManager.Instance;
            if (teacherManager == null)
            {
                Debug.LogError($"{nameof(TeacherManager)} instance not found in Input. Make sure the {nameof(TeacherManager)} script is attached to a GameObject in the scene.");
            }
        }
        
        if (playerManager == null)
        {
            playerManager = PlayerManager.Instance;
            if (playerManager == null)
            {
                Debug.LogError($"{nameof(PlayerManager)} instance not found in Input. Make sure the {nameof(PlayerManager)} script is attached to a GameObject in the scene.");
            }
        }
        
        if (teacherManager && playerManager)
        {
            teacherManager.Initialize(dialogue, playerManager);
            playerManager.Initialize(teacherManager);
        }
    }
    
    // Interact action
    public void InteractAction(InputAction.CallbackContext context)
    {
        if (isInObjection)
        {
            return;
        }
        
        if (context.performed) // Check if the action was performed
        {
            Debug.Log("Interact action triggered.");
        }
    }
    
    // Objection action
    public void ObjectionAction(InputAction.CallbackContext context)
    {
        if (isInObjection)
        {
            return;
        }
        
        if (context.performed) // Check if the action was performed
        {
            switch (playerSettings.pitchLevel) // Check the pitch level
            {
                case PlayerSettings.PitchLevels.Low:
                    audioManager.oneShotSource.pitch = 0.75f; // Set the pitch to low
                    audioManager.PlaySource(audioManager.ObjectionMaleSFX); // Play
                    break;
                case PlayerSettings.PitchLevels.Medium:
                    audioManager.oneShotSource.pitch = 1f; // Set the pitch to medium
                    audioManager.PlaySource(audioManager.ObjectionMaleSFX); //
                    break;
                case PlayerSettings.PitchLevels.High:
                    audioManager.PlaySource(audioManager.ObjectionFemaleSFX); // Play the
                    break;
                default:
                    break;
            }
            
            teacherManager.HandleObjection();
        }
    }
    
    // Text forward action
    public void TextForwardAction(InputAction.CallbackContext context)
    {
        if (isInObjection)
        {
            return;
        }
        
        if (context.performed) // Check if the action was performed
        {
            dialogue.NextLineInput();
        }
    }
    
    // Text rollback action
    public void TextRollbackAction(InputAction.CallbackContext context)
    {
        if (isInObjection)
        {
            return;
        }
        
        if(context.performed) // Check if the action was performed
        {
            dialogue.PreviousLineInput();
        }
    }
}
