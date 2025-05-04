using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ReasonButton : MonoBehaviour
{
    private enum ButtonState
    {
        Visible,
        Hiding,
        Hidden,
    }
    
    public struct ButtonData
    {
        public int ButtonIndex;
        public string ButtonString;
        public float XPosition;
        public float YPosition;
        public float ButtonWidth;
        public float ButtonHeight;
        public float Offset;
        public Vector2 HiddenButtonPosition;
    }
    
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;
    
    [Header("Image Data")]
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private Color defaultImageColor = Color.white;

    [Header("Answer Data")]
    [SerializeField] private Color correctAnswerButtonColor = Color.green;
    [SerializeField] private Color incorrectAnswerButtonColor = Color.red;

    private ButtonData buttonData;
    private ButtonState currentButtonState;
    private Vector2 hidingTrajectory;
    
    private const string kButtonText = "Button ";

    public Button Button => button;
    public string Text => text.text;
    
    public void SetButtonData(ButtonData buttonData)
    {
        this.buttonData = buttonData;
        currentButtonState = ButtonState.Visible;
        
        gameObject.name = kButtonText + this.buttonData.ButtonIndex;
        
        SetButtonPosition();
        SetButtonColors();
        SetButtonText();
    }

    private void SetButtonPosition()
    {
        var offset = buttonData.Offset;
        rectTransform.anchoredPosition = new Vector2(buttonData.XPosition, buttonData.YPosition);
        rectTransform.sizeDelta = new Vector2(buttonData.ButtonWidth - offset, buttonData.ButtonHeight - offset);
    }
    
    private void SetButtonColors()
    {
        image.color = defaultImageColor;
        image.sprite = buttonSprite;
        image.type = Image.Type.Sliced;
        image.preserveAspect = true;
    }
    
    private void SetButtonText()
    {
        text.text = buttonData.ButtonString;
        text.enableAutoSizing = true;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.black;
    }

    public void SetButtonColor(bool isAnswerCorrect)
    {
        image.color = isAnswerCorrect ? correctAnswerButtonColor : incorrectAnswerButtonColor;
    }

    public void HideButton()
    {
        currentButtonState = ButtonState.Hiding;
        hidingTrajectory = buttonData.HiddenButtonPosition - (Vector2)transform.position;
    }
    
    public void Update()
    {
        if (currentButtonState is ButtonState.Visible or ButtonState.Hidden)
        {
            return;
        }
        
        var currentPosition = transform.position;
        if (Mathf.Approximately(currentPosition.y, buttonData.HiddenButtonPosition.y))
        {
            currentButtonState = ButtonState.Hidden;
            return;
        }
        
        currentPosition.y += hidingTrajectory.y * Time.deltaTime;
        transform.position = currentPosition;
    }
}
