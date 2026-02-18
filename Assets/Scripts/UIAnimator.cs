using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    [Header("Animation Type")]
    public bool pulse = true;
    public bool rotate = false;
    public bool fadeInOut = false;
    
    [Header("Pulse Settings")]
    [Range(0.8f, 1.5f)]
    public float pulseMinScale = 0.95f;
    [Range(0.8f, 1.5f)]
    public float pulseMaxScale = 1.05f;
    public float pulseSpeed = 1f;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 50f;
    
    [Header("Fade Settings")]
    [Range(0.3f, 1f)]
    public float minAlpha = 0.7f;
    public float fadeSpeed = 1f;
    
    private Vector3 originalScale;
    private CanvasGroup canvasGroup;
    private float pulseTimer = 0f;
    private float fadeTimer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        
        // Add CanvasGroup if fade animation is enabled
        if (fadeInOut)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    void Update()
    {
        if (pulse)
        {
            AnimatePulse();
        }
        
        if (rotate)
        {
            AnimateRotation();
        }
        
        if (fadeInOut && canvasGroup != null)
        {
            AnimateFade();
        }
    }

    void AnimatePulse()
    {
        pulseTimer += Time.deltaTime * pulseSpeed;
        float scale = Mathf.Lerp(pulseMinScale, pulseMaxScale, 
                                 (Mathf.Sin(pulseTimer) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }

    void AnimateRotation()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void AnimateFade()
    {
        fadeTimer += Time.deltaTime * fadeSpeed;
        canvasGroup.alpha = Mathf.Lerp(minAlpha, 1f, 
                                       (Mathf.Sin(fadeTimer) + 1f) / 2f);
    }
}
