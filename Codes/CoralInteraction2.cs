using UnityEngine;

public class CoralInteraction2 : MonoBehaviour
{
    public GameObject interactUI;
    private bool canInteract = false;
    private bool hasInteracted = false;

    void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(true);
        else
            Debug.LogWarning("interactUI is not assigned in the Inspector!");
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            hasInteracted = true;
            Debug.Log("Interacting with Coral! Sending quest ID 4.");
            QuestManager2.Instance.CompleteQuest(4, interactUI);
            canInteract = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }
}
