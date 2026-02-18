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

    void Start()
    {
        originalScale = transform.localScale;
        
        // If no audio source is assigned, try to get it from this object
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        // If still no audio source, search in parent or log warning
        if (audioSource == null)
        {
            audioSource = GetComponentInParent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning($"CharacterHoverAudio on {gameObject.name}: No AudioSource found. Please assign one in the inspector.");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
        // Reset scale
        if (scaleOnHover)
        {
            transform.localScale = originalScale;
        }
    }
}
