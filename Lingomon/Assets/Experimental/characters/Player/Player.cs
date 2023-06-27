using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            move.y = 0;
        }
        else
        {
            move.x = 0;
        }

        animator.SetFloat("vertical", move.y);
        animator.SetFloat("horizontal", move.x);

        rb.velocity = new Vector2(move.x, move.y);
    }
}
