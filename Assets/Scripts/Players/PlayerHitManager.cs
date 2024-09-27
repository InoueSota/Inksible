using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private AllObjectManager allObjectManager;

    // ��{���
    private Vector2 halfSize;

    void Start()
    {
        allObjectManager = GetComponent<AllObjectManager>();

        halfSize.x = transform.localScale.x * 0.5f;
        halfSize.y = transform.localScale.y * 0.5f;
    }

    void LateUpdate()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.WHITE)
            {
                // X������
                float xBetween = Mathf.Abs(transform.position.x - block.transform.position.x);
                float xDoubleSize = halfSize.x + block.transform.localScale.x * 0.5f;

                // Y������
                float yBetween = Mathf.Abs(transform.position.y - block.transform.position.y);
                float yDoubleSize = halfSize.y + block.transform.localScale.y * 0.5f;

                if (xBetween <= xDoubleSize && yBetween <= yDoubleSize)
                {
                    block.GetComponent<AllObjectManager>().SetBlockType(allObjectManager.GetBlockType());
                    block.GetComponent<Animator>().SetTrigger("Start");
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

            // Item�Q�b�g�t���O
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
