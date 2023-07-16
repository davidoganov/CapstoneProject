using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Transition, Dialog }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    GameState state;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DialogManager.Instance.OnDialogOpen += () => 
        { 
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnDialogClose += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
        playerController.OnEncountered += StartBattle;
        playerController.inTranstion += startTransition;
        playerController.transitionDone += endTransition;
        battleSystem.OnBattleOver += EndBattle;
    }

    void StartDialog() { state = GameState.Dialog; }

    void EndDialog() { state = GameState.FreeRoam; }

    void startTransition() { state = GameState.Transition; }

    void endTransition() { state = GameState.FreeRoam; }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<LingomonParty>();
        var wildLingomon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildLingomon();

        battleSystem.StartBattle(playerParty, wildLingomon);
    }

    public void StartTrainerBattle(TrainerController trainer)
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<LingomonParty>();
        var trainerParty = trainer.GetComponent<LingomonParty>();

        battleSystem.StartTrainerBattle(playerParty, trainerParty);
    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
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
