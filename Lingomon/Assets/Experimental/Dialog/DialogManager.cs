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

    private void Awake() {
        Instance = this;
    }

    public void ShowDialog(Dialog dialog) {
        inDialog = true;
        dialogBox.SetActive(true);
        this.dialog = dialog;
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void NextInDialog() {
        if (!printing) {
            ++currentLine;
            print("pot next");
            if (currentLine < dialog.Lines.Count) {
                print("next");
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            } else {
                inDialog = false;
                currentLine = 0;
                dialogBox.SetActive(false);
            }
        }
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
}
