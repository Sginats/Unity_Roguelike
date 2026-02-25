using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterHoverAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip hoverSound;
    
    [Header("Visual Feedback")]
    public bool scaleOnHover = true;
    public float hoverScale = 1.15f;
    public float animationSpeed = 8f;
    
    [Header("TV Controller Reference")]
    public TVController tvController;
    
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        if (audioSource == null)
        {
            audioSource = FindFirstObjectByType<AudioSource>();
        }
        
        if (tvController == null)
        {
            tvController = FindFirstObjectByType<TVController>();
        }
    }

    void Update()
    {
        if (scaleOnHover)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tvController != null && !tvController.IsPoweredOn())
            return;
        
        isHovering = true;
        
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
        
        if (scaleOnHover)
        {
            targetScale = originalScale * hoverScale;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        
        if (scaleOnHover)
        {
            targetScale = originalScale;
        }
    }
}
