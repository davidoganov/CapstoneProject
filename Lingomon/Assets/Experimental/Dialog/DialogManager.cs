using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DialogState { normal, battleOption }

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

    public event Action OnDialogOpen;
    public event Action OnDialogClose;

    private void Awake() {
        Instance = this;
        state = DialogState.normal;
    }

    public IEnumerator ShowDialog(Dialog dialog, Action onFinished = null) {
        yield return new WaitForEndOfFrame();

        OnDialogOpen?.Invoke();
        inDialog = true;
        dialogBox.SetActive(true);
        this.dialog = dialog;
        onDialogFinished = onFinished;
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line) {
        printing = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        //yield return new WaitForSeconds(1f);
        printing = false;
    }

    public void HandleUpdate()
    {
        if (state == DialogState.normal)
        {
            dialogHandler();
        }
        else if (state == DialogState.battleOption)
        {
            battleOptionHandler();
        }
    }

    public void dialogHandler()
    {
        if (!printing && Input.GetKeyDown(KeyCode.Z))
        {
            ++currentLine;
            print("pot next");
            if (currentLine < dialog.Lines.Count)
            {
                print("next");
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                if (currentLine == dialog.Lines.Count - 1 && dialog.IsTrainer)
                {
                    //activate ActionSelector in dialogBox
                    actionSelector.SetActive(true);
                    currentAction = 0;
                    updateSelections(currentAction);
                    state = DialogState.battleOption;
                }
            }
            else
            {
                OnDialogClose();
                inDialog = false;
                currentLine = 0;
                dialogBox.SetActive(false);
                //onDialogFinished?.Invoke();
            }
        }
    }

    public void battleOptionHandler()
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
            if (currentAction == 0)
            {
                onDialogFinished?.Invoke();
            }
        }
        //check for z input choice
    }

    void updateSelections(int selection)
    {
        actionText[0].color = (selection == 0) ? Color.blue : Color.black;
        actionText[1].color = (selection == 1) ? Color.blue : Color.black;
    }
}
