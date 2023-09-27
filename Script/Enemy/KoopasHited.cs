using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopasHited : MonoBehaviour
{
    private Vector2 enemyPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            Destroy(gameObject);
            GameObject KoopasHited = (GameObject)Instantiate(Resources.Load("Prefabs/KoopasHited"));
            KoopasHited.transform.localPosition = enemyPosition;
        }
    }
}
