using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;
    [SerializeField] GameObject interactBox;

    private int index;
    private bool inDialogue;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        inDialogue = true;
        AudioManager.instance.Play("introTheme");
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialogue && Input.GetKeyDown(KeyCode.Z))
        {
            if (interactBox.activeSelf)
            {
                interactBox.SetActive(false);
            }
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            /*else if (gameObject.activeSelf == false)
            {
                Debug.Log("Switching Scene...");
                StartCoroutine(SwitchScene());
            }*/
            else if (inDialogue)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
        /*if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Switching Scene...");
            StartCoroutine(SwitchScene());
        }*/

        /*if(gameObject.activeSelf == false)
        {
            StartCoroutine(SwitchScene());
        }*/
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(1f / textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1) 
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
            inDialogue = false;
            Debug.Log("Out of Dialogue");
            StartCoroutine(SwitchScene());
        }
    }

    IEnumerator SwitchScene()
    {
        //yield return new WaitForSeconds (0.5f);
        AudioManager.instance.Stop("introTheme");
        Debug.Log("TRYING TO LOAD SCENE!!");

        yield return SceneManager.LoadSceneAsync("Experiment");
    }
}
