using UnityEngine;
using UnityEngine.UI;

public class TVController : MonoBehaviour
{
    [Header("TV Screen")]
    public GameObject tvScreen;
    public GameObject[] channels;
    
    [Header("Audio")]
    public AudioSource tvAudioSource;
    public Slider volumeSlider;
    
    [Header("UI Elements")]
    public Toggle powerToggle;
    public Toggle[] channelToggles;
    
    private int currentChannel = 0;
    private bool isPoweredOn = false;

    void Start()
    {
        // Initialize TV as off
        if (tvScreen != null)
            tvScreen.SetActive(false);
        
        // Set up volume slider
        if (volumeSlider != null && tvAudioSource != null)
        {
            volumeSlider.value = tvAudioSource.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
        
        // Disable channel toggles initially
        foreach (var toggle in channelToggles)
        {
            if (toggle != null)
                toggle.interactable = false;
        }
        
        // Initialize channels
        foreach (var channel in channels)
        {
            if (channel != null)
                channel.SetActive(false);
        }
    }

    public void TogglePower(bool value)
    {
        isPoweredOn = value;
        
        if (tvScreen != null)
            tvScreen.SetActive(value);
        
        // Enable/disable channel toggles based on power state
        foreach (var toggle in channelToggles)
        {
            if (toggle != null)
                toggle.interactable = value;
        }
        
        if (value)
        {
            // Turn on TV - show current channel
            ShowChannel(currentChannel);
        }
        else
        {
            // Turn off TV - hide all channels
            foreach (var channel in channels)
            {
                if (channel != null)
                    channel.SetActive(false);
            }
        }
        
        Debug.Log("TV Power: " + (value ? "ON" : "OFF"));
    }

    public void SwitchChannel(int channelIndex)
    {
        if (!isPoweredOn || channels == null || channelIndex < 0 || channelIndex >= channels.Length)
            return;
        
        currentChannel = channelIndex;
        ShowChannel(currentChannel);
        
        Debug.Log("Switched to channel: " + (channelIndex + 1));
    }

    private void ShowChannel(int channelIndex)
    {
        // Hide all channels
        foreach (var channel in channels)
        {
            if (channel != null)
                channel.SetActive(false);
        }
        
        // Show selected channel with bounds check
        if (channelIndex >= 0 && channelIndex < channels.Length && channels[channelIndex] != null)
        {
            channels[channelIndex].SetActive(true);
        }
    }

    public void ChangeVolume(float value)
    {
        if (tvAudioSource != null)
        {
            tvAudioSource.volume = value;
        }
        
        Debug.Log("Volume: " + (value * 100).ToString("F0") + "%");
    }
}
