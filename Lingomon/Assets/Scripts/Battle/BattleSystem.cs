using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public enum BattleState { Start, ActionSelection, MoveSelection, RunningTurn
        , Busy, BattleOver}

public enum BattleAction { Move, Run }

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
    Question currentQuestion;
    int currentAction;
    int currentMove;
    List<int> randomIndexes;

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
        }

        currentQuestion = enemyUnit.Lingomon.GenerateQuestion();
        randomIndexes = battleDialogueBox.SetAnswerNames(currentQuestion);

        yield return new WaitForSeconds(1f);
        promptDialogueBox.EnablePromptBox(true);
        promptDialogueBox.SetDialogue($"{currentQuestion.Base.Prompt}");

        ActionSelection();
    }

    void BattleOver(bool won)
    {
        state = BattleState.BattleOver;
        promptDialogueBox.EnablePromptBox(false);
        isTrainerBattle = false;
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

    IEnumerator RunTurns(BattleAction playerAction)
    {
        state = BattleState.RunningTurn;

        if (playerAction == BattleAction.Move)
        {
            string answer = currentQuestion.Base.Answers[randomIndexes[currentMove]];
            bool correct = enemyUnit.Lingomon.CorrectAnswer(currentQuestion, answer);

            yield return RunMove(correct);
            //yield return RunAfterTurn()
            if (state == BattleState.BattleOver) yield break;
            currentQuestion = enemyUnit.Lingomon.GenerateQuestion();
            promptDialogueBox.SetDialogue($"{currentQuestion.Base.Prompt}");
            randomIndexes = battleDialogueBox.SetAnswerNames(currentQuestion);
        }
        else if (playerAction == BattleAction.Run)
        {

        }
    }

    IEnumerator RunMove(bool correct)
    {
        //yield return battleDialogueBox.TypeDialogue(/*if answer is correct or not $"Answer Correct?");

        //yield return new WaitForSeconds(1f);

        if (correct)
        {
            yield return battleDialogueBox.TypeDialogue($"Answer Correct");
            yield return RunAfterTurn(enemyUnit);
        }
        else
        {
            yield return battleDialogueBox.TypeDialogue($"Answer Incorrect");
            yield return RunAfterTurn(playerUnit);
        }
    }

    IEnumerator RunAfterTurn(BattleUnit sourceUnit)
    {
        if (state == BattleState.BattleOver) yield break;


        yield return new WaitForSeconds(1f);

        bool isFainted = sourceUnit.Lingomon.TakeDamage(30);
        yield return sourceUnit.Hud.UpdateHP();
        if (isFainted)
        {
            yield return battleDialogueBox.TypeDialogue($"{sourceUnit.Lingomon.Base.Name} Fainted");
            //sourceUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);

            //CheckForBattleOver(sourceUnit);
            if (!isTrainerBattle)
            {
                if (sourceUnit == playerUnit)
                    BattleOver(false);
                else
                    BattleOver(true);
            }
            else
            {
                var nextLingomon = trainerParty.GetHealthyLingomon();
                if (sourceUnit != playerUnit && nextLingomon != null)
                    yield return StartCoroutine(SendNextTrainerLingomon(nextLingomon));
                else
                    BattleOver(true);
            }
        }

        //yield return new WaitForSeconds(0.5f);
        if (state != BattleState.BattleOver) ActionSelection();
    }

   /* void CheckForBattleOver(BattleUnit sourceUnit)
    {

    }*/

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
                StartCoroutine(RunTurns(BattleAction.Run));
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
            StartCoroutine(RunTurns(BattleAction.Move));
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