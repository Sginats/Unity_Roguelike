using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    public float scaleOnClick = 0.9f;
    public float animationDuration = 0.2f;
    
    [Header("Audio Settings")]
    public AudioSource sfxAudioSource;
    public AudioClip clickSound;
    
    private Toggle toggle;
    private Vector3 originalScale;
    private bool isAnimating = false;
    private float animationTimer = 0f;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        originalScale = transform.localScale;
        
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }
    }

    void Update()
    {
        if (isAnimating)
        {
            animationTimer += Time.deltaTime;
            float progress = animationTimer / animationDuration;
            
            if (progress < 0.5f)
            {
                float scaleProgress = progress * 2f;
                transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleOnClick, scaleProgress);
            }
            else
            {
                float scaleProgress = (progress - 0.5f) * 2f;
                transform.localScale = Vector3.Lerp(originalScale * scaleOnClick, originalScale, scaleProgress);
            }
            
            if (progress >= 1f)
            {
                transform.localScale = originalScale;
                isAnimating = false;
                animationTimer = 0f;
            }
        }
    }

    void OnToggleChanged(bool value)
    {
        isAnimating = true;
        animationTimer = 0f;
        
        if (sfxAudioSource != null && clickSound != null)
        {
            sfxAudioSource.PlayOneShot(clickSound);
        }
    }
}
