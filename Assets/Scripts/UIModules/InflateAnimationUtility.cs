using System.Collections;
using UnityEngine;

public class InflateAnimationUtility : MonoBehaviour
{
    public float animationDuration = 1f;
    public bool isAnimating = false;


    public IEnumerator Animate(bool show)
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
            transform.localScale = Vector3.Lerp(startScale, endScale, smoothProgress);
            
            yield return null;
        }

        // Ensure we reach the exact target scale
        transform.localScale = endScale;
        
        if (!show)
        {
            gameObject.SetActive(false);
        }
        
        isAnimating = false;
    }
    
}