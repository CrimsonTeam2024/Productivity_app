using UnityEngine;
using System.Collections;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;    // Duration of one shake movement
    [SerializeField] private float shakeOffset = 30f;      // How far to shake horizontally
    
    private Vector3 originalPosition;
    private bool isShaking = false;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        if (!isShaking)
        {
            StartCoroutine(DirectionalShakeCoroutine());
        }
    }

    private IEnumerator DirectionalShakeCoroutine()
    {
        isShaking = true;
        
        // Perform shake pattern 2 times
        for (int cycle = 0; cycle < 2; cycle++)
        {
            // Shake left
            float elapsedTime = 0f;
            while (elapsedTime < shakeDuration)
            {
                float progress = elapsedTime / shakeDuration;
                float offset = Mathf.Lerp(0, -shakeOffset, progress);
                transform.localPosition = originalPosition + new Vector3(offset, 0, 0);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            // Shake right
            elapsedTime = 0f;
            while (elapsedTime < shakeDuration)
            {
                float progress = elapsedTime / shakeDuration;
                float offset = Mathf.Lerp(-shakeOffset, shakeOffset, progress);
                transform.localPosition = originalPosition + new Vector3(offset, 0, 0);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            // Return to center
            elapsedTime = 0f;
            while (elapsedTime < shakeDuration)
            {
                float progress = elapsedTime / shakeDuration;
                float offset = Mathf.Lerp(shakeOffset, 0, progress);
                transform.localPosition = originalPosition + new Vector3(offset, 0, 0);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        
        // Ensure we end at the original position
        transform.localPosition = originalPosition;
        isShaking = false;
    }

}