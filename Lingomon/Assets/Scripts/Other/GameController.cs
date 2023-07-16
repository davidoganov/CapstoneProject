using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Transition, Dialog , Paused }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    GameState state;

    GameState stateBeforePause;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DialogManager.Instance.OnDialogOpen += () => 
        {
            //QuestManager.Instance.hideQuests();
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnDialogClose += () =>
        {
            if (state == GameState.Dialog)
            {
                //QuestManager.Instance.revealQuests();
                state = GameState.FreeRoam;
            }
        };
        playerController.OnEncountered += StartBattle;
        playerController.inTranstion += startTransition;
        playerController.transitionDone += endTransition;
        battleSystem.OnBattleOver += EndBattle;
    }

    void StartDialog() { QuestManager.Instance.hideQuests(); state = GameState.Dialog; }

    void EndDialog() { QuestManager.Instance.revealQuests(); state = GameState.FreeRoam; }

    void startTransition() { state = GameState.Transition; }

    void endTransition() { state = GameState.FreeRoam; }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            QuestManager.Instance.hideQuests();
            playerController.pauseMovement();
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            QuestManager.Instance.revealQuests();
            state = stateBeforePause;
        }
    }

    public void StartBattle()
    {
        QuestManager.Instance.progressTask(1);
        QuestManager.Instance.hideQuests();
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<LingomonParty>();
        var wildLingomon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildLingomon();

        battleSystem.StartBattle(playerParty, wildLingomon);
    }

    public void StartTrainerBattle(TrainerController trainer)
    {
        QuestManager.Instance.progressTask(2);
        QuestManager.Instance.hideQuests();
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<LingomonParty>();
        var trainerParty = trainer.GetComponent<LingomonParty>();

        battleSystem.StartTrainerBattle(playerParty, trainerParty);
    }

    void EndBattle(bool won)
    {
        QuestManager.Instance.revealQuests();
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
            QuestManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
