using UnityEngine;

public class CrayfishInteraction : MonoBehaviour
{
    public GameObject interactUI;
    public Transform focusPoint; // Empty GameObject to focus camera
    private bool canInteract = false;
    private bool hasInteracted = false;
    private bool interactionInProgress = false;

    void Start() { interactUI.SetActive(true); }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted && !interactionInProgress)
        {
            hasInteracted = true;
            interactionInProgress = true;
            interactUI.SetActive(false);

            InteractionManager.Instance.StartInteraction(
                focusPoint,
                "Crayfish are sensitive to rising temperatures and ocean acidification. They struggle to adapt to these rapid changes.",
                () => {
                    interactionInProgress = false;
                    QuestManager.Instance.CompleteQuest(1, interactUI); // Quest ID 1
                });
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) canInteract = true; }
    private void OnTriggerExit(Collider other) { canInteract = false; }
}
