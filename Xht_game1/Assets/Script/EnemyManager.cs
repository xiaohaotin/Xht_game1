using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    Rigidbody2D rigibody2D;
    float speed = 0;
    float jumpPower = 400;
    public enum MOVE_DIRECTION
    {
        STOP,
        LEFT,
        RIGHT,
    }
    MOVE_DIRECTION movedirection = MOVE_DIRECTION.RIGHT;

    // Start is called before the first frame update
    void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGround())
        {
            //向きを変える
            ChangeDirection();
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
                speed = -3;
                break;
            case MOVE_DIRECTION.RIGHT:
                transform.localScale = new Vector3(1, 1, 1);
                speed = 3;
                break;
        }
        rigibody2D.velocity = new Vector2(speed, rigibody2D.velocity.y);
    }
    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f* transform.localScale.x;
        Vector3 endtVec = startVec - transform.up * 1.0f;
        Debug.DrawLine(startVec,endtVec);
        return Physics2D.Linecast(startVec, endtVec, blockLayer); 
            
    }

    void ChangeDirection()
    {
        if (movedirection == MOVE_DIRECTION.RIGHT)
        {
            //左に移動
            movedirection = MOVE_DIRECTION.LEFT;
        }
        else
        {
            //右に移動
            movedirection = MOVE_DIRECTION.RIGHT;
        }

    }
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}

