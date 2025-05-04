using TMPro;
using UnityEngine;

public class ReasonPanel : MonoBehaviour
{
    [Header("Positioning")]
    public Vector3 reasonPanelPosition;
    [SerializeField] private float speed = 10f;
    public bool hidden = true;

    [Header("Text")]
    

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