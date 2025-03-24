using UnityEngine;

public class SeaUrchinInteraction : MonoBehaviour
{
    public GameObject interactUI; // Interaction UI
    private bool canInteract = false;

    void Start()
    {
        // Show the interaction UI when the game starts
        if (interactUI != null)
            interactUI.SetActive(true);
    }

    void Update()
    {
        // If the player is close and presses the interact key (E in this case)
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacting with Sea Urchin! Completing Quest 0.");
            QuestManager2.Instance.CompleteQuest(0, interactUI); // Complete quest 0 for Sea Urchin
            canInteract = false; // Prevent further interaction until quest is completed
        }
    }

    // Triggered when the player enters the interaction range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    // Triggered when the player leaves the interaction range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
