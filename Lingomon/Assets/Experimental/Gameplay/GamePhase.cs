using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhase : MonoBehaviour
{
    [SerializeField] public int phase;
    GameObject bridgeStop;

    public static GamePhase Instance { get; private set; }
    private void Awake() { Instance = this; }

    private void Start()
    {
        nextPhase();
    }

    public void nextPhase() {
        ++phase;
        switch (phase)
        {
            case 0:
                QuestManager.Instance.addTask(0);
                break;
            case 1:
                QuestManager.Instance.addTask(1);
                QuestManager.Instance.addTask(2);
                break;
            case 2:
                QuestManager.Instance.addTask(3);
                break;
            case 3:
                QuestManager.Instance.addTask(4);
                break;
            case 4:
                QuestManager.Instance.addTask(5);
                break;
        }
    }
}
