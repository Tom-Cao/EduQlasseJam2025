using UnityEngine;
using UnityEngine.Events;

public class ObjectionState : MonoBehaviour
{
    public static ObjectionState instance; // Singleton instance of the ObjectionState class
    
    public UnityEvent onObjectionStart; // Event triggered when the objection starts

    public UnityEvent onObjectionEnd; // Event triggered when the objection ends

    public UnityEvent onObjectionCorrect; // Event triggered when the objection is correct

    public UnityEvent onObjectionWrong; // Event triggered when the objection is wrong

    public ObjectionStateType currentState; // The type of objection state
    public enum ObjectionStateType
    {
        Dialogue, // No objection state
        Objection, // Objection state
        ObjectionCorrect,
        ObjectionWrong,
    }

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

    public void StartObjection()
    {
        currentState = ObjectionStateType.Objection; // Set the current state to Objection
    }

    public void EndObjection()
    {
        currentState = ObjectionStateType.Dialogue; // Set the current state back to Dialogue
    }
    public void CorrectObjection()
    {
        currentState = ObjectionStateType.ObjectionCorrect; // Set the current state to ObjectionCorrect
    }

    public void WrongObjection()
    {
        currentState = ObjectionStateType.ObjectionWrong; // Set the current state to ObjectionWrong
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = ObjectionStateType.Dialogue; // Initialize the current state to Dialogue
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
