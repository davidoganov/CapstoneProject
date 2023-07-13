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
    public bool inTransition = false;
    PlayerController playerScript;

    void Start() {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player" && !inTransition) { 
            Debug.Log("starting transition...");
            playerScript.canMove = false;
            StartCoroutine(enteringHouse());
        }   
    }

    IEnumerator enteringHouse() {
        
        print("co starting");
        float transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1f) {
            transitionProg += .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / speed);
        }
        Debug.Log("ending transition...");
        playerStorage.initialValue = playerPosition;
        playerScript.canMove = true;
        SceneManager.LoadScene(sceneName);
    }
}

/*
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
    public bool inTransition = false;
    float transitionProg = 0f;
    Player playerScript;

    void Start() {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        if (inTransition) {
            Debug.Log("running transition...");
            transitionProg += Time.deltaTime * speed;
            sr.material.SetFloat("_CutOff", transitionProg);
            if (transitionProg >= 1f) {
                Debug.Log("ending transition...");
                playerStorage.initialValue = playerPosition;
                inTransition = false;
                playerScript.canMove = true;
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player" && !inTransition) { 
            Debug.Log("starting transition...");
            playerScript.canMove = false;
            inTransition = true;
            transitionProg = 0f;
            sr.material.SetFloat("_CutOff", transitionProg);
        }   
    }

    IEnumerator enteringHouse() {
        print("running");
        float transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1f) {
            transitionProg += .2f;
            sr.material.SetFloat("CutOff", transitionProg);
            yield return new WaitForSeconds(1f);
        }
    }
}
*/