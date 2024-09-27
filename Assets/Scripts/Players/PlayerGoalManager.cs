using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoalManager : MonoBehaviour
{
    // 基本情報
    private Vector2 halfSize;

    // ゴールフラグ
    private bool isGoal;

    // ゴールオブジェクト
    private Transform goalTransform;
    private Vector2 goalHalfSize;

    void Start()
    {
        halfSize.x = transform.localScale.x * 0.5f;
        halfSize.y = transform.localScale.y * 0.5f;

        isGoal = false;
    }
    public void Initialize()
    {
        isGoal = false;
    }
    public void CreateInitialize(Transform _goalTransform)
    {
        goalTransform = _goalTransform;
        goalHalfSize.x = goalTransform.localScale.x * 0.5f;
        goalHalfSize.y = goalTransform.localScale.y * 0.5f;
    }


    void LateUpdate()
    {
        CheckIsGoal();
    }

    void CheckIsGoal()
    {
        if (!isGoal)
        {
            float thisRightX = transform.position.x + halfSize.x;
            float goalLeftX = goalTransform.position.x - goalHalfSize.x;

            if (goalLeftX < thisRightX)
            {
                gameObject.SetActive(false);
                isGoal = true;
            }
        }
    }

    public bool GetIsGoal()
    {
        return isGoal;
    }
}
