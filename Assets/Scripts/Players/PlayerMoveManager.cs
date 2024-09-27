using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    // 他コンポーネント取得
    private AllPlayerManager allPlayerManager;

    // 自コンポーネント取得
    private AllObjectManager allObjectManager;
    private PlayerHitManager playerHitManager;

    // 入力
    private InputManager inputManager;
    private bool isPushLeft;
    private bool isPushRight;
    private bool isTriggerJump;

    // 基本情報
    private Vector2 halfSize;
    private Vector2 cameraHalfSize;

    // 座標系
    private Vector3 originPosition;
    private Vector3 nextPosition;

    // 横移動
    [Header("横移動")]
    [SerializeField] private float maxSpeed;
    private float moveSpeed;
    private Vector3 moveDirection;

    // ジャンプ
    private bool isJumping;
    [Header("ジャンプ")]
    [SerializeField] private float jumpDistance;
    [SerializeField] private float jumpSpeed;
    private float jumpTarget;

    // 滞空
    private bool isHovering;
    [Header("滞空")]
    [SerializeField] private float hangTime;
    private float hangTimer;

    // 重力
    private bool isGravity;
    [Header("重力")]
    [SerializeField] private float gravityMax;
    [SerializeField] private float addGravity;
    private float gravityPower;

    void Start()
    {
        allObjectManager = GetComponent<AllObjectManager>();
        playerHitManager = GetComponent<PlayerHitManager>();
        inputManager = GetComponent<InputManager>();

        halfSize.x = transform.localScale.x * 0.5f;
        halfSize.y = transform.localScale.y * 0.5f;
        cameraHalfSize.x = Camera.main.ScreenToWorldPoint(new(Screen.width, 0f, 0f)).x;
        cameraHalfSize.y = Camera.main.ScreenToWorldPoint(new(0f, Screen.height, 0f)).y;
        originPosition = transform.position;
    }
    public void Initialize()
    {
        transform.position = originPosition;
        isJumping = false;
        isHovering = false;
        isGravity = false;
    }
    public void CreateInitialize(AllPlayerManager _allPlayerManager)
    {
        allPlayerManager = _allPlayerManager;
    }

    void Update()
    {
        if (allPlayerManager.GetIsPlayerActive(this))
        {
            GetInput();

            nextPosition = transform.position;

            Move();
            Jump();
            Hovering();
            Gravity();
            ClampInCamera();

            transform.position = nextPosition;
        }
    }

    void CheckDirection()
    {
        if (isPushLeft || isPushRight)
        {
            moveSpeed = maxSpeed;

            if (isPushLeft)
            {
                moveDirection = Vector3.left;
            }
            else if (isPushRight)
            {
                moveDirection = Vector3.right;
            }
        }
        else
        {
            moveSpeed = 0f;
        }
    }
    void Move()
    {
        // 移動方向の修正
        CheckDirection();

        // 移動
        float deltaMoveSpeed = moveSpeed * Time.deltaTime;
        nextPosition += deltaMoveSpeed * moveDirection;

        // ブロックとの衝突判定
        if (isPushLeft || isPushRight)
        {
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() != allObjectManager.GetDifferentBlockType())
                {
                    // X軸判定
                    float xBetween = Mathf.Abs(nextPosition.x - block.transform.position.x);
                    float xDoubleSize = halfSize.x + 0.5f;

                    // Y軸判定
                    float yBetween = Mathf.Abs(nextPosition.y - block.transform.position.y);
                    float yDoubleSize = halfSize.y + 0.45f;

                    if (yBetween < yDoubleSize && xBetween < xDoubleSize)
                    {
                        if (block.transform.position.x < nextPosition.x)
                        {
                            nextPosition.x = block.transform.position.x + 0.5f + halfSize.x;
                            break;
                        }
                        else
                        {
                            nextPosition.x = block.transform.position.x - 0.5f - halfSize.x;
                            break;
                        }
                    }
                }
            }
        }
    }
    void Jump()
    {
        // ジャンプ開始と初期化
        if (!isJumping && !isHovering && !isGravity && isTriggerJump)
        {
            jumpTarget = nextPosition.y + jumpDistance;
            isJumping = true;
        }

        // ジャンプ処理
        if (isJumping)
        {
            float deltaJumpSpeed = jumpSpeed * Time.deltaTime;
            nextPosition.y += (jumpTarget - nextPosition.y) * deltaJumpSpeed;

            // ジャンプ終了処理
            if (Mathf.Abs(jumpTarget - nextPosition.y) < 0.03f)
            {
                nextPosition.y = jumpTarget;

                hangTimer = hangTime;
                isHovering = true;
                isJumping = false;
            }

            // ブロックとの衝突判定
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() != allObjectManager.GetDifferentBlockType())
                {
                    // X軸判定
                    float xBetween = Mathf.Abs(nextPosition.x - block.transform.position.x);
                    float xDoubleSize = halfSize.x + 0.45f;

                    // Y軸判定
                    float yBetween = Mathf.Abs(nextPosition.y - block.transform.position.y);
                    float yDoubleSize = halfSize.y + 0.5f;

                    if (xBetween < xDoubleSize && yBetween < yDoubleSize)
                    {
                        if (nextPosition.y < block.transform.position.y)
                        {
                            nextPosition.y = block.transform.position.y - 0.5f - halfSize.y;
                            isJumping = false;
                            break;
                        }
                    }
                }
            }

        }
    }
    void Hovering()
    {
        if (isHovering)
        {
            hangTimer -= Time.deltaTime;
            if (hangTimer <= 0f) { isHovering = false; }
        }
    }
    void Gravity()
    {
        if (!isGravity)
        {
            if (!isJumping && !isHovering)
            {
                // ブロックとの衝突判定
                bool noBlock = true;

                foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
                {
                    if (block.GetComponent<AllObjectManager>().GetBlockType() != allObjectManager.GetDifferentBlockType())
                    {
                        // X軸判定
                        float xBetween = Mathf.Abs(nextPosition.x - block.transform.position.x);
                        float xDoubleSize = halfSize.x + 0.45f;

                        // Y軸判定
                        float yBetween = Mathf.Abs(nextPosition.y - block.transform.position.y);
                        float yDoubleSize = halfSize.y + 0.5f;

                        if (yBetween <= yDoubleSize && xBetween < xDoubleSize)
                        {
                            if (nextPosition.y > block.transform.position.y)
                            {
                                noBlock = false;
                                break;
                            }
                        }
                    }
                }

                if (noBlock)
                {
                    gravityPower = 0f;
                    isGravity = true;
                }
            }
        }
        else
        {
            gravityPower += addGravity * Time.deltaTime;

            float deltaGravityPower = gravityPower * Time.deltaTime;
            nextPosition.y -= deltaGravityPower;

            // ブロックとの衝突判定
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() != allObjectManager.GetDifferentBlockType())
                {
                    // X軸判定
                    float xBetween = Mathf.Abs(nextPosition.x - block.transform.position.x);
                    float xDoubleSize = halfSize.x + 0.45f;

                    // Y軸判定
                    float yBetween = Mathf.Abs(nextPosition.y - block.transform.position.y);
                    float yDoubleSize = halfSize.y + 0.5f;

                    if (xBetween < xDoubleSize && yBetween < yDoubleSize)
                    {
                        if (nextPosition.y > block.transform.position.y)
                        {
                            nextPosition.y = block.transform.position.y + 0.5f + halfSize.y;
                            isGravity = false;
                            break;
                        }
                    }
                }
            }
        }
    }
    void ClampInCamera()
    {
        // 左端を超えたか
        float thisLeftX = nextPosition.x - halfSize.x;
        if (thisLeftX < -cameraHalfSize.x)
        {
            nextPosition.x = -cameraHalfSize.x + halfSize.x;
        }

        // 右端を超えたか
        float thisRightX = nextPosition.x + halfSize.x;
        if (thisRightX > cameraHalfSize.x)
        {
            nextPosition.x = cameraHalfSize.x - halfSize.x;
        }
    }

    void GetInput()
    {
        isPushLeft = false;
        isPushRight = false;
        isTriggerJump = false;

        if (inputManager.IsPush(InputManager.INPUTPATTERN.HORIZONTAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.HORIZONTAL) < -0.1f)
            {
                isPushLeft = true;
            }
            else if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.HORIZONTAL) > 0.1f)
            {
                isPushRight = true;
            }
        }
        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerJump = true;
        }
    }
}
