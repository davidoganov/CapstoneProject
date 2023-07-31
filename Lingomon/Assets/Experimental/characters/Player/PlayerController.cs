using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] Sprite sprite;
    private Vector2 input; 

    public Character character;
    float vert = -1f;
    float hori = 0f;

    public event Action OnEncountered;
    //public event Action inTranstion;
    //public event Action transitionDone;

    public static PlayerController i { get; private set; }

    private void Awake()
    {
        i = this;
        character = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnPlayer());
        //----------------------------------------------//
    }

    IEnumerator spawnPlayer()
    {
        GameController.Instance.PauseGame(true);
        yield return StartCoroutine(TransitionManager.Instance.playerSpawning());
        GameController.Instance.PauseGame(false);
    }

    public void pauseMovement()
    {
        character.IsMoving = false;
        character.HandleUpdate();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!character.IsMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
                vert = input.y;
                hori = input.x;
            }
            else
            {
                pauseMovement();
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z)) Interact();

    }

    void Interact()
    {
        Vector3 playerDir = new Vector3(hori, vert);
        Vector3 interactPos = transform.position + playerDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, GameLayers.i.TriggerableLayers);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }

    public string Name
    {
        get => Name;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}