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
}
