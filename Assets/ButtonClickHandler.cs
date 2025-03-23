using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;
    public Text displayText;

    void Start()
    {
        // Assign the button click event
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Change the text when the button is clicked
        displayText.text = "Save the Ocean";
    }
}