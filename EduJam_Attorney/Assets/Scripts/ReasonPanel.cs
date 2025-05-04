using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReasonPanel : MonoBehaviour
{
    [Header("Positioning")]
    public Vector3 reasonPanelPosition;
    [SerializeField] private float speed = 10f;
    public bool hidden = true;
    [SerializeField] private RectTransform hiddenRectTransform;

    [Header("Button")]
    [SerializeField] private ReasonButton reasonButtonPrefab;

    [Header("Data")]
    [SerializeField] private float waitToShowResultsDuration = 3f;

    [Header("References")]
    public static ReasonPanel instance; // Singleton instance of the ReasonPanel class
    
    private List<ReasonButton> reasonButtons;
    private ReasonButton lastClickedReasonButton;
    private Coroutine waitToShowResultsRoutine;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the instance if it's null
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

    }

    public void BuildPanel()
    {
        gameObject.SetActive(true);
        
        Dialogue dialogue = Dialogue.instance;
        int currentLineIndex = dialogue.CurrentLineIndex; // Get the current line index from the dialogue
        // Get the word bank from current statement
        DialogueData currentDialogueData = dialogue.DialogueData[currentLineIndex]; // Get the current dialogue data
        string[] wordBank = currentDialogueData.WordBankStrings.ToArray(); // Get the word bank strings

        int numOfStatements = wordBank.Length;

        // get the width of the panel buttons
        RectTransform rectTransform = GetComponent<RectTransform>();
        float panelWidth = rectTransform.rect.width; // Get the width of the panel
        float statementWidth = panelWidth / numOfStatements; // Calculate the width of each statement
        
        // get the height of the panel buttons
        float panelHeight = rectTransform.rect.height; // Get the height of the panel
        float statementHeight = panelHeight / numOfStatements; // Calculate the height of each statement

        // Offset between each statement
        float offset = 0.5f; // Adjust this value to change the spacing between statements

        // for each statement, create a new button object and set its text to the statement
        reasonButtons = new List<ReasonButton>();
        for (int i = 0; i < numOfStatements; i++)
        {
            // position the text object using the statementWidth and statementHeight
            float xPos = (i * statementWidth) + (statementWidth / 2) - (panelWidth / 2); // Center the text object
            float yPos = 0; // Center the text object
            
            ReasonButton reasonButton = Instantiate(
                original: reasonButtonPrefab,
                parent: transform,
                worldPositionStays: false);
            
            reasonButton.SetButtonData(new ReasonButton.ButtonData
            {
                ButtonIndex = i,
                ButtonString = wordBank[i],
                XPosition = xPos,
                YPosition = yPos,
                ButtonWidth = statementWidth,
                ButtonHeight = statementHeight,
                Offset = offset,
                HiddenButtonPosition = hiddenRectTransform.position,
            });

            // Configure the Button component
            reasonButton.Button.onClick.AddListener(() =>
            {
                Debug.Log("reason button clicked");
                lastClickedReasonButton = reasonButton;
                TeacherManager.Instance.HandleReasonPanelChoice(reasonButton.Text, HandleAnswerCheckedByTeacher);
            });
            
            reasonButtons.Add(reasonButton);
        }
    }

    private void HandleAnswerCheckedByTeacher(bool isAnswerCorrect, Action onObjectionActionsCompleted)
    {
        foreach (var reasonButton in reasonButtons)
        {
            if (reasonButton == lastClickedReasonButton)
            {
                lastClickedReasonButton.SetButtonColor(isAnswerCorrect);
                continue;
            }
            
            reasonButton.HideButton();
        }

        if (waitToShowResultsRoutine != null)
        {
            StopCoroutine(waitToShowResultsRoutine);
        }
        
        waitToShowResultsRoutine = StartCoroutine(WaitToShowResultRoutine(onObjectionActionsCompleted));
    }
    
    private IEnumerator WaitToShowResultRoutine(Action onShowResultsCompleted)
    {
        yield return new WaitForSeconds(waitToShowResultsDuration);

        foreach (var reasonButton in reasonButtons)
        {
            Destroy(reasonButton.gameObject);
        }
        
        onShowResultsCompleted?.Invoke();
    }
    
    public void Start()
    {
        BuildPanel(); // Build the panel when the script is loaded
    }

    public void Update()
    {
        // MAGIC NUMBER: world coordinates
        if (hidden)
            reasonPanelPosition = new Vector3(0, -15f, 0); // Move the panel down if hidden
        else
            reasonPanelPosition = new Vector3(0, -2f, 0); // Move the panel up if not hidden
        // move to reasonPanelPosition
        transform.position = Vector3.Lerp(transform.position, reasonPanelPosition, speed * Time.deltaTime); // Smoothly move the panel to the target position
    }
}