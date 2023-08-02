using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine.TextCore.Text;

public enum DialogState { normal, battleOption, healOption }

public class DialogManager : MonoBehaviour
{
    DialogState state;
    [SerializeField] GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    [SerializeField] int lettersPerSecond;
    public bool inDialog = false;
    [SerializeField] Color highlightedColor;
    [SerializeField] GameObject actionSelector;
    [SerializeField] List<TextMeshProUGUI> actionText;

    public static DialogManager Instance { get; private set; }
    Dialog dialog;
    Action onDialogFinished;
    int currentLine = 0;
    int currentAction = 0;
    bool printing = false;
    Character npc;
    bool isCutscene;

    public event Action OnDialogOpen;
    public event Action OnDialogClose;

    private void Awake() {
        Instance = this;
        state = DialogState.normal;
    }

    public IEnumerator ShowDialog(Dialog dialog, Character character=null, Action onFinished = null, bool isCutscene=false) {
        yield return new WaitForEndOfFrame();

        OnDialogOpen?.Invoke();
        inDialog = true;
        dialogBox.SetActive(true);
        this.dialog = dialog;
        onDialogFinished = onFinished;
        this.isCutscene = isCutscene;
        yield return TypeDialog(dialog.Lines[0]);
        npc = character;
    }

    public IEnumerator TypeDialog(string line) {
        printing = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        printing = false;
        if (isCutscene)
        {
            yield return new WaitForSeconds(.5f);
            inDialog = false;
            currentLine = 0;
            dialogBox.SetActive(false);
        }
    }

    public void HandleUpdate()
    {
        if (state == DialogState.normal)
        {
            dialogHandler();
        }
        else if (state == DialogState.battleOption)
        {
            optionHandler(true);
        }
        else if (state == DialogState.healOption)
        {
            optionHandler(false);
        }
    }

    public void dialogHandler()
    {
        if (!printing && Input.GetKeyDown(KeyCode.Z))
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                if (currentLine == dialog.Lines.Count - 1 && dialog.IsTrainer || dialog.IsNurse)
                {
                    //activate ActionSelector in dialogBox
                    actionSelector.SetActive(true);
                    currentAction = 0;
                    updateSelections(currentAction);
                    if(dialog.isTrainer)
                        state = DialogState.battleOption;
                    else if (dialog.isNurse)
                        state = DialogState.healOption;
                }
            }
            else
            {
                OnDialogClose();
                inDialog = false;
                currentLine = 0;
                dialogBox.SetActive(false);
                resetNPCDirection();
                if (npc != null && npc.CharacterName.Equals("Larry"))
                {
                    QuestManager.Instance.progressTask(0);
                    QuestManager.Instance.progressTask(3);
                    QuestManager.Instance.progressTask(5);
                }
                //onDialogFinished?.Invoke();
            }
        }
    }

    public void optionHandler(bool battle)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentAction < 1)
        {
            ++currentAction;
            updateSelections(currentAction);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentAction > 0)
        {
            --currentAction;
            updateSelections(currentAction);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            state = DialogState.normal;
            OnDialogClose();
            inDialog = false;
            currentLine = 0;
            actionSelector.SetActive(false);
            updateSelections(currentAction);
            dialogBox.SetActive(false);
            if (currentAction == 0 && battle)
            {
                StartCoroutine(startTrainerBattle());
            }
            else if (currentAction == 0 && !battle)
            {
                StartCoroutine(healLingomon());
            }
            else 
            {
                resetNPCDirection();
            }
        }
    }
    IEnumerator startTrainerBattle()
    {
        GameController.Instance.PauseGame(true);
        yield return StartCoroutine(TransitionManager.Instance.lingomonBattle1());
        StartCoroutine(TransitionManager.Instance.lingomonBattle2());
        GameController.Instance.PauseGame(false);
        resetNPCDirection();
        onDialogFinished?.Invoke();
    }

    IEnumerator healLingomon()
    {
        //GameController.Instance.PauseGame(true);
        //transition? probably not
        yield return new WaitForSeconds(0.5f);
        resetNPCDirection();
        onDialogFinished?.Invoke();
    }

    void resetNPCDirection()
    {
        if (npc != null) npc.Animator.SetFacingDirection(npc.Animator.DefaultDirection);
    }

    void updateSelections(int selection)
    {
        actionText[0].color = (selection == 0) ? Color.blue : Color.black;
        actionText[1].color = (selection == 1) ? Color.blue : Color.black;
    }
}
