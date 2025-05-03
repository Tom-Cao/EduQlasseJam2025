using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Grab input system
    SimpleInput controls;

    // Main dialogue system
    Dialogue dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controls = new SimpleInput();

        if (dialogue == null)
        {
            dialogue = Dialogue.instance; // Get the instance of the Dialogue class
            if (dialogue == null)
            {
                Debug.LogError("Dialogue instance not found in Input. Make sure the Dialogue script is attached to a GameObject in the scene.");
            }
        }

        // 3 actions: Text forward, Text rollback, Interact and Objection
        controls.Player.Interact.performed += ctx => InteractAction();
        controls.Player.Objection.performed += ctx => ObjectionAction();
        controls.Player.Next.performed += ctx => TextForwardAction();
        controls.Player.Previous.performed += ctx => TextRollbackAction();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // Interact action
    private void InteractAction()
    {
        
    }
    // Objection action
    private void ObjectionAction()
    {
       
    }
    // Text forward action
    private void TextForwardAction()
    {
        dialogue.NextLineInput();
    }
    // Text rollback action
    private void TextRollbackAction()
    {
        dialogue.PreviousLineInput();
    }
}
