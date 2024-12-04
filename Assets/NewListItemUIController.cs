using UnityEngine;



public class NewListItemUIController : MonoBehaviour
{

    // Get all the input fields (text or otherwise) from the NewListItemPanel GameObject

    // Expose methods that validate whether user input in input fields is valid

    // Create methods for doing stuff with UI in the case of valid or invalid input
 [SerializeField] private GameObject newListItemPanel;

    // Method to show the NewListItemPanel with animation
    public void ShowNewListItemPanel()
    {
        if (newListItemPanel != null)
        {
            // Make sure the panel is active
            newListItemPanel.SetActive(true);

            // Start the animation
            StartCoroutine(AnimateNewListItemPanel());
        }
        else
        {
            Debug.LogError("NewListItemPanel is not assigned in the Inspector.");
        }
    }

    // Coroutine for animating the NewListItemPanel
    private System.Collections.IEnumerator AnimateNewListItemPanel()
    {
        // Ensure the panel starts at scale 0
        newListItemPanel.transform.localScale = Vector3.zero;

        // Target scale (fully visible)
        Vector3 targetScale = Vector3.one;

        // Animation duration
        float duration = 0.5f;
        float elapsedTime = 0f;

        // Animate the scale
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate scale over time
            newListItemPanel.transform.localScale = Vector3.Lerp(
                Vector3.zero, 
                targetScale, 
                elapsedTime / duration
            );

            yield return null; // Wait until the next frame
        }

        // Ensure final scale is exactly 1
        newListItemPanel.transform.localScale = Vector3.one;
    }

    // Methods for input validation (to be implemented)
    public bool ValidateInput()
    {
        // Placeholder for input validation logic
        Debug.Log("Validating input...");
        return true;
    }

    // Method for handling valid input
    public void HandleValidInput()
    {
        Debug.Log("Handling valid input...");
        // Logic for valid input
    }

    // Method for handling invalid input
    public void HandleInvalidInput()
    {
        Debug.Log("Handling invalid input...");
        // Logic for invalid input
    }
}

