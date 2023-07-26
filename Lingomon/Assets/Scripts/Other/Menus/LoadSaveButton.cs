using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSaveButton : MonoBehaviour
{
    // init variables
    public TMP_Dropdown saves;
    public Button loadSaveButton;
    public SceneManagement sceneManager;

    private void Start()
    {
        // init button interactability
        UpdateLoadSaveButtonInteractability();
    }

    public void LoadingSave()
    {
        // implement which save to load here in the future when database stuff fully working
        sceneManager.TransitionToExperimentScene();
    }

    private bool ValidDropdownSelection()
    {
        return saves.value != null; 
    }

    private void UpdateLoadSaveButtonInteractability()
    {
        loadSaveButton.interactable = ValidDropdownSelection();
    }
}
