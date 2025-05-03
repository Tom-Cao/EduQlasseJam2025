using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Grab input system
    //SimpleInput controls;

    // Main dialogue system
    Dialogue dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //controls = new SimpleInput();

        if (dialogue == null)
        {
            dialogue = Dialogue.instance; // Get the instance of the Dialogue class
            if (dialogue == null)
            {
                Debug.LogError("Dialogue instance not found in Input. Make sure the Dialogue script is attached to a GameObject in the scene.");
            }
        }
    }

    // Interact action
    public void InteractAction(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the action was performed
        {
            Debug.Log("Interact action triggered.");
        }
    }
    // Objection action
    public void ObjectionAction(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the action was performed
        {
            Debug.Log("OBJECTION!!!!!");
        }
    }
    // Text forward action
    public void TextForwardAction(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the action was performed
        {
            dialogue.NextLineInput();
        }
    }
    
    // Text rollback action
    public void TextRollbackAction(InputAction.CallbackContext context)
    {
        if(context.performed) // Check if the action was performed
        {
            dialogue.PreviousLineInput();
        }
    }
}
