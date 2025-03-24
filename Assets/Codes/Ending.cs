using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class JellyfishReflection : MonoBehaviour
{
    public GameObject dialogueUI;       // Dialogue Canvas
    public TextMeshProUGUI dialogueText;
    public GameObject interactUI;       // "!" symbol
    private int dialogueStep = 0;
    private bool isInteracting = false;
    private bool canInteract = false;
    private bool hasInteracted = false;

    private string[] dialogues = {
        "Hello again, little crab. You�ve seen the changes... how it once was, and how it has become.", // 0
        "We hope you had an immersive and engaging learning experience.", // 1
        "Please answer these questions to reflect on your stance on climate change now. Press E to continue.", // 2
        "1. How concerned are you about the impact of climate change on regions far from where you live?", // 3
        "2. Do you believe long-term climate impacts (over decades) are as urgent as short-term crises?", // 4
        "3. How do you feel about the effects of climate change on people you don�t personally know?", // 5
        "Thank you for your reflections. Together, awareness can lead to action." // 6
    };

    void Start()
    {
        dialogueUI.SetActive(false);
        interactUI.SetActive(true); // Show "!" initially
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasInteracted)
            {
                interactUI.SetActive(false);
                dialogueUI.SetActive(true);
                ShowDialogue();
                hasInteracted = true;
                isInteracting = true;
            }
            else if (isInteracting)
            {
                NextDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    void ShowDialogue()
    {
        if (dialogueStep < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueStep];
        }
        else
        {
            dialogueUI.SetActive(false);
        }
    }

    void NextDialogue()
    {
        dialogueStep++;
        ShowDialogue();
    }
}
