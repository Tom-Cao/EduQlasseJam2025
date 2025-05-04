using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox; // Reference to the dialogue box GameObject
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject objectionWord;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;

    // SINGLETON INSTANCE
    public static HUDManager instance;
    public void Awake()
    {
        if (instance == null) // Check if the instance is null
        {
            instance = this; // Assign this instance to the singleton instance
        }
        else if (instance != this) // If another instance already exists
        {
            Destroy(gameObject); // Destroy this GameObject to enforce the singleton pattern
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // null check
        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox"); // Find the dialogue box GameObject in the scene
            if (dialogueBox == null)
            {
                Debug.LogError("DialogueBox GameObject not found in the scene. Make sure it is present.");
            }
        }

        if (background == null)
        {
            background = GameObject.Find("Background"); // Find the background GameObject in the scene
            if (background == null)
            {
                Debug.LogError("Background GameObject not found in the scene. Make sure it is present.");
            }
        }

        if (objectionWord == null)
        {
            objectionWord = GameObject.Find("ObjectionWord"); // Find the objection word GameObject in the scene
            if (objectionWord == null)
            {
                Debug.LogError("ObjectionWord GameObject not found in the scene. Make sure it is present.");
            }
        }

        if (previousButton == null)
        {
            previousButton = GameObject.Find("PreviousButton"); // Find the previous button GameObject in the scene
            if (previousButton == null)
            {
                Debug.LogError("PreviousButton GameObject not found in the scene. Make sure it is present.");
            }
        }

        if (nextButton == null)
        {
            nextButton = GameObject.Find("NextButton"); // Find the next button GameObject in the scene
            if (nextButton == null)
            {
                Debug.LogError("NextButton GameObject not found in the scene. Make sure it is present.");
            }
        }
    }

    // Hide or unhide the dialogue box
    public void ShowHideDialogueBox(bool active)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(active); // Set the active state of the dialogue box GameObject
        }
        else
        {
            Debug.LogError("DialogueBox GameObject is not assigned or found in the scene.");
        }
    }

    // Hide or unhide the background
    public void ShowHideBackground(bool active)
    {
        if (background != null)
        {
            background.SetActive(active); // Set the active state of the background GameObject
        }
        else
        {
            Debug.LogError("Background GameObject is not assigned or found in the scene.");
        }
    }

    // Hide or unhide the objection word
    public void ShowHideObjectionWord(bool active)
    {
        if (objectionWord != null)
        {
            objectionWord.SetActive(active); // Set the active state of the objection word GameObject
        }
        else
        {
            Debug.LogError("ObjectionWord GameObject is not assigned or found in the scene.");
        }
    }

    // Hide or unhide the previous button
    public void ShowHidePreviousButton(bool active)
    {
        if (previousButton != null)
        {
            previousButton.SetActive(active); // Set the active state of the previous button GameObject
        }
        else
        {
            Debug.LogError("PreviousButton GameObject is not assigned or found in the scene.");
        }
    }

    // Hide or unhide the next button
    public void ShowHideNextButton(bool active)
    {
        if (nextButton != null)
        {
            nextButton.SetActive(active); // Set the active state of the next button GameObject
        }
        else
        {
            Debug.LogError("NextButton GameObject is not assigned or found in the scene.");
        }
    }

    // Hide or unhide the dialogue box and background
    public void ShowHideDialogueBoxAndBackground(bool active)
    {
        ShowHideDialogueBox(active); // Call the method to show/hide the dialogue box
        ShowHideBackground(active); // Call the method to show/hide the background
    }
}
