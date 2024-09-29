using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private SpriteRenderer spriteRenderer;

    // ���R���|�[�l���g�擾
    private GameManager gameManager;

    // �^�C�v
    public enum ColorType
    {
        COLOR1,
        COLOR2
    }
    [SerializeField] private ColorType colorType;

    // �F
    private Color color;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        color = gameManager.GetColor((int)colorType + 1);

        // ���T�C�Y
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
