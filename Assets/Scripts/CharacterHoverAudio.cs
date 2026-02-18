using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterHoverAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip hoverSound;
    
    [Header("Visual Feedback (Optional)")]
    public bool scaleOnHover = true;
    public float hoverScale = 1.1f;
    
    private Vector3 originalScale;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
        
        // If no audio source is assigned, try to get it from this object
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        // If still no audio source, try to find one in the scene
        if (audioSource == null)
        {
            audioSource = FindFirstObjectByType<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        
        // Play hover sound
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
        
        // Scale up on hover
        if (scaleOnHover)
        {
            transform.localScale = originalScale * hoverScale;
        }
        
        Debug.Log("Hovering over: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        
        // Reset scale
        if (scaleOnHover)
        {
            transform.localScale = originalScale;
        }
    }
}
