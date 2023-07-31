using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Quest[] quests;
    [SerializeField] TextMeshProUGUI questUIList;
    [SerializeField] GameObject questUI;
    int activeQuests = 0;
    bool wasOn = false;

    public static QuestManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
        updateQuestUI();
    }

    public void progressTask(int questIndex)
    {
        if (quests[questIndex].updateTask()) activeQuests--;
        if (activeQuests == 0) GamePhase.Instance.nextPhase();
        updateQuestUI();
    }

    public void addTask(int questIndex)
    {
        quests[questIndex].unlockTask();
        activeQuests++;
        updateQuestUI();
    }

    public void hideQuests() {
        wasOn = questUI.activeSelf;
        questUI.SetActive(false);
    }

    public void revealQuests() {
        if (wasOn) questUI.SetActive(true);
    }

    void updateQuestUI()
    {
        string questText = "";
        foreach (Quest q in quests) { 
            questText += q.ToString();
        }
        questUIList.text = questText;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            questUI.SetActive(!questUI.activeSelf);
        }
    }
}
