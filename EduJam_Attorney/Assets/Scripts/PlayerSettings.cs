using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public enum PitchLevels
    {
        Low,
        Medium,
        High
    }

    public PitchLevels pitchLevel; // Enum to set the pitch level

    public static PlayerSettings instance; // Singleton instance of the PlayerSettings class

    [SerializeField] private int score = 0; // Variable to store the score
    public int Score
    {
        get { return score; } // Getter for the score
        set { score = value; } // Setter for the score
    }

    void Awake()
    {
        instance = this; // Assign the instance of the PlayerSettings class
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add Score
    public void AddScore(int amount)
    {
        score += amount; // Increase the score by the specified amount
    }
}
