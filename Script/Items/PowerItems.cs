using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItems : MonoBehaviour
{
    private GameObject mario;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (mario.GetComponent<MarioScript>().level < 2)
            {
                mario.GetComponent<MarioScript>().level += 1;
                mario.GetComponent<MarioScript>().transForm = true;
                Destroy(gameObject);
            }
        }
    }
}
