using UnityEngine;

public class SceneSetupHelper : MonoBehaviour
{
    [Header("Quick Reference")]
    [TextArea(10, 20)]
    public string setupInstructions = 
        "TV SIMULATION SETUP HELPER\n\n" +
        "1. Open TV.unity scene\n" +
        "2. Find 'TV Manager' GameObject\n" +
        "3. Assign all references in TVController\n" +
        "4. Create channels with characters\n" +
        "5. Connect UI events\n" +
        "6. Test in Play Mode\n\n" +
        "See README.md and Setup Guide in Pages for detailed instructions.";

    [Header("Scene Validation")]
    public bool checkReferences = false;

    void Start()
    {
        if (checkReferences)
        {
            ValidateScene();
        }
    }

    [ContextMenu("Validate Scene Setup")]
    void ValidateScene()
    {
        TVController tvController = FindFirstObjectByType<TVController>();
        
        if (tvController == null)
        {
            Debug.LogError("❌ TVController not found! Add TVController script to TV Manager GameObject.");
            return;
        }

        Debug.Log("✅ TVController found.");

        if (tvController.musicAudioSource == null)
            Debug.LogWarning("⚠️ Music Audio Source not assigned in TVController.");
        else
            Debug.Log("✅ Music Audio Source assigned.");

        if (tvController.sfxAudioSource == null)
            Debug.LogWarning("⚠️ SFX Audio Source not assigned in TVController.");
        else
            Debug.Log("✅ SFX Audio Source assigned.");

        if (tvController.powerToggle == null)
            Debug.LogWarning("⚠️ Power Toggle not assigned in TVController.");
        else
            Debug.Log("✅ Power Toggle assigned.");

        if (tvController.nextChannelButton == null)
            Debug.LogWarning("⚠️ Next Channel Button not assigned in TVController.");
        else
            Debug.Log("✅ Next Channel Button assigned.");

        if (tvController.previousChannelButton == null)
            Debug.LogWarning("⚠️ Previous Channel Button not assigned in TVController.");
        else
            Debug.Log("✅ Previous Channel Button assigned.");

        if (tvController.volumeSlider == null)
            Debug.LogWarning("⚠️ Volume Slider not assigned in TVController.");
        else
            Debug.Log("✅ Volume Slider assigned.");

        if (tvController.channels == null || tvController.channels.Length == 0)
            Debug.LogWarning("⚠️ No channels configured in TVController.");
        else
            Debug.Log($"✅ {tvController.channels.Length} channel(s) configured.");

        ValidateChannels(tvController);

        Debug.Log("\n📋 Scene validation complete! Check console for any warnings.");
    }

    void ValidateChannels(TVController tvController)
    {
        if (tvController.channels == null) return;

        for (int i = 0; i < tvController.channels.Length; i++)
        {
            ChannelData channel = tvController.channels[i];
            
            if (channel == null)
            {
                Debug.LogWarning($"⚠️ Channel {i} is null.");
                continue;
            }

            Debug.Log($"\n📺 Channel {i}: {channel.channelName}");

            if (channel.channelObject == null)
                Debug.LogWarning($"  ⚠️ Channel Object not assigned.");
            else
                Debug.Log($"  ✅ Channel Object assigned.");

            if (channel.backgroundImage == null)
                Debug.LogWarning($"  ⚠️ Background Image not assigned.");
            else
                Debug.Log($"  ✅ Background Image assigned.");

            if (channel.backgroundMusic == null)
                Debug.LogWarning($"  ⚠️ Background Music not assigned.");
            else
                Debug.Log($"  ✅ Background Music assigned.");

            if (channel.characters == null || channel.characters.Length == 0)
                Debug.LogWarning($"  ⚠️ No characters assigned (need 3+).");
            else if (channel.characters.Length < 3)
                Debug.LogWarning($"  ⚠️ Only {channel.characters.Length} character(s) assigned (need 3+).");
            else
                Debug.Log($"  ✅ {channel.characters.Length} character(s) assigned.");

            ValidateCharacters(channel.characters, i);
        }
    }

    void ValidateCharacters(GameObject[] characters, int channelIndex)
    {
        if (characters == null) return;

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                Debug.LogWarning($"    ⚠️ Character {i} is null.");
                continue;
            }

            CharacterHoverAudio hoverAudio = characters[i].GetComponent<CharacterHoverAudio>();
            
            if (hoverAudio == null)
            {
                Debug.LogWarning($"    ⚠️ Character '{characters[i].name}' missing CharacterHoverAudio script.");
            }
            else
            {
                Debug.Log($"    ✅ Character '{characters[i].name}' has CharacterHoverAudio.");
                
                if (hoverAudio.hoverSound == null)
                    Debug.LogWarning($"      ⚠️ No hover sound assigned.");
                else
                    Debug.Log($"      ✅ Hover sound assigned.");
            }
        }
    }

    [ContextMenu("Show Available Audio")]
    void ShowAvailableAudio()
    {
        Debug.Log("🔊 Available Audio Files:");
        Debug.Log("Music: MrBean.mp3, ThemeSong.mp3, Slider.mp3");
        Debug.Log("SFX: Klikskis.wav, DM-CGS-19.wav, DM-CGS-22.wav, hurt.mp3, death.mp3");
    }

    [ContextMenu("Show Available Images")]
    void ShowAvailableImages()
    {
        Debug.Log("🖼️ Available Images:");
        Debug.Log("Characters: Bean.png, Policists.png, Sieviete.png, Tante.png, Lacis.png, Mase.png");
        Debug.Log("Backgrounds: Road.jpg, gameover.jpg, TV.png");
        Debug.Log("UI: TVREMOTE.png");
    }
}
