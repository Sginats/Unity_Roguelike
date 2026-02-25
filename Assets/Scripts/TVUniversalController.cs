using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TVUniversalController : MonoBehaviour
{
    [Header("TV Screen References")]
    public GameObject tvOffOverlay;
    public GameObject[] channelObjects;
    
    [Header("Audio")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip[] channelMusic;
    
    [Header("UI Controls")]
    public Toggle powerToggle;
    public Slider volumeSlider;
    public TMP_Text channelNameText;
    public TMP_Text volumeText;
    
    [Header("Channel Names")]
    public string[] channelNames = new string[] { "Channel 1", "Channel 2" };
    
    private int currentChannelIndex = 0;
    private bool isPoweredOn = false;

    void Start()
    {
        SetInitialVolume();
        TurnOffTV();
        
        if (powerToggle != null && powerToggle.isOn)
        {
            TurnOnTV();
        }
    }

    void SetInitialVolume()
    {
        if (volumeSlider != null)
        {
            float volume = volumeSlider.value;
            
            if (musicAudioSource != null)
                musicAudioSource.volume = volume;
            
            if (sfxAudioSource != null)
                sfxAudioSource.volume = volume;
            
            UpdateVolumeText(volume);
            
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    public void TogglePower(bool isOn)
    {
        isPoweredOn = isOn;
        
        if (isOn)
        {
            TurnOnTV();
        }
        else
        {
            TurnOffTV();
        }
    }

    void TurnOnTV()
    {
        isPoweredOn = true;
        
        if (tvOffOverlay != null)
            tvOffOverlay.SetActive(false);
        
        ShowChannel(currentChannelIndex);
        PlayChannelMusic(currentChannelIndex);
        UpdateChannelDisplay();
    }

    void TurnOffTV()
    {
        isPoweredOn = false;
        
        if (tvOffOverlay != null)
            tvOffOverlay.SetActive(true);
        
        HideAllChannels();
        StopMusic();
        UpdateChannelDisplay();
    }

    public void NextChannel()
    {
        if (!isPoweredOn) return;
        
        currentChannelIndex = (currentChannelIndex + 1) % channelObjects.Length;
        SwitchToChannel(currentChannelIndex);
    }

    public void PreviousChannel()
    {
        if (!isPoweredOn) return;
        
        currentChannelIndex--;
        if (currentChannelIndex < 0)
            currentChannelIndex = channelObjects.Length - 1;
        
        SwitchToChannel(currentChannelIndex);
    }

    void SwitchToChannel(int channelIndex)
    {
        HideAllChannels();
        ShowChannel(channelIndex);
        PlayChannelMusic(channelIndex);
        UpdateChannelDisplay();
    }

    void ShowChannel(int channelIndex)
    {
        if (channelIndex < 0 || channelIndex >= channelObjects.Length)
            return;
        
        if (channelObjects[channelIndex] != null)
            channelObjects[channelIndex].SetActive(true);
    }

    void HideAllChannels()
    {
        foreach (GameObject channel in channelObjects)
        {
            if (channel != null)
                channel.SetActive(false);
        }
    }

    void PlayChannelMusic(int channelIndex)
    {
        if (musicAudioSource == null) return;
        
        musicAudioSource.Stop();
        
        if (channelMusic != null && channelIndex < channelMusic.Length && channelMusic[channelIndex] != null)
        {
            musicAudioSource.clip = channelMusic[channelIndex];
            musicAudioSource.Play();
        }
    }

    void StopMusic()
    {
        if (musicAudioSource != null)
            musicAudioSource.Stop();
    }

    void OnVolumeChanged(float value)
    {
        if (musicAudioSource != null)
            musicAudioSource.volume = value;
        
        if (sfxAudioSource != null)
            sfxAudioSource.volume = value;
        
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        if (volumeText != null)
        {
            volumeText.text = $"Volume: {(value * 100):F0}%";
        }
    }

    void UpdateChannelDisplay()
    {
        if (channelNameText != null)
        {
            if (isPoweredOn && currentChannelIndex < channelNames.Length)
            {
                channelNameText.text = channelNames[currentChannelIndex];
            }
            else
            {
                channelNameText.text = "---";
            }
        }
    }

    public bool IsPoweredOn()
    {
        return isPoweredOn;
    }
}
