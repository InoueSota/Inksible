using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 自コンポーネント取得
    private SpriteRenderer spriteRenderer;

    // 他コンポーネント取得
    private GameManager gameManager;

    // タイプ
    public enum ColorType
    {
        COLOR1,
        COLOR2
    }
    [SerializeField] private ColorType colorType;

    // 色
    private Color color;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        color = gameManager.GetColor((int)colorType + 1);

        // リサイズ
        transform.localScale = new(0.5f, 0.5f, 1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
    
    // Setter
    public void SetIsActive(bool _isActive)
    {
        if (_isActive)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    // Getter
    public ColorType GetColorType()
    {
        return colorType;
    }
}
