using UnityEngine;

public class PlayerGoalManager : MonoBehaviour
{
    // 基本情報
    private Vector2 halfSize;

    // ゴールフラグ
    private bool isGoal;

    // ゴールオブジェクト
    private Vector3 goalPosition;
    private Vector2 goalHalfSize;

    void Start()
    {
        halfSize.x = transform.localScale.x * 0.5f;
        halfSize.y = transform.localScale.y * 0.5f;

        goalPosition = new(8.25f, -0.5f, 0f);
        goalHalfSize.x = 1.25f;
        goalHalfSize.y = 1.25f;

        isGoal = false;
    }
    public void Initialize()
    {
        isGoal = false;
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
            float goalLeftX = goalPosition.x - goalHalfSize.x;

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
