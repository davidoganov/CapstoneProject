using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Transition, Dialog , Paused, Menu, Cutscene}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] MenuManager menuManager;

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
            if (state != GameState.Cutscene)
                state = GameState.Dialog;
        };
        DialogManager.Instance.OnDialogClose += () =>
        {
            if (state == GameState.Dialog)
            {
                //QuestManager.Instance.revealQuests();
                if (state != GameState.Cutscene)
                    state = GameState.FreeRoam;
            }
        };
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    public void enterMenu() {
        MapController.Instance.hideMap();
        QuestManager.Instance.hideQuests();
        stateBeforePause = state;
        state = GameState.Menu;
    }

    public void leaveMenu() {
        MapController.Instance.revealMap();
        QuestManager.Instance.revealQuests();
        state = stateBeforePause;
    }

    public void StartCutsceneState() 
    {
        state = GameState.Cutscene;
    }

    public void StartFreeRoamState()
    { 
        state = GameState.FreeRoam;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            MapController.Instance.hideMap();
            QuestManager.Instance.hideQuests();
            playerController.pauseMovement();
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            MapController.Instance.revealMap();
            QuestManager.Instance.revealQuests();
            state = stateBeforePause;
        }
    }

    public void StartBattle()
    {
        QuestManager.Instance.progressTask(1);
        MapController.Instance.hideMap();
        QuestManager.Instance.hideQuests();
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        state = GameState.Battle;
        var playerParty = playerController.GetComponent<LingomonParty>();
        var wildLingomon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildLingomon();

        battleSystem.StartBattle(playerParty, wildLingomon);
    }

    public void StartTrainerBattle(TrainerController trainer)
    {
        QuestManager.Instance.progressTask(2);
        QuestManager.Instance.hideQuests();
        MapController.Instance.hideMap();

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
        MapController.Instance.revealMap();
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
            MapController.Instance.HandleUpdate();
            QuestManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Cutscene)
        {
            playerController.character.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }

        if (state != GameState.Paused) menuManager.HandleUpdate();

    }
}
