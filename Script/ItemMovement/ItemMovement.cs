using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    private float movementSpeed;
    private bool moveLeft = true;

    private void Awake()
    {
        movementSpeed = 2f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 move = transform.localPosition;
        if (moveLeft) move.x -= movementSpeed * Time.deltaTime;
        else move.x += movementSpeed * Time.deltaTime;
        transform.localPosition = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" && collision.contacts[0].normal.x > 0)
        {
            moveLeft = false;
            Vector2 turn = transform.localScale;
            turn.x = -1;
            transform.localScale = turn;
        }
        else if (collision.gameObject.tag != "Player" && collision.contacts[0].normal.x < 0)
        {
            moveLeft = true; 
            Vector2 turn = transform.localScale;
            turn.x = 1;
            transform.localScale = turn;
        }
    }

   /* private void Turning()
    {
        moveLeft = !moveLeft;
        Vector2 turn = transform.localScale;
        turn.x *= -1;
        transform.localScale = turn;
    }*/
}
