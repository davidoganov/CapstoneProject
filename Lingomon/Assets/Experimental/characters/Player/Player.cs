using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public GameObject cmr;
    public Animator animator;
    public float speed = 2f;
    public Transform movePoint;
    public Tilemap obs1;
    public Tilemap obs2;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        //cmr.transform.position = Vector3.MoveTowards(cmr.transform.position, new Vector3(movePoint.position.x, movePoint.position.y, -10), speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) == 0) {

            Vector3 nextPos = new Vector3(0f, 0f, 0f);
            nextPos.x = Input.GetAxisRaw("Horizontal");
            nextPos.y = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(nextPos.x) > Mathf.Abs(nextPos.y))
            {
                nextPos.y = 0;
            }
            else
            {
                nextPos.x = 0;
            }

            animator.SetFloat("vertical", nextPos.y);
            animator.SetFloat("horizontal", nextPos.x);
            Vector3Int obs1MapTile = obs1.WorldToCell(movePoint.position + nextPos);
            Vector3Int obs2MapTile = obs2.WorldToCell(movePoint.position + nextPos);
            if (obs1.GetTile(obs1MapTile) == null && obs2.GetTile(obs2MapTile) == null) {
                movePoint.position += nextPos;
            }
        }
    }
}
