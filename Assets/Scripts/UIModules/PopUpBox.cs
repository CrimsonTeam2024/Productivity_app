using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpBox : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform popUpPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI congratMessage;
    
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float targetScreenPercentage = 0.25f; // 1/4 of screen
    
    private Vector2 targetSize;
    private CanvasScaler canvasScaler;
    private bool isAnimating = false;

    private void Awake()
    {
        // Get canvas scaler for reference resolution
        canvasScaler = GetComponentInParent<CanvasScaler>();
        
        // Setup close button
        closeButton.onClick.AddListener(HideBanner);
        
        // Initialize banner
        popUpPanel.localScale = Vector3.zero;
        gameObject.SetActive(false);
        
        // Calculate target size (25% of screen)
        if (canvasScaler != null)
        {
            float width = canvasScaler.referenceResolution.x * targetScreenPercentage;
            float height = width * 0.5f; // 2:1 aspect ratio, adjust as needed
            targetSize = new Vector2(width, height);
            popUpPanel.sizeDelta = targetSize;
        }
    }

    public void ShowBanner(string message = "")
    {
        if (!isAnimating)
        {
            if (!string.IsNullOrEmpty(message))
            {
                congratMessage.text = message;
            }
            
            gameObject.SetActive(true);
            StartCoroutine(AnimateBanner(true));
        }
    }

    public void HideBanner()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateBanner(false));
        }
    }

    private System.Collections.IEnumerator AnimateBanner(bool show)
    {
        isAnimating = true;
        float elapsed = 0f;
        
        Vector3 startScale = show ? Vector3.zero : Vector3.one;
        Vector3 endScale = show ? Vector3.one : Vector3.zero;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / animationDuration;
            
            // Smooth animation curve
            float smoothProgress = Mathf.SmoothStep(0, 1, progress);
            
            // Apply scale
            popUpPanel.localScale = Vector3.Lerp(startScale, endScale, smoothProgress);
            
            yield return null;
        }

        // Ensure we reach the exact target scale
        popUpPanel.localScale = endScale;
        
        if (!show)
        {
            gameObject.SetActive(false);
        }
        
        isAnimating = false;
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(HideBanner);
    }
}