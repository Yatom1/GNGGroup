using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Add this for IEnumerator!

public class QuestManager2 : MonoBehaviour
{
    public static QuestManager2 Instance;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI dataText;
    public GameObject completionMessage;
    public GameObject dataPanel;
    public GameObject SceneTransition;
    private int currentQuestIndex = 0;

    private string[] quests =
    {
        "Find and interact with the Sea Urchin.",
        "Observe where the kelp once was Kelp",
        "Interact with the Jellyfish swarm.",
        "Tap on the Mussel to see its weakened shell.",
        "Examine the Coral and its bleaching effect.",
        "Quests Complete, transitioning soon"
    };

    private string[] questData =
    {
        "**Info will show here",
        "**Sea Urchins & Overpopulation**\nWithout predators like sea stars, urchins multiply rapidly and destroy kelp forests, leading to habitat loss.",
        "**Kelp Decline**\nOvergrowing sea urchins destroy kelp forests!",
        "**Jellyfish Overgrowth**\nWith fewer predators, jellyfish populations explode, reducing fish populations and disrupting ecosystems.",
        "**Mussels & Ocean Acidification**\nAcidified oceans weaken mussel shells, making them more fragile and threatening food chains.",
        "**Coral Bleaching**\nRising temperatures stress coral, expelling algae and turning them white—leading to mass coral die-offs."
    };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dataPanel.SetActive(true);
        questText.text = quests[currentQuestIndex];
        dataText.text = questData[currentQuestIndex];
    }

    public void CompleteQuest(int questIndex, GameObject interactUI)
    {
        if (questIndex == currentQuestIndex)
        {
            interactUI.SetActive(false);
            StartCoroutine(AdvanceQuest()); // Start coroutine here!
        }
    }

    IEnumerator AdvanceQuest() // Now a coroutine!
    {
        currentQuestIndex++;
        FindObjectOfType<ArrowManager>().UpdateArrowTarget(currentQuestIndex);
        questText.text = quests[currentQuestIndex];
        dataText.text = questData[currentQuestIndex];
        if (currentQuestIndex == quests.Length-1)
        {
            yield return new WaitForSeconds(10f);
            SceneTransition.SetActive(true);
        }
       
    }
}
