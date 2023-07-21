using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] TextMeshProUGUI[] menuOptions;
    int option = 0;

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            menu.SetActive(!(menu.activeSelf));
            if (menu.activeSelf)
            {
                GameController.Instance.enterMenu();
            }
            else
            {
                GameController.Instance.leaveMenu();
                option = 0;
            }
        }

        if (menu.activeSelf) menuUpdate();

        
    }


    // Update is called once per frame
    public void menuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && option < 2) option++;
        else if (Input.GetKeyDown(KeyCode.UpArrow) && option > 0) option--;

        for (int i = 0; i < 3; i++)
        {
            menuOptions[i].color = option == i ? Color.blue : Color.black;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            menuSelect();
        }
    }

    public void menuSelect() {
        switch (option)
        {
            case 0: //Options

                break;
            case 1: //Save

                break;
            case 2: //Exit
                Application.Quit();
                break;
        }
    }


}
