using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknowBoxController : MonoBehaviour
{
    private GameObject mario;

    private float bonceForce = 0.5f;
    private float bonceSpeed = 4f;
    private bool bonce = true;
    private Vector3 itemPosition;

    public bool isMushRoom = false;
    public bool isCoin = false;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nếu như đối tượng là mario và va chạm dưới khối vuông
        if (collision.collider.tag == "Collider" && collision.contacts[0].normal.y>0)
        {
            itemPosition = transform.position;
            BlockBonceCtrl();
            if (isMushRoom)
            {
                MushroomAndFlower();
            }
            else if(isCoin)
            {
                CoinCtrl();
            }
        }
    }

    private void BlockBonceCtrl()
    {
        if (bonce)
        {
            StartCoroutine(BlockBonces());
            bonce = false;
        }
    }

    IEnumerator BlockBonces()
    {
        //nảy lên
        while (true)
        {
            //Thay đổi vị trí của block (nảy lên) bằng độ cao của khối cộng với bonceForce thì dừng
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bonceSpeed*Time.deltaTime);
            if (transform.localPosition.y >= itemPosition.y + bonceForce) break;
            yield return null;
        }
        //Xuống
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bonceSpeed*Time.deltaTime);
            if (transform.localPosition.y <= itemPosition.y) break;
            Destroy(this.gameObject);
            GameObject spaceBlock = (GameObject)Instantiate(Resources.Load("Prefabs/SpaceBlock"));
            spaceBlock.transform.position = itemPosition;
            yield return null;
        }
    }
    private void MushroomAndFlower()
    {
        int currentLevel = mario.GetComponent<MarioScript>().level;
        GameObject item = null;
        if(currentLevel == 0)
        {
            item = (GameObject)Instantiate(Resources.Load("Prefabs/Mushroom"));
        }
        else
        {
            item = (GameObject)Instantiate(Resources.Load("Prefabs/Flower"));
        }
        mario.GetComponent<MarioScript>().AudioClipController("MarioUp");
        item.transform.SetParent(this.transform.parent);
        item.transform.localPosition = new Vector2(itemPosition.x, itemPosition.y + 1f);
    }
    private void CoinCtrl()
    {
        GameObject coin = (GameObject)Instantiate(Resources.Load("Prefabs/Coin"));
        coin.transform.SetParent(this.transform.parent);
        coin.transform.localPosition = new Vector2(itemPosition.x, itemPosition.y + 1f);
    }
}
