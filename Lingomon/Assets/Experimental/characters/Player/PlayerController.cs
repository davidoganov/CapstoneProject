using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;

    public LayerMask grassLayer;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    private Vector2 input; 

    public VectorValue startingPosition;
    public bool canMove = false;
    public SpriteRenderer sr;
    bool inSpawnTrans = false;
    public float tranSpeed;
    float transitionProg = 0;

    public event Action OnEncountered;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transitionProg = 1f;
        inSpawnTrans = true;
        sr.material.SetFloat("_CutOff", transitionProg);
        //----------------------------------------------//
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (inSpawnTrans) {
            animator.SetFloat("vertical", 0f);
            animator.SetFloat("horizontal", 0f);
            transitionProg -= Time.deltaTime * tranSpeed;
            sr.material.SetFloat("_CutOff", transitionProg);
            if (transitionProg <= 0f) {
                inSpawnTrans = false;
                canMove = true;
            }
        } else if (!canMove) {
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

                if (isWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
            else
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0);
            }
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
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncountered();
            }
        }
    }
}