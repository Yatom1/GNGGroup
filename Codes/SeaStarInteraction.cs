using UnityEngine;

public class SeaStarInteraction : MonoBehaviour
{
    public GameObject interactUI;
    public Transform focusPoint; // Empty GameObject to focus camera
    private bool canInteract = false;
    private bool hasInteracted = false;
    private bool interactionInProgress = false; // NEW: Lock

    void Start() { interactUI.SetActive(true); }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted && !interactionInProgress)
        {
            hasInteracted = true;
            interactionInProgress = true;
            interactUI.SetActive(false);

            // Disable movement, focus camera, type dialogue
            InteractionManager.Instance.StartInteraction(
                focusPoint,
                "Sea stars help control sea urchin populations, keeping kelp forests healthy.",
                () => {
                    // Callback: After dialogue done, complete quest
                    interactionInProgress = false;
                    QuestManager.Instance.CompleteQuest(0, interactUI);
                });
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) canInteract = true; }
    private void OnTriggerExit(Collider other) { canInteract = false; }
}
