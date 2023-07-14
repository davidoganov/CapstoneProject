using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public int lettersPerSecond;
    public bool inDialog = false;

    public static DialogManager Instance { get; private set; }
    Dialog dialog;
    int currentLine = 0;
    bool printing = false;

    public event Action OnDialogOpen;
    public event Action OnDialogClose;

    private void Awake() {
        Instance = this;
    }

    public void ShowDialog(Dialog dialog) {
        OnDialogOpen();
        inDialog = true;
        dialogBox.SetActive(true);
        this.dialog = dialog;
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line) {
        printing = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        yield return new WaitForSeconds(1f);
        printing = false;
    }

    public void HandleUpdate()
    {
        if (!printing && Input.GetKeyDown(KeyCode.Z))
        {
            ++currentLine;
            print("pot next");
            if (currentLine < dialog.Lines.Count)
            {
                print("next");
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                OnDialogClose();
                inDialog = false;
                currentLine = 0;
                dialogBox.SetActive(false);
            }
        }
    }
}
