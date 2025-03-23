using UnityEngine;

public class JellyFishInteraction2 : MonoBehaviour
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
            Debug.Log("Interacting with Jellyfish! Sending quest ID 2.");
            QuestManager2.Instance.CompleteQuest(2, interactUI);
            canInteract = false;
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) canInteract = true; }
    private void OnTriggerExit(Collider other) { canInteract = false; }
}
