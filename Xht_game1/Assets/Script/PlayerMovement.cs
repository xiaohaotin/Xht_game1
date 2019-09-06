using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;
    Rigidbody2D rigibody2D;
    float speed = 0;
    float jumpPower = 400;
    public enum MOVE_DIRECTION {
        STOP,
        LEFT,
        RIGHT,
    }
    MOVE_DIRECTION movedirection = MOVE_DIRECTION.STOP;
    
    // Start is called before the first frame update
    void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x == 0)
        {
            //止まる
            movedirection = MOVE_DIRECTION.STOP;
        }
        else if (x > 0)
        {
            //右に移動
            movedirection = MOVE_DIRECTION.RIGHT;
        }
        else if (x < 0)
        {
            //左に移動
            movedirection = MOVE_DIRECTION.LEFT;
        }
        if ( IsGround() && Input.GetKeyDown("space"))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        switch (movedirection)
        {
            case MOVE_DIRECTION.STOP:
                speed = 0;
                break;
            case MOVE_DIRECTION.LEFT:
                transform.localScale = new Vector3(-1,1,1);
                speed = -4;
                break;
            case MOVE_DIRECTION.RIGHT:
                transform.localScale = new Vector3(1, 1, 1);
                speed = 4;
                break;
        }
        rigibody2D.velocity = new Vector2(speed, rigibody2D.velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
    }
    bool IsGround()
    {
        return Physics2D.Linecast(transform.position-transform.right*0.3f, transform.position - transform.up * 0.1f,blockLayer)
            || Physics2D.Linecast(transform.position + transform.right * 0.3f, transform.position - transform.up * 0.1f, blockLayer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            //Debug.Log("GameOver");
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Finish")
        {
            //Debug.Log("Finish");
            gameManager.GameClear();
        }
        if (collision.gameObject.tag == "Item")
        {
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if (this.transform.position.y > enemy.transform.position.y)
            {
                //敵を踏んだら
                //プレイヤーをジャンプさせる
                rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                //敵をぶつかったら
                DestroyPLayer();
                gameManager.GameOver();
            }
        }
    }
    void DestroyPLayer()
    {
        Destroy(this.gameObject);
    }
}
