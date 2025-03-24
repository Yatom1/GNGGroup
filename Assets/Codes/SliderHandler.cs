using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    public Slider slider; // Reference to the Slider component
    private int sliderValue; // To store the slider value

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        // Subscribe to the Slider's value changed event
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        sliderValue = Mathf.RoundToInt(value); // Store the rounded slider value
        Debug.Log("Slider Value: " + sliderValue);
    }

    public int GetSliderValue()
    {
        return sliderValue;
    }
}