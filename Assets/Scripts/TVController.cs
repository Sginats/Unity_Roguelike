using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TVController : MonoBehaviour
{
    [Header("TV Screen")]
    public GameObject tvScreen;
    public Image tvScreenImage;
    public Sprite offScreenSprite;
    public ChannelData[] channels;
    
    [Header("Audio")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    
    [Header("UI Elements")]
    public Toggle powerToggle;
    public Button nextChannelButton;
    public Button previousChannelButton;
    public TMP_Dropdown channelDropdown;
    public TMP_Text channelNameText;
    public TMP_Text statusText;
    
    [Header("Transition Settings")]
    public float transitionDuration = 0.3f;
    public CanvasGroup transitionOverlay;
    
    private int currentChannelIndex = 0;
    private bool isPoweredOn = false;
    private bool isTransitioning = false;

    void Start()
    {
        InitializeTV();
        SetupVolumeControl();
        SetupChannelDropdown();
        UpdateUI();
    }

    void InitializeTV()
    {
        isPoweredOn = false;
        
        if (tvScreen != null)
            tvScreen.SetActive(false);
        
        if (tvScreenImage != null && offScreenSprite != null)
            tvScreenImage.sprite = offScreenSprite;
        
        if (musicAudioSource != null)
        {
            musicAudioSource.loop = true;
            musicAudioSource.Stop();
        }
        
        foreach (var channel in channels)
        {
            if (channel != null && channel.channelObject != null)
            {
                channel.channelObject.SetActive(false);
                DisableChannelInteraction(channel);
            }
        }
        
        if (nextChannelButton != null)
            nextChannelButton.interactable = false;
        
        if (previousChannelButton != null)
            previousChannelButton.interactable = false;
        
        if (channelDropdown != null)
            channelDropdown.interactable = false;
    }

    void SetupVolumeControl()
    {
        if (volumeSlider != null && musicAudioSource != null)
        {
            volumeSlider.value = musicAudioSource.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
            UpdateVolumeText(musicAudioSource.volume);
        }
    }

    void SetupChannelDropdown()
    {
        if (channelDropdown != null && channels.Length > 0)
        {
            channelDropdown.ClearOptions();
            
            System.Collections.Generic.List<string> channelNames = new System.Collections.Generic.List<string>();
            for (int i = 0; i < channels.Length; i++)
            {
                channelNames.Add(channels[i].channelName);
            }
            
            channelDropdown.AddOptions(channelNames);
            channelDropdown.onValueChanged.AddListener(OnDropdownChannelChanged);
        }
    }

    public void TogglePower(bool value)
    {
        isPoweredOn = value;
        
        if (value)
        {
            TurnOnTV();
        }
        else
        {
            TurnOffTV();
        }
        
        UpdateUI();
    }

    void TurnOnTV()
    {
        if (tvScreen != null)
            tvScreen.SetActive(true);
        
        if (nextChannelButton != null)
            nextChannelButton.interactable = true;
        
        if (previousChannelButton != null)
            previousChannelButton.interactable = true;
        
        if (channelDropdown != null)
            channelDropdown.interactable = true;
        
        ShowChannel(currentChannelIndex);
        
        if (statusText != null)
            statusText.text = "TV: ON";
    }

    void TurnOffTV()
    {
        if (musicAudioSource != null)
            musicAudioSource.Stop();
        
        foreach (var channel in channels)
        {
            if (channel != null && channel.channelObject != null)
            {
                channel.channelObject.SetActive(false);
                DisableChannelInteraction(channel);
            }
        }
        
        if (tvScreenImage != null && offScreenSprite != null)
            tvScreenImage.sprite = offScreenSprite;
        
        if (tvScreen != null)
            tvScreen.SetActive(false);
        
        if (nextChannelButton != null)
            nextChannelButton.interactable = false;
        
        if (previousChannelButton != null)
            previousChannelButton.interactable = false;
        
        if (channelDropdown != null)
            channelDropdown.interactable = false;
        
        if (statusText != null)
            statusText.text = "TV: OFF";
    }

    public void NextChannel()
    {
        if (!isPoweredOn || isTransitioning || channels.Length == 0)
            return;
        
        currentChannelIndex = (currentChannelIndex + 1) % channels.Length;
        StartCoroutine(TransitionToChannel(currentChannelIndex));
    }

    public void PreviousChannel()
    {
        if (!isPoweredOn || isTransitioning || channels.Length == 0)
            return;
        
        currentChannelIndex--;
        if (currentChannelIndex < 0)
            currentChannelIndex = channels.Length - 1;
        
        StartCoroutine(TransitionToChannel(currentChannelIndex));
    }

    void OnDropdownChannelChanged(int index)
    {
        if (!isPoweredOn || isTransitioning)
            return;
        
        currentChannelIndex = index;
        StartCoroutine(TransitionToChannel(currentChannelIndex));
    }

    System.Collections.IEnumerator TransitionToChannel(int channelIndex)
    {
        isTransitioning = true;
        
        if (transitionOverlay != null)
        {
            float elapsed = 0f;
            while (elapsed < transitionDuration / 2)
            {
                elapsed += Time.deltaTime;
                transitionOverlay.alpha = Mathf.Lerp(0, 1, elapsed / (transitionDuration / 2));
                yield return null;
            }
        }
        
        ShowChannel(channelIndex);
        
        if (transitionOverlay != null)
        {
            float elapsed = 0f;
            while (elapsed < transitionDuration / 2)
            {
                elapsed += Time.deltaTime;
                transitionOverlay.alpha = Mathf.Lerp(1, 0, elapsed / (transitionDuration / 2));
                yield return null;
            }
            transitionOverlay.alpha = 0;
        }
        
        isTransitioning = false;
    }

    void ShowChannel(int channelIndex)
    {
        if (channelIndex < 0 || channelIndex >= channels.Length)
            return;
        
        foreach (var channel in channels)
        {
            if (channel != null && channel.channelObject != null)
            {
                channel.channelObject.SetActive(false);
                DisableChannelInteraction(channel);
            }
        }
        
        ChannelData selectedChannel = channels[channelIndex];
        
        if (selectedChannel.channelObject != null)
        {
            selectedChannel.channelObject.SetActive(true);
            EnableChannelInteraction(selectedChannel);
        }
        
        if (tvScreenImage != null && selectedChannel.backgroundImage != null)
        {
            tvScreenImage.sprite = selectedChannel.backgroundImage;
        }
        
        if (musicAudioSource != null)
        {
            musicAudioSource.Stop();
            
            if (selectedChannel.backgroundMusic != null)
            {
                musicAudioSource.clip = selectedChannel.backgroundMusic;
                musicAudioSource.Play();
            }
        }
        
        UpdateUI();
    }

    void EnableChannelInteraction(ChannelData channel)
    {
        if (channel.characters != null)
        {
            foreach (var character in channel.characters)
            {
                if (character != null)
                {
                    CharacterHoverAudio hoverScript = character.GetComponent<CharacterHoverAudio>();
                    if (hoverScript != null)
                    {
                        hoverScript.enabled = true;
                    }
                }
            }
        }
    }

    void DisableChannelInteraction(ChannelData channel)
    {
        if (channel.characters != null)
        {
            foreach (var character in channel.characters)
            {
                if (character != null)
                {
                    CharacterHoverAudio hoverScript = character.GetComponent<CharacterHoverAudio>();
                    if (hoverScript != null)
                    {
                        hoverScript.enabled = false;
                    }
                }
            }
        }
    }

    public void ChangeVolume(float value)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = value;
        }
        
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = value;
        }
        
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        if (volumeText != null)
        {
            volumeText.text = "Volume: " + (value * 100).ToString("F0") + "%";
        }
    }

    void UpdateUI()
    {
        if (channelNameText != null && isPoweredOn && currentChannelIndex >= 0 && currentChannelIndex < channels.Length)
        {
            channelNameText.text = channels[currentChannelIndex].channelName;
        }
        else if (channelNameText != null)
        {
            channelNameText.text = "---";
        }
        
        if (channelDropdown != null && isPoweredOn)
        {
            channelDropdown.value = currentChannelIndex;
        }
    }

    public bool IsPoweredOn()
    {
        return isPoweredOn;
    }
}

[System.Serializable]
public class ChannelData
{
    public string channelName;
    public GameObject channelObject;
    public Sprite backgroundImage;
    public AudioClip backgroundMusic;
    public GameObject[] characters;
}
