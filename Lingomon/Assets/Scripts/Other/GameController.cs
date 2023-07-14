using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Transition, Dialog }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] DialogManager dialogManager;

    GameState state;

    private void Start()
    {
        dialogManager.OnDialogOpen += StartDialog;
        dialogManager.OnDialogClose += EndDialog;
        playerController.OnEncountered += StartBattle;
        playerController.inTranstion += startTransition;
        playerController.transitionDone += endTransition;
        battleSystem.OnBattleOver += EndBattle;
    }

    void StartDialog() { state = GameState.Dialog; }

    void EndDialog() { state = GameState.FreeRoam; }

    void startTransition() { state = GameState.Transition; }

    void endTransition() { state = GameState.FreeRoam; }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
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
            dialogManager.HandleUpdate();
        }
    }
}
