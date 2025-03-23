using UnityEngine;
using TMPro;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public TextMeshProUGUI questText; // UI for quest objectives
    public TextMeshProUGUI dataText; //  New: Data canvas text
    public GameObject caveTrigger;
    public GameObject arrowText;

    private int currentQuestIndex = 0;

    private string[] quests =
    {
        "Find the crayfish and observe\nits behavior.",
        "Find and observe a\nswimming turtle.",
        "Find an oyster and tap on it.",
        "Find and interact with coral.",
        "Find and observe an octopus.",
        "Find the kelp and move a fragment.",
        "Enter the cave to continue\nyour journey."
    };

    private string[] interactionInfo =
    {
        "Sea stars help control sea urchin\npopulations, keeping kelp forests healthy.",
        "Crayfish are sensitive to temperature and\nocean acidification.",
        "Turtles rely on balanced sand temperatures for proper hatchling development.",
        "Oysters build their shells using calcium carbonate, but acidification weakens them.",
        "Coral reefs support marine biodiversity, but warming oceans cause coral bleaching.",
        "Octopuses are highly intelligent\nand depend on coral reefs for shelter.",
        "Kelp forests act as carbon sinks and provide food and habitat."
    };

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dataText.gameObject.SetActive(true);
        caveTrigger.SetActive(false);
    }

    public void CompleteQuest(int questID, GameObject interactUI)
    {
        Debug.Log($"🔵 Attempting to complete Quest {questID}");
        Debug.Log($"🔹 Current Quest Index BEFORE check: {currentQuestIndex}");

        if (questID == currentQuestIndex)
        {
            interactUI.SetActive(false);

            if (currentQuestIndex < quests.Length - 1)
            {
                questText.text = quests[currentQuestIndex];
                dataText.text = interactionInfo[currentQuestIndex];

                // 🟢 Update ArrowManager:
                

                currentQuestIndex = questID + 1;
                FindObjectOfType<ArrowManager>().UpdateArrowTarget(currentQuestIndex);
            }
            else
            {
                caveTrigger.SetActive(true);
                questText.text = "All quests completed! Enter the cave.";
                arrowText.SetActive(false); // Disable arrow after all quests
            }
        }

    }
       
    }


