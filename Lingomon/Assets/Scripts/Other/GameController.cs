using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { FreeRoam, Battle, Transition, Dialog , Paused, Menu, Cutscene, Heal }
public enum BattleType { wild, trainer, specialist}
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] MenuManager menuManager;
    [SerializeField] Dialog onLossDialog;
    public bool ranAway;
    BattleType currentBattle;
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
        currentBattle = BattleType.wild;
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
        currentBattle = trainer.isSpecialist ? BattleType.specialist : BattleType.trainer;
        QuestManager.Instance.hideQuests();
        MapController.Instance.hideMap();

        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<LingomonParty>();
        var trainerParty = trainer.GetComponent<LingomonParty>();

        battleSystem.StartTrainerBattle(playerParty, trainerParty);
    }

    public void HealPlayerLingomon()
    {
        PauseGame(true);
        Debug.Log("Lingomon Healed");
        var playerParty = playerController.GetComponent<LingomonParty>();
        var lingomon = playerParty.GetPlayerLingomon();
        lingomon.HP = lingomon.MaxHP;
        PauseGame(false);
    }

    void EndBattle(bool won)
    {
        if (!ranAway && won)
        {
            switch (currentBattle)
            {
                case BattleType.wild:
                    QuestManager.Instance.progressTask(1);
                    break;
                case BattleType.trainer:
                    QuestManager.Instance.progressTask(2);
                    break;
                case BattleType.specialist:
                    QuestManager.Instance.progressTask(4);
                    break;
            }
            ranAway = false;
        }
        if (won)
        {
            QuestManager.Instance.revealQuests();
            MapController.Instance.revealMap();
            state = GameState.FreeRoam;
            battleSystem.gameObject.SetActive(false);
            worldCamera.gameObject.SetActive(true);
        }
        else
        {
   
            StartCoroutine(onPlayerLoss());
        }
    }

    public IEnumerator onPlayerLoss() {
        PauseGame(true);
        yield return StartCoroutine(TransitionManager.Instance.playerLoss());
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(3);
        playerController.transform.position = new Vector3(-.5f, -4.25f, 0f);
        playerController.character.Animator.SetFacingDirection(FacingDirection.Right);
        yield return StartCoroutine(TransitionManager.Instance.playerSpawning());
        QuestManager.Instance.revealQuests();
        
        PauseGame(false);
        HealPlayerLingomon();
        StartCoroutine(DialogManager.Instance.ShowDialog(onLossDialog));
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
