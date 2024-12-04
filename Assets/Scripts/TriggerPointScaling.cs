using UnityEngine;

public class TriggerPointScaling : MonoBehaviour
{
    private Vector3 originalScale;  // Store the original scale
    public float scaleMultiplier = 1.5f;  // How much bigger the trigger will get
    public float scaleDuration = 0.3f;  // Duration for how long the trigger stays scaled up

    private string keyPress;  // The key assigned to this trigger point

    void Start()
    {
        // Store the original scale of the trigger point when the game starts
        originalScale = transform.localScale;

        // Set the expected key based on the tag of the trigger point
        switch (gameObject.tag)
        {
            case "Number1":
                keyPress = "1";
                break;
            case "Number2":
                keyPress = "2";
                break;
            case "Number3":
                keyPress = "3";
                break;
            case "Number4":
                keyPress = "4";
                break;
            case "Number5":
                keyPress = "5";
                break;
            default:
                Debug.LogWarning("TriggerPoint tag not recognized!");
                break;
        }
    }

    void Update()
    {
        // Check for key press corresponding to this trigger point
        if (Input.GetKeyDown(keyPress))
        {
            ScaleTriggerPoint();  // Scale the trigger point
        }
    }

    // Method to scale up the trigger point temporarily
    public void ScaleTriggerPoint()
    {
        // Temporarily scale the trigger point
        transform.localScale *= scaleMultiplier;

        // Start the coroutine to reset the scale after the defined duration
        StartCoroutine(ResetScaleAfterDelay());
    }

    // Coroutine to reset the scale back to the original size after a delay
    private System.Collections.IEnumerator ResetScaleAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(scaleDuration);

        // Reset the trigger point's scale to the original size
        transform.localScale = originalScale;
    }
}
