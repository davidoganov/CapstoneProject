using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject miniMap;
    Stack<bool> wasOn = new Stack<bool>();

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
    }
}

