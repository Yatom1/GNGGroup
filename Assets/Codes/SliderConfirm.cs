using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class SliderData
{
    public float sliderValue;
}

public class SliderConfirm : MonoBehaviour
{
    public Slider slider;
    public TMP_Text displayText;

    private string filePath;

    void Start()
    {
#if UNITY_EDITOR
        // Save inside Assets/SaveData when running in the Editor
        string directory = Application.dataPath + "/SaveData";
#else
        // Save to persistentDataPath when running a build
        string directory = Application.persistentDataPath + "/SaveData";
#endif
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        filePath = Path.Combine(directory, "sliderData.json");
        LoadFromFile(); // Load saved value at the start
    }

    // Called when the button is clicked
    public void ConfirmValue()
    {
        float confirmedValue = slider.value;
        displayText.text = "Value: " + confirmedValue.ToString("F1");
        Debug.Log("Confirmed slider value: " + confirmedValue);

        SaveToFile(confirmedValue);
    }

    // SAVE DATA TO FILE
    private void SaveToFile(float value)
    {
        SliderData data = new SliderData();
        data.sliderValue = value;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved slider value to file: " + filePath);
    }

    // LOAD DATA FROM FILE
    private void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SliderData data = JsonUtility.FromJson<SliderData>(json);

            slider.value = data.sliderValue;
            displayText.text = "Value: " + data.sliderValue.ToString("F1");
            Debug.Log("Loaded slider value from file: " + data.sliderValue);
        }
        else
        {
            Debug.Log("No save file found, using default value.");
        }
    }
}
