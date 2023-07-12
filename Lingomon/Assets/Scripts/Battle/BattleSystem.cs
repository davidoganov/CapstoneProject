using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogueBox battleDialogueBox;
    [SerializeField] PromptDialogueBox promptDialogueBox;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Lingomon);
        enemyHud.SetData(enemyUnit.Lingomon);

        battleDialogueBox.SetAnswerNames(playerUnit.Lingomon.Answers);

        yield return battleDialogueBox.TypeDialogue($"A wild {enemyUnit.Lingomon.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);
        promptDialogueBox.EnablePromptBox(true);
        promptDialogueBox.SetDialogue($"No way the text box works :O.");

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(battleDialogueBox.TypeDialogue("Choose an action"));
        battleDialogueBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        battleDialogueBox.EnableActionSelector(false);
        battleDialogueBox.EnableDialogueText(false);
        battleDialogueBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        //CHECK IF ANSWER IS CORRECT
        //IF SO DEAL DAMAGE, IF NOT TAKE DAMAGE
        state = BattleState.Busy;

        var answer = playerUnit.Lingomon.Answers[currentMove];
        yield return battleDialogueBox.TypeDialogue(/*if answer is correct or not*/ $"Answer Correct?");
        
        yield return new WaitForSeconds(1f);

        //if (CORRECT)
        bool isFainted = enemyUnit.Lingomon.TakeDamage(answer, 30);
        yield return enemyHud.UpdateHP();

        if(isFainted)
        {
            yield return battleDialogueBox.TypeDialogue("Enemy Fainted");

            yield return new WaitForSeconds(2f); //play animation here
            OnBattleOver(true);
        }
        else //IF INCORRECT
        {
            //StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var answer = playerUnit.Lingomon.Answers[currentMove]; //UNNCESSSARAYRY
        yield return battleDialogueBox.TypeDialogue("Incorrect answer");

        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.Lingomon.TakeDamage(answer, 30);
        yield return playerHud.UpdateHP();

        if(isFainted)
        {
            yield return battleDialogueBox.TypeDialogue("Enemy Fainted"); //play animation here

            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }
        else 
        {
            PlayerAction();
        }   
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
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
                PlayerMove();
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
            StartCoroutine(PerformPlayerMove());
        }
    }
}