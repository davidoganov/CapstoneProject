using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject miniMap;
    [SerializeField] Camera miniMapCamera;
    [SerializeField] GameObject player;
    Stack<bool> wasOn = new Stack<bool>();

    float xMin = -4.5f;
    float xMax = 2.5f;
    float yMin = -6.25f;
    float yMax = 13.75f;

    public static MapController Instance { get; private set; }
    private void Awake() { Instance = this; }

    public void hideMap() {
        wasOn.Push(miniMap.activeSelf);
        miniMap.SetActive(false);
    }

    public void revealMap() { 
        if (wasOn.Peek()) miniMap.SetActive(true);
        wasOn.Pop();
    }

    public void showMap() {
        miniMap.SetActive(true);
    }

    public void removeMap() {
        miniMap.SetActive(false);
    }


    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M)) miniMap.SetActive(!miniMap.activeSelf);

        if (miniMap.activeSelf)
        {
            float xPos = player.transform.position.x;
            float yPos = player.transform.position.y;

            xPos = Mathf.Max(xPos, xMin);
            xPos = Mathf.Min(xPos, xMax);

            yPos = Mathf.Max(yPos, yMin);
            yPos = Mathf.Min(yPos, yMax);

            miniMapCamera.transform.position = new Vector3(xPos, yPos, miniMapCamera.transform.position.z);
        }
    }
}

