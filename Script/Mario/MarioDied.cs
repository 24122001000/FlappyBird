using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioDied : MonoBehaviour
{
    [SerializeField]
    private Vector2 diePoint;
    public float speedf = 20f;
    public float high = 120f;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playDied();
    }

    private void playDied()
    {
        
        StartCoroutine(AMarioDied());
    }

    IEnumerator AMarioDied()
    {
        while (true)
        {
            //Thay đổi vị trí của block (nảy lên) bằng độ cao của khối cộng với bonceForce thì dừng
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speedf * Time.deltaTime);
            if (transform.localPosition.y >= diePoint.y+ high + 1) {
                break;
            }
            yield return null;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speedf * Time.deltaTime);
            if (transform.localPosition.y < -10f)
            {
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
}
