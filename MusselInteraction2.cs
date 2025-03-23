using UnityEngine;

public class MusselInteraction2 : MonoBehaviour
{
    public GameObject interactUI;
    public GameObject MusselPrefab;
    public GameObject MusselCrushed;// Drag your crushed mussel prefab here
    public Renderer musselRenderer;         // Drag the Mussel’s Renderer component here
    public Color damagedColor = Color.white; // Color after acidification effect
    public int tapsRequired = 3;            // How many taps to crush
    private int tapCount = 0;

    private bool canInteract = false;
    private bool hasInteracted = false;

    void Start()
    {
        MusselCrushed.SetActive(false);
        interactUI.SetActive(true);
        if (musselRenderer == null)
            musselRenderer = GetComponent<Renderer>(); // Auto-assign if not set
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            tapCount++;
            Debug.Log("Tapped Mussel! Count: " + tapCount);

            // Shift color gradually after each tap
            float lerpValue = (float)tapCount / tapsRequired;
            musselRenderer.material.color = Color.Lerp(musselRenderer.material.color, damagedColor, lerpValue);

            if (tapCount >= tapsRequired)
            {
                CompleteInteraction();
            }
        }
    }

    void CompleteInteraction()
    {
        hasInteracted = true;
        Debug.Log("Mussel fully interacted! Sending quest ID 4.");
        MusselCrushed.SetActive(true);
        MusselPrefab.SetActive(false);

        QuestManager2.Instance.CompleteQuest(3, interactUI); // Update QuestManager (adjust index if needed)
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
