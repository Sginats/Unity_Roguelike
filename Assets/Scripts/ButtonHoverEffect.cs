using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Settings")]
    public float hoverScale = 1.15f;
    public float animationSpeed = 10f;
    
    [Header("Audio Settings")]
    public AudioSource sfxAudioSource;
    public AudioClip hoverSound;
    
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        targetScale = originalScale * hoverScale;
        
        if (sfxAudioSource != null && hoverSound != null)
        {
            sfxAudioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        targetScale = originalScale;
    }
}
