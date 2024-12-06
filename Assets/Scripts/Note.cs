using UnityEngine.UI;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Transform triggerPoint;
    private string expectedKey; // The key this note expects to be pressed
    private bool isHit = false;
    private bool isMissed = false; // Prevent multiple "Missed note!" calls

    private float step; // Speed of the note movement
    private float timeToReach; // Time in seconds for the note to reach the trigger

    // Define the distance thresholds for different types of hits
    private float perfectHitThreshold = 20f;  // Perfect hit if distance
    private float closeHitThreshold = 40f;    // Close hit if distance
    public ScoreManager scoreManager;


    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        // Initialize the note's movement speed based on the time it should take to reach the trigger
        if (triggerPoint != null)
        {
            float distance = Vector3.Distance(transform.position, triggerPoint.position);
            step = distance / timeToReach;
        }

       
    }

    public void SetSpeed(float time)
    {
        timeToReach = time;
        if (triggerPoint != null)
        {
            float distance = Vector3.Distance(transform.position, triggerPoint.position);
            step = distance / timeToReach; // Calculate speed based on distance and time
        }
    }

    void Update()
    {
        // Skip update if already hit or missed
        if (isHit || isMissed) return;

        // Move the note towards the trigger point in world space
        if (triggerPoint != null)
        {
            Vector3 direction = (triggerPoint.position - transform.position).normalized;
            transform.position += direction * step * Time.deltaTime;

            // Check if the note has passed the trigger point, and miss it if so
            if (Vector3.Distance(transform.position, triggerPoint.position) < 0.1f)
            {
                if (!isMissed)
                {
                    Miss();
                }
            }
        }

        // Check for key press and proximity to the trigger point
        if (Input.GetKeyDown(expectedKey) && IsNearTrigger())
        {
            // If key is pressed near the trigger, hit the note
            Hit();
        }
    }

    public void SetPath(Transform path)
    {
        // Get the trigger point of the path
        triggerPoint = path.Find("TriggerPoint");

        // Set the expected key based on the tag of the trigger point
        switch (triggerPoint.tag)
        {
            case "Number1":
                expectedKey = "1";
                break;
            case "Number2":
                expectedKey = "2";
                break;
            case "Number3":
                expectedKey = "3";
                break;
            case "Number4":
                expectedKey = "4";
                break;
            case "Number5":
                expectedKey = "5";
                break;
            default:
                Debug.LogWarning("TriggerPoint tag not recognized!");
                break;
        }
    }

    void Hit()
    {
        if (isHit) return; // Prevent hitting a note multiple times
        isHit = true;
        transform.localScale *= 1.5f; // Make the note bigger
        Debug.Log("Hit note!");

        // Get the Image component
        Image img = GetComponent<Image>();
        if (img != null)
        {
            // Determine the hit type based on distance
            float distance = Vector3.Distance(transform.position, triggerPoint.position);

            // Perfect hit
            if (distance <= perfectHitThreshold)
            {
                img.color = Color.green; // Change to green for a perfect hit
                scoreManager.ScoreIncrease("Perfect");
            }
            // Close hit
            else if (distance <= closeHitThreshold)
            {
                img.color = Color.yellow; // Change to yellow for a close hit
                scoreManager.ScoreIncrease("Close");
            }
        }
        else
        {
            // Log a warning if no Image component is found
            Debug.LogWarning("Image component not found on the note!");
        }

        // Destroy the note immediately
        Destroy(gameObject, 0.2f);
    }

    void Miss()
    {
        if (isMissed) return; // Prevent multiple misses
        isMissed = true;
        Debug.Log("Missed note!");

        // Get the Image component
        Image img = GetComponent<Image>();
        if (img != null)
        {
            // Change color to red if the Image component is found
            img.color = Color.red;
        }
        else
        {
            // Log a warning if no Image component is found
            Debug.LogWarning("Image component not found on the note!");
        }

        // Destroy the note immediately
        Destroy(gameObject, 0.2f);
    }

    private bool IsNearTrigger()
    {
        // Check if the note is close to the trigger point
        float distance = Vector3.Distance(transform.position, triggerPoint.position);
        return triggerPoint != null && distance <= closeHitThreshold;
    }
}
