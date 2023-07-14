using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public enum BattleState { Start, ActionSelection, MoveSelection, PerformMove
        , Busy, RunningTurn, BattleOver}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogueBox battleDialogueBox;
    [SerializeField] PromptDialogueBox promptDialogueBox;
    [SerializeField] Image playerImage;
    [SerializeField] Image trainerImage;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    LingomonParty playerParty;
    LingomonParty trainerParty;
    Lingomon wildLingomon;

    bool isTrainerBattle = false;
    PlayerController player;
    TrainerController trainer;

    public void StartBattle( LingomonParty playerParty,  Lingomon wildLingomon)
    {
        this.playerParty = playerParty;
        this.wildLingomon = wildLingomon;
        StartCoroutine(SetupBattle());
    }

    public void StartTrainerBattle(LingomonParty playerParty, LingomonParty trainerParty)
    {
        this.playerParty = playerParty;
        this.trainerParty = trainerParty;

        isTrainerBattle = true;
        player = playerParty.GetComponent<PlayerController>();
        trainer = trainerParty.GetComponent<TrainerController>();

        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Clear();
        enemyUnit.Clear();
        promptDialogueBox.EnablePromptBox(false);
        if (!isTrainerBattle)
        {
            //Wild lingomon battle
            playerUnit.Setup(playerParty.GetHealthyLingomon()); 
            enemyUnit.Setup(wildLingomon);

            battleDialogueBox.SetAnswerNames(playerUnit.Lingomon.Answers);
            yield return battleDialogueBox.TypeDialogue($"A wild {enemyUnit.Lingomon.Base.Name} appeared.");
        }
        else
        {
            //trainer battle
            //show player and trainer sprites
            playerUnit.gameObject.SetActive(false);
            enemyUnit.gameObject.SetActive(false);

            playerImage.gameObject.SetActive(true);
            trainerImage.gameObject.SetActive(true);
            playerImage.sprite = player.Sprite;
            trainerImage.sprite = trainer.Sprite;

            yield return battleDialogueBox.TypeDialogue($"{trainer.Name} wants to battle");
            yield return new WaitForSeconds(1.5f);

            // send out first trainer lingomon
            trainerImage.gameObject.SetActive(false);
            enemyUnit.gameObject.SetActive(true);
            var enemyLingomon = trainerParty.GetHealthyLingomon();
            enemyUnit.Setup(enemyLingomon);
            yield return battleDialogueBox.TypeDialogue($"{trainer.Name} sent out {enemyLingomon.Base.Name}");
            yield return new WaitForSeconds(1.5f);

            //send out players lingomon
            playerImage.gameObject.SetActive(false);
            playerUnit.gameObject.SetActive(true);
            var playerLingomon = playerParty.GetHealthyLingomon();
            playerUnit.Setup(playerLingomon);
            yield return battleDialogueBox.TypeDialogue($"Go {playerLingomon.Base.Name}!");
            battleDialogueBox.SetAnswerNames(playerUnit.Lingomon.Answers);
        }

        yield return new WaitForSeconds(1f);
        promptDialogueBox.EnablePromptBox(true);
        promptDialogueBox.SetDialogue($"No way the text box works :O.");

        ActionSelection();
    }

    void BattleOver(bool won)
    {
        state = BattleState.BattleOver;
        OnBattleOver(won);
    }

    void ActionSelection()
    {
        state = BattleState.ActionSelection;
        StartCoroutine(battleDialogueBox.TypeDialogue("Choose an action"));
        battleDialogueBox.EnableActionSelector(true);
    }

    void MoveSelection()
    {
        state = BattleState.MoveSelection;
        battleDialogueBox.EnableActionSelector(false);
        battleDialogueBox.EnableDialogueText(false);
        battleDialogueBox.EnableMoveSelector(true);
    }

    IEnumerator PlayerMove()
    {
        //CHECK IF ANSWER IS CORRECT
        //IF SO DEAL DAMAGE, IF NOT TAKE DAMAGE
        state = BattleState.PerformMove;

        var answer = playerUnit.Lingomon.Answers[currentMove];
        yield return battleDialogueBox.TypeDialogue(/*if answer is correct or not*/ $"Answer Correct?");

        yield return RunMove(playerUnit, enemyUnit, answer);
        //IF INCORRECT
        if (state == BattleState.PerformMove)
            StartCoroutine(EnemyMove(true));
        /*else
            ActionSelection();*/
    }

    IEnumerator EnemyMove(bool isCorrectAnswer)
    {
        if (!isCorrectAnswer)
        {
            state = BattleState.PerformMove;
            var answer = playerUnit.Lingomon.Answers[currentMove]; //UNNCESSSARAYRY
            yield return battleDialogueBox.TypeDialogue("Incorrect answer");

            yield return new WaitForSeconds(1f);

            yield return RunMove(enemyUnit, playerUnit, answer);
        }

        // if the battle stat was not changed then go next
        if(state == BattleState.PerformMove)
            ActionSelection();
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Answer answer)
    {
        
        //yield return battleDialogueBox.TypeDialogue(/*if answer is correct or not*/ $"Answer Correct?");

        yield return new WaitForSeconds(1f);

        //if (CORRECT)
        bool isFainted = targetUnit.Lingomon.TakeDamage(answer, 30);
        yield return targetUnit.Hud.UpdateHP();

        if (isFainted)
        {
            yield return battleDialogueBox.TypeDialogue("Enemy Fainted");

            yield return new WaitForSeconds(2f); //play animation here
            if(!isTrainerBattle)
                BattleOver(true);
            else
            {
                var nextLingomon = trainerParty.GetHealthyLingomon();
                if (nextLingomon != null)
                    StartCoroutine(SendNextTrainerLingomon(nextLingomon));
                else
                    BattleOver(true);
            }
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.ActionSelection)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.MoveSelection)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection() 
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        battleDialogueBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                //Fight
                MoveSelection();
            }
            else if (currentAction == 1)
            {
                //Run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Lingomon.Answers.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Lingomon.Answers.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        battleDialogueBox.UpdateMoveSelection(currentMove);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            battleDialogueBox.EnableMoveSelector(false);
            battleDialogueBox.EnableDialogueText(true);
            StartCoroutine(PlayerMove());
        }
    }

    IEnumerator SendNextTrainerLingomon(Lingomon nextLingomon)
    {
        state = BattleState.Busy;

        enemyUnit.Setup(nextLingomon);
        yield return battleDialogueBox.TypeDialogue($"{trainer.Name} sent out {nextLingomon.Base.Name}!");

        state = BattleState.RunningTurn;
    }
}