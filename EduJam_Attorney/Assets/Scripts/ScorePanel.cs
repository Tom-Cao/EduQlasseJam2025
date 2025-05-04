using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameState;  
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    public Vector3 scorePanelPosition;
    [SerializeField] private float speed = 5f;
    public bool hidden = true;

    public static ScorePanel instance; // Singleton instance

    private void Awake() 
    {
        // Singleton pattern
        if (instance == null) // Check if the instance is null
        {
            instance = this; // Assign this instance to the singleton instance
        }
        else if (instance != this) // If another instance already exists
        {
            Destroy(gameObject); // Destroy this GameObject to enforce the singleton pattern
        }

        if (gameState == null)
        {
            gameState = GameObject.Find("GameState").GetComponent<TextMeshProUGUI>();
            if (gameState == null)
            {
                Debug.LogError("GameState Text component not found in the scene.");
            }
        }

        if (scoreText == null)
        {
            scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            if (scoreText == null)
            {
                Debug.LogError("Score Text component not found in the scene.");
            }
        }

        if (highScoreText == null)
        {
            highScoreText = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
            if (highScoreText == null)
            {
                Debug.LogError("HighScore Text component not found in the scene.");
            }
        }

        if (restartButton == null)
        {
            restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
            if (restartButton == null)
            {
                Debug.LogError("RestartButton Button component not found in the scene.");
            }
        }

        if (exitButton == null)
        {
            exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
            if (exitButton == null)
            {
                Debug.LogError("ExitButton Button component not found in the scene.");
            }
        }

        // Subscribe to events
        restartButton.onClick.AddListener(() => HandleGameRestart());
        exitButton.onClick.AddListener(() => HandleGameExit());
    }

    private void Start()
    {
        hidden = true;
        highScoreText.text = "Meilleur Score: " + getHighScore().ToString();
    }

    private void Update() 
    {
        scoreText.text = PlayerSettings.instance.Score.ToString();
        // MAGIC NUMBER: world coordinates
        if (hidden)
            scorePanelPosition = new Vector3(0, 30f, 0); // Move the panel down if hidden
        else
            scorePanelPosition = new Vector3(0, 0f, 0); // Move the panel up if not hidden
        // move to reasonPanelPosition
        transform.position = Vector3.Lerp(transform.position, scorePanelPosition, speed * Time.deltaTime);
    }

    //Event system
    public void HandleGameRestart()
    {
        gameState.text = "";
        PlayerSettings.instance.Score = 0;

        // Load main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void HandleGameOver()
    {
        gameState.text = "Jeu Terminer!";
        TrySetNewHighScore(PlayerSettings.instance.Score);
        highScoreText.text = "Meilleur Score: " + getHighScore().ToString();
    }

    public void HandleGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        Application.Quit(); // Quit the application
#endif
    }

    // Player Refs Highscore
    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }

    public static int getHighScore()
    {
        return PlayerPrefs.GetInt("highscore");
    }

    public static bool TrySetNewHighScore(int score)
    {
        int currentHighScore = getHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}
