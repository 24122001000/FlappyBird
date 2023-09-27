using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombasDied : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GDied());
    }

    IEnumerator GDied()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
