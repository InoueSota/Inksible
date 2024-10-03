using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    // 自コンポーネント取得
    private AllObjectManager allObjectManager;

    // 他コンポーネント取得
    [SerializeField] private AllPlayerManager allPlayerManager;

    // 座標
    private Vector3 startPosition;
    private Vector3 endPosition;

    // 時間
    [SerializeField] private float timeMag;
    private float moveTime;
    private float moveTimer;

    // フラグ
    private bool isActive;

    // 生成対象
    [SerializeField] private GameObject circleFadeOutPrefab;
    [SerializeField] private float particleIntarvalTime;
    private float particleIntervalTimer;
    private Color particleColor;

    void Start()
    {
        allObjectManager = GetComponent<AllObjectManager>();
    }

    void Update()
    {
        Move();
        Particle();
    }

    void Move()
    {
        if (isActive)
        {
            moveTimer -= Time.deltaTime;
            moveTimer = Mathf.Clamp(moveTimer, 0f, moveTime);
            // 終了
            if (moveTimer <= 0f)
            {
                allPlayerManager.ChangePlayerActive();
                isActive = false;
            }

            // 終了していないとき
            float t = moveTimer / moveTime;
            transform.position = Vector3.Lerp(endPosition, startPosition, t);
        }
    }
    void Particle()
    {
        if (isActive)
        {
            particleIntervalTimer -= Time.deltaTime;

            if (particleIntervalTimer <= 0f)
            {
                GameObject circleFadeOut = Instantiate(circleFadeOutPrefab, transform.position, Quaternion.identity);
                circleFadeOut.GetComponent<SpriteRenderer>().color = particleColor;
                particleIntervalTimer = particleIntarvalTime;
            }
        }
    }

    public void SetStart(AllObjectManager.BlockType _blockType, Vector3 _startPosition, Vector3 _endPosition, Color _color)
    {
        allObjectManager.SetBlockType(_blockType);

        startPosition = _startPosition;
        endPosition = _endPosition;

        if (startPosition != endPosition)
        {
            moveTime = Vector3.Distance(startPosition, endPosition) * timeMag;
            isActive = true;
        }
        else
        {
            allPlayerManager.ChangePlayerActive();
        }
        moveTimer = moveTime;

        particleIntervalTimer = 0f;
        particleColor = _color;

    }
    public void Initialize()
    {
        isActive = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger2D(collision);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        OnTrigger2D(collision);
    }
    void OnTrigger2D(Collider2D collision)
    {
        if (collision.CompareTag("Block") && isActive)
        {
            if (collision.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
            {
                collision.GetComponent<AllObjectManager>().SetBlockType(allObjectManager.GetBlockType());
                collision.GetComponent<Animator>().SetTrigger("Change");
            }
        }
        else if (collision.CompareTag("Item"))
        {
            bool isGet = false;
            ItemManager hitItemManager = collision.GetComponent<ItemManager>();

            if (hitItemManager.GetColorType() == (ItemManager.ColorType)allObjectManager.GetBlockType())
            {
                hitItemManager.SetIsActive(false);
                isGet = true;
            }

            // Itemゲットフラグ
            if (isGet)
            {
                if (hitItemManager.GetColorType() == ItemManager.ColorType.COLOR1)
                {
                    GlobalVariables.isGetItem1 = true;
                }
                else
                {
                    GlobalVariables.isGetItem2 = true;
                }
            }
        }
    }
}
