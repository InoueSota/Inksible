using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    // 自コンポーネント取得
    private AllObjectManager allObjectManager;
    private SpriteRenderer spriteRenderer;

    // 基本情報
    private Vector2 halfSize;

    // 色変え線
    [SerializeField] private GameObject verticalLinePrefab;
    [SerializeField] private GameObject horizontalLinePrefab;

    // MainCameraのTransform
    private Transform mainCameraTransform;

    void Start()
    {
        allObjectManager = GetComponent<AllObjectManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        halfSize.x = transform.localScale.x * 0.5f;
        halfSize.y = transform.localScale.y * 0.5f;

        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    public void ChangeColor(GameObject _block, bool _isVertical)
    {
        if (_block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
        {
            _block.GetComponent<AllObjectManager>().SetBlockType(allObjectManager.GetBlockType());
            _block.GetComponent<Animator>().SetTrigger("Start");

            ColorLine(_block.transform.position, _isVertical);
        }
    }
    void ColorLine(Vector3 _blockPosition, bool _isVertical)
    {
        // 上下方向で接触
        if (_isVertical)
        {
            GameObject vertical = Instantiate(verticalLinePrefab, new(_blockPosition.x, mainCameraTransform.position.y, 0f), Quaternion.identity);
            vertical.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

            // 色を変える
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
                {
                    // X軸判定
                    float xBetween = Mathf.Abs(vertical.transform.position.x - block.transform.position.x);

                    if (xBetween <= 0.2f)
                    {
                        block.GetComponent<AllObjectManager>().SetBlockType(allObjectManager.GetBlockType());
                        block.GetComponent<Animator>().SetTrigger("Change");
                    }
                }
            }
        }
        // 左右方向で接触
        else
        {
            GameObject horizontal = Instantiate(horizontalLinePrefab, new(mainCameraTransform.position.x, _blockPosition.y, 0f), Quaternion.identity);
            horizontal.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

            // 色を変える
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
                {
                    // X軸判定
                    float yBetween = Mathf.Abs(horizontal.transform.position.y - block.transform.position.y);

                    if (yBetween <= 0.2f)
                    {
                        block.GetComponent<AllObjectManager>().SetBlockType(allObjectManager.GetBlockType());
                        block.GetComponent<Animator>().SetTrigger("Change");
                    }
                }
            }
        }
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
        if (collision.CompareTag("Item"))
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
