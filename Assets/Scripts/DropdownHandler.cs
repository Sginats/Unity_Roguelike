using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown dropdown;
    public GameObject feedbackText;
    
    [Header("Options")]
    public string[] optionLabels;
    
    void Start()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        
        // Clear existing options
        if (dropdown != null)
        {
            dropdown.ClearOptions();
            
            // Add new options if provided
            if (optionLabels != null && optionLabels.Length > 0)
            {
                dropdown.AddOptions(new System.Collections.Generic.List<string>(optionLabels));
            }
            
            // Add listener for dropdown changes
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }

    void OnDropdownValueChanged(int index)
    {
        if (dropdown != null)
        {
            string selectedOption = dropdown.options[index].text;
            Debug.Log("Selected option: " + selectedOption);
            
            // Update feedback text if available
            if (feedbackText != null)
            {
                TMP_Text textComponent = feedbackText.GetComponent<TMP_Text>();
                if (textComponent != null)
                {
                    textComponent.text = "Selected: " + selectedOption;
                }
            }
        }
    }
    
    public void SetDropdownValue(int index)
    {
        if (dropdown != null && index >= 0 && index < dropdown.options.Count)
        {
            dropdown.value = index;
        }
    }
}
