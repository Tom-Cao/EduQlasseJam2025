using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReasonPanel : MonoBehaviour
{
    [Header("Positioning")]
    public Vector3 reasonPanelPosition;
    [SerializeField] private float speed = 5f;
    public bool hidden = true;

    [Header("Button")]
    [SerializeField] private Sprite buttonSprite;
    

    [Header("References")]
    public static ReasonPanel instance; // Singleton instance of the ReasonPanel class
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
        for (int i = 0; i < numOfStatements; i++)
        {
            // position the text object using the statementWidth and statementHeight
            float xPos = (i * statementWidth) + (statementWidth / 2) - (panelWidth / 2); // Center the text object
            float yPos = 0; // Center the text object

            // Create button
            GameObject button = new("StatementButton" + i, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image),
                typeof(UnityEngine.UI.Button)); 

            // Set the button's parent to the panel
            button.transform.SetParent(transform, false);

            // Set the button's position
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(xPos, yPos);
            buttonRect.sizeDelta = new Vector2(statementWidth - offset, statementHeight - offset); // Adjust size with offset

            // Set the button's image
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = Color.white; // Set the button's background color
            buttonImage.sprite = buttonSprite;
            buttonImage.type = Image.Type.Sliced; // Set the image type to sliced
            buttonImage.preserveAspect = true; // Preserve the aspect ratio of the image

            // Set the button's color
            ColorBlock colorBlock = button.GetComponent<UnityEngine.UI.Button>().colors;
            colorBlock.normalColor = Color.white; // Set the normal color of the button
            colorBlock.highlightedColor = Color.gray; // Set the highlighted color of the button
            colorBlock.pressedColor = Color.red; // Set the pressed color of the button
            button.GetComponent<UnityEngine.UI.Button>().colors = colorBlock; // Apply the color block to the button

            // Make a text object and set its text to the word bank string
            GameObject textObject = new("StatementText" + i, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            textObject.transform.SetParent(button.transform, false); // Set the text object as a child of the button
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
            text.text = wordBank[i]; // Set the text to the word bank string
            text.enableAutoSizing = true; // Enable auto-sizing for the text
            text.alignment = TextAlignmentOptions.Center; // Center the text
            text.color = Color.black; // Set the text color to black

            // Configure the Button component
            UnityEngine.UI.Button uiButton = button.GetComponent<UnityEngine.UI.Button>();
            uiButton.onClick.AddListener(() => TeacherManager.Instance.HandleReasonPanelChoice(text.text)); // Add a click listener
        }
    }

    public void Start()
    {
        BuildPanel(); // Build the panel when the script is loaded
    }

    public void Update()
    {
        // MAGIC NUMBER: world coordinates
        if (hidden)
            reasonPanelPosition = new Vector3(0, -30f, 0); // Move the panel down if hidden
        else
            reasonPanelPosition = new Vector3(0, -2f, 0); // Move the panel up if not hidden
        // move to reasonPanelPosition
        transform.position = Vector3.Lerp(transform.position, reasonPanelPosition, speed * Time.deltaTime); // Smoothly move the panel to the target position
    }
}