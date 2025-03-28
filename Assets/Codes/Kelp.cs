using UnityEngine;

public class KelpInteraction : MonoBehaviour
{
    public GameObject interactUI;
    private bool canInteract = false;
    private bool hasInteracted = false;

    void Start() { interactUI.SetActive(true); }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            hasInteracted = true;
            Debug.Log("Interacting with Kelp! Sending quest ID 6.");
            QuestManager.Instance.CompleteQuest(6, interactUI);
            canInteract = false;
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) canInteract = true; }
    private void OnTriggerExit(Collider other) { canInteract = false; }
}
