using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;

    public LayerMask grassLayer;
    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;
    private bool isMoving;
    private Vector2 input; 

    public VectorValue startingPosition;
    public bool canMove = false;
    public SpriteRenderer sr;
    public float tranSpeed;
    float vert = -1f;
    float hori = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playerSpawning());
        //----------------------------------------------//
        transform.position = startingPosition.initialValue;
    }

    IEnumerator playerSpawning() {
        canMove = false;
        float transitionProg = 1.1f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") > 0f) {
            transitionProg -= .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / tranSpeed);
        }
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (!canMove) {
            animator.SetFloat("vertical", 0f);
            animator.SetFloat("horizontal", 0f);
        } else if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                animator.SetFloat("vertical", input.y);
                animator.SetFloat("horizontal", input.x);
                hori = input.x;
                vert = input.y;

                if (isWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
            else
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z)) { 
            print("starting");
            Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactablesLayer) != null)
        {
            return false;
        }

        return true;
    }

    void Interact()
    {
        Vector3 playerDir = new Vector3(hori, vert);
        print(playerDir);
        Vector3 interactPos = transform.position + playerDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactablesLayer);
        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
            canMove = !DialogManager.Instance.inDialog;
        }
        
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encounter Success");
            }
        }
    }
}