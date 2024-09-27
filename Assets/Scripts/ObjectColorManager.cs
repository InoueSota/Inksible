using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private GameManager gameManager;

    // ���R���|�[�l���g�擾
    private AllObjectManager allObjectManager;
    private SpriteRenderer spriteRenderer;

    // �Y���F���g���Ă��邩
    [SerializeField] private bool isActive = true;

    // �F
    private Color color1;
    private Color color2;
    private Color translucentColor1;
    private Color translucentColor2;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        allObjectManager = GetComponent<AllObjectManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        color1 = gameManager.GetColor1();
        color2 = gameManager.GetColor2();
        translucentColor1 = color1;
        translucentColor2 = color2;
        translucentColor1.a = 0.5f;
        translucentColor2.a = 0.5f;
    }

    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        switch (allObjectManager.GetBlockType())
        {
            case AllObjectManager.BlockType.WHITE:

                spriteRenderer.color = Color.white;

                break;

            case AllObjectManager.BlockType.COLOR1:

                if (isActive)
                {
                    spriteRenderer.color = color1;
                }
                else
                {
                    spriteRenderer.color = translucentColor1;
                }

                break;
            case AllObjectManager.BlockType.COLOR2:

                if (isActive)
                {
                    spriteRenderer.color = color2;
                }
                else
                {
                    spriteRenderer.color = translucentColor2;
                }

                break;
        }
    }

    public void SetIsActive(bool _isActive)
    {
        isActive = _isActive;
    }

}
