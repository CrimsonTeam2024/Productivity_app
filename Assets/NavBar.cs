using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{

    [Header("UI Settings")]
    [SerializeField] private RectTransform viewsParent;
    [SerializeField] int currentViewIndex = 0;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private bool enableCircularNavigation = true;

    private bool isTransitioning = false;
    private float elementWidth = 1080f;
    private int totalElements = 4;

    public void TransitionToIndex(int targetIndex)
    {
        if (isTransitioning)
            return;

        if (targetIndex == currentViewIndex)
            return;

        // Calculate the shortest path for circular navigation
        int directDistance = Mathf.Abs(targetIndex - currentViewIndex);
        int wrappedDistance = totalElements - directDistance;
        
        if (enableCircularNavigation && directDistance > wrappedDistance)
        {
            // Determine if we should wrap forward or backward
            bool wrapForward = targetIndex < currentViewIndex;
            StartCoroutine(AnimateCircularTransition(targetIndex, wrapForward));
        }
        else
        {
            // Standard linear transition
            StartCoroutine(AnimateTransition(targetIndex));
        }
    }

    private IEnumerator AnimateCircularTransition(int targetIndex, bool wrapForward)
    {
        isTransitioning = true;
        float startX = viewsParent.localPosition.x;
        float targetX = -targetIndex * elementWidth;

        // Calculate wrapped position
        float wrappedOffset = totalElements * elementWidth;
        float transitionalX = wrapForward ? targetX + wrappedOffset : targetX - wrappedOffset;
        
        // First, instantly move to the transitional position
        if (wrapForward)
        {
            viewsParent.localPosition = new Vector2(startX - wrappedOffset, viewsParent.localPosition.y);
            startX = viewsParent.localPosition.x;
        }
        else
        {
            viewsParent.localPosition = new Vector2(startX + wrappedOffset, viewsParent.localPosition.y);
            startX = viewsParent.localPosition.x;
        }

        // Then animate to the target position
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / transitionDuration;
            float curveValue = transitionCurve.Evaluate(normalizedTime);
            
            float newX = Mathf.Lerp(startX, targetX, curveValue);
            viewsParent.localPosition = new Vector2(newX, viewsParent.localPosition.y);
            
            yield return null;
        }

        // Ensure we end up exactly at the target position
        viewsParent.localPosition = new Vector2(targetX, viewsParent.localPosition.y);
        currentViewIndex = targetIndex;
        isTransitioning = false;
    }

    private IEnumerator AnimateTransition(int targetIndex)
    {
        isTransitioning = true;
        float startX = viewsParent.localPosition.x;
        float targetX = -targetIndex * elementWidth;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / transitionDuration;
            float curveValue = transitionCurve.Evaluate(normalizedTime);
            
            float newX = Mathf.Lerp(startX, targetX, curveValue);
            viewsParent.localPosition = new Vector2(newX, viewsParent.localPosition.y);
            
            yield return null;
        }

        // Ensure we end up exactly at the target position
        viewsParent.localPosition = new Vector2(targetX, viewsParent.localPosition.y);
        currentViewIndex = targetIndex;
        isTransitioning = false;
    }

    public void SetTransitionDuration(float duration)
    {
        transitionDuration = Mathf.Max(0.1f, duration);
    }

    public void SetCircularNavigation(bool enable)
    {
        enableCircularNavigation = enable;
    }
}
