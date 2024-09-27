using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLineManager : MonoBehaviour
{
    private bool isActive;

    // êF
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    public enum ColorMode
    {
        COLOR1,
        COLOR2
    }
    private ColorMode colorMode;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive)
        {
            float thisLeft = transform.position.x - transform.localScale.x * 0.5f;

            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
                {
                    if (thisLeft < block.transform.position.x)
                    {
                        switch (colorMode)
                        {
                            case ColorMode.COLOR1:
                                block.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.COLOR1);
                                break;
                            case ColorMode.COLOR2:
                                block.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.COLOR2);
                                break;
                        }
                        block.GetComponent<Animator>().SetTrigger("Start");
                    }
                }
            }
        }
    }

    public void SetStart(ColorMode _colorMode, Color _color)
    {
        if (!isActive)
        {
            colorMode = _colorMode;
            spriteRenderer.color = _color;
            animator.SetBool("IsActive", true);
            isActive = true;
        }
    }
    public void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsActive", false);
        isActive = false;
    }
    public void FinishChangeLine()
    {
        animator.SetBool("IsActive", false);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().FinishChangeLine();
        gameObject.SetActive(false);
    }
}
