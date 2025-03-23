using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JellyfishGuide : MonoBehaviour
{
    public static JellyfishGuide Instance; // Singleton instance

    public GameObject dialogueUI;  // First Canvas (JellyfishGuideCanvas)
    public GameObject interactUI;  // Second Canvas (!)
    public GameObject questUI;
    public GameObject dataUI;
    public GameObject GuidingArrow;
    public TMPro.TextMeshProUGUI dialogueText;

    private int dialogueStep = 0;
    private bool isInteracting = false;  // Track interaction state
    private bool hasInteracted = false;  // New flag to check if interaction occurred
    private bool canInteract = false;    // Check if player is near enough

    private string[] dialogues = {
        "Welcome, little crab! Let me show you how to move. Press the space bar to jump and continue.", // 0
        "Try ducking, press Ctrl.", // 1
        "Try moving forward, press W.", // 2
        "Try moving back, press S.", // 3
        "Try moving left, press A.", // 4
        "Try moving right, press D.", // 5
        "Anything with ! above them is interactable. Try finding something and interact with it. Go close to it and press E. (Press E to continue)", // 6
        "Great job! Before we begin, we want to gauge your understanding of climate change first. (PRESS E)", // 7
        "Question 1: What are some effects of climate change on ocean life? (Press E)", // 8
        "Question 2: How do rising temperatures impact marine ecosystems? (Press E)", // 9
        "Question 3: What role do coral reefs play in ocean biodiversity? (Press E)", // 10
        "I will appear later to check up on you and ask you more questions. (Press E)", // 11
        "For now, there will be a display of quests in the red box, data in the green box, and the yellow box will point you in the direction of the next quest when you are facing it. (Press E)", //12
        "Try to do quests in order as there is a bug. Press N to start.", //13
    };

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowDialogue();
        interactUI.SetActive(false);  // Hide (!) initially
        questUI.SetActive(false);
        dataUI.SetActive(false);
        GuidingArrow.SetActive(false);
    }

    void Update()
    {
        if (!isInteracting)
        {
            HandleTutorialInput();
        }
        else
        {
            HandleInteraction();
        }
    }

    void HandleTutorialInput()
    {
        switch (dialogueStep)
        {
            case 0: if (Input.GetKeyDown(KeyCode.Space)) NextDialogue(); break;
            case 1: if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) NextDialogue(); break;
            case 2: if (Input.GetKeyDown(KeyCode.W)) NextDialogue(); break;
            case 3: if (Input.GetKeyDown(KeyCode.S)) NextDialogue(); break;
            case 4: if (Input.GetKeyDown(KeyCode.A)) NextDialogue(); break;
            case 5: if (Input.GetKeyDown(KeyCode.D)) NextDialogue(); break;
            case 6: if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(StartInteractionPhase()); break;
            case 7: // Player must interact by pressing 'E'
                if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
                {
                    Debug.Log("Interaction triggered at step 7");
                    interactUI.SetActive(false); // Hide "!"
                    dialogueUI.SetActive(true); // Show dialogue
                    isInteracting = false; // Reset interaction state
                    hasInteracted = true; // Now the player can progress
                    ShowDialogue(); // Display dialogue 7
                }
                else if (hasInteracted && Input.GetKeyDown(KeyCode.E))
                {
                    NextDialogue(); // Move to Q1 (dialogue 8)
                }
                break;

            case 8: // Ensure Q1 only starts after dialogue 7 is read  
                if (hasInteracted && Input.GetKeyDown(KeyCode.E))
                {
                    NextDialogue();
                }
                break;
            case 9: // Q2
            case 10: // Q3
                if (Input.GetKeyDown(KeyCode.E))
                {

                    NextDialogue();
                }
                break;
            case 11: // Transition scene
                if (Input.GetKeyDown(KeyCode.E))
                {

                    NextDialogue();
                }
                break;
            case 12:
                questUI.SetActive(true);
                dataUI.SetActive(true);
                GuidingArrow.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    NextDialogue();
                }
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.N))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
                }
                break;
        }
    }

    void HandleInteraction()
    {
        if ((canInteract || hasInteracted) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E to interact");
            interactUI.SetActive(false);  // Hide "!"
            isInteracting = false;
            hasInteracted = true; // Set flag so the player doesn't need to stay in zone
            NextDialogue();
        }
    }

    IEnumerator StartInteractionPhase()
    {
        isInteracting = true;
        yield return new WaitForSeconds(2f);  // Wait 3 seconds
        dialogueUI.SetActive(false);  // Hide Jellyfish's dialogue
        interactUI.SetActive(true);   // Show "!"
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
        {
            canInteract = true;
            Debug.Log("Player entered interaction zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Player left interaction zone");
        }
    }

    public void ShowDialogue()
    {
        if (dialogueStep < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueStep];
            dialogueUI.SetActive(true);
        }
        else
        {
            dialogueUI.SetActive(false);
        }
    }

    public void NextDialogue()
    {
        dialogueStep++;
        Debug.Log("Dialogue Step: " + dialogueStep);
        ShowDialogue();
    }
}
