using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Quest[] quests;
    [SerializeField] TextMeshProUGUI questUIList;
    [SerializeField] GameObject questUI;
    bool wasOn = false;

    public static QuestManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
        updateQuestUI();
    }

    public void progressTask(int questIndex)
    {
        quests[questIndex].updateTask();
        updateQuestUI();
    }

    public void addTask(int questIndex)
    {
        quests[questIndex].unlockTask();
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
