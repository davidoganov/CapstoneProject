using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{   
    public string sceneName;
    public float speed;
    public int transitionIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public SpriteRenderer sr;
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player") { 
            Debug.Log("starting transition...");
            StartCoroutine(playerController.enteringHouse(sceneName, playerPosition, playerStorage));
        }   
    }
}