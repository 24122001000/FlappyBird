using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject mario;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0))
        {
            if(mario.GetComponent<MarioScript>().level > 0)
            {
                mario.GetComponent<MarioScript>().level --;
                mario.GetComponent<MarioScript>().transForm = true;
            }
            else
            {
                mario.GetComponent<MarioScript>().MarioDied();
            }
        }
    }
}
