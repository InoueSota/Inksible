using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // �^�C�v
    public enum ColorType
    {
        COLOR1,
        COLOR2
    }
    private ColorType colorType;

    // ���R���|�[�l���g�擾
    private SpriteRenderer spriteRenderer;

    // �F
    private Color color;

    void Update()
    {
        
    }

    // Setter
    public void Initialize(ColorType _colorType, Color _color)
    {
        colorType = _colorType;
        color = _color;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
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
