using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D r2d;
    private AudioSource audioSource;

    [SerializeField] private float speed;
    [SerializeField] private bool onGround;
    [SerializeField] private bool turning;

    private float marioSpeed = 7f;
    private float highJump;//Nhấn giữ
    private float sortJump;//Ko nhấn giữ
    private float fallForce;//Lực hút trái đất tác dụng lên Mario

    private bool turnRight = true;//Biến kiểm tra xem mario có quay bên phải hay ko

    //Hiển thị độ lớn và lv
    public int level;
    public bool transForm;
    Vector2 diedPoint;
    GameObject marioDied;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        onGround = true;
        highJump = 450f;
        sortJump = 5f;
        fallForce = 5f;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", speed);
        anim.SetBool("OnGround", onGround);
        anim.SetBool("Turning", turning);
        MarioTransForm();
    }
    
    private void FixedUpdate()
    {
        MarioMovement();
        MarioJump();
    }

    private void MarioMovement()
    {
        float key = Input.GetAxis("Horizontal");
        
        r2d.velocity = new Vector2(marioSpeed * key, r2d.velocity.y);
        speed = Mathf.Abs(marioSpeed * key);
        if (key > 0 && !turnRight) MarioTurning();//Nếu đang đi sang phải mà mặt quay bên trái thì quay đầu
        if (key < 0 && turnRight) MarioTurning();//Ngược lại
    }

    private void MarioTurning()
    {
        //Nếu mario ko quay phải
        turnRight = !turnRight;
        Vector2 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
        if (marioSpeed > 0) StartCoroutine(MarioTurningFace());
    }

    private void MarioJump()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && onGround == true)
        {
            r2d.AddForce(Vector2.up * highJump);
            AudioClipController("MarioJumpSuper");
            onGround = false;

        }
        //Áp dụng lực hút trái đất cho mario rơi xuống nhanh hơn
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (fallForce - 1) * Time.deltaTime;
        }
        else if(r2d.velocity.y>0 && !Input.GetKey(KeyCode.UpArrow))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (sortJump - 1) * Time.deltaTime;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            onGround = true;
        }
    }

    IEnumerator MarioTurningFace()
    {
        turning = true;
        yield return new WaitForSeconds(0.1f);
        turning = false;
    }

    private void ThrowFireBall()
    {

    }

    private void MarioTransForm()
    {
        if(transForm == true)
        {
            switch (level)
            {
                case 0:
                    {
                        StartCoroutine(MarioNormalForm());
                        AudioClipController("MarioPipe");
                        transForm = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioAteMushRoom());
                        AudioClipController("MarioPowerUp");
                        transForm = false;
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioAteFlower());
                        AudioClipController("MarioPowerUp");
                        transForm = false;
                        break;
                    }
                    
                default:transForm = false; break;
            }
            
        }
        if (gameObject.transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator MarioNormalForm()
    {
        float delay = 0.1f;
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
    }
    IEnumerator MarioAteMushRoom()
    {
        float delay = 0.1f;
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0); 
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("Mario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
    }
    IEnumerator MarioAteFlower()
    {
        float delay = 0.1f;
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 1);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 1);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 1);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 0);
        yield return new WaitForSeconds(delay);
        anim.SetLayerWeight(anim.GetLayerIndex("BigMario"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("FlowerPower"), 1);
        yield return new WaitForSeconds(delay);
    }

    public void AudioClipController(string fileName)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sound/" + fileName));
    }

    public void MarioDied()
    {
        diedPoint = transform.localPosition;
        marioDied = (GameObject)Instantiate(Resources.Load("Prefabs/MarioDied"));
        marioDied.transform.localPosition = diedPoint;
        Destroy(gameObject);
    }
}
