using UnityEngine;

public class FallManager : MonoBehaviour
{
    [Header("生成対象")]
    [SerializeField] private GameObject fallPrefab;

    [Header("生成間隔")]
    [SerializeField] private float createIntervalMax;
    [SerializeField] private float createIntervalMin;
    private float createIntervalTimer;

    [Header("高さのずらし")]
    [SerializeField] private float heightDiff;

    // 生成範囲
    private Vector2 cameraHalfSize;

    void Start()
    {
        cameraHalfSize.x = Camera.main.ScreenToWorldPoint(new(Screen.width, 0f, 0f)).x;
        cameraHalfSize.y = Camera.main.ScreenToWorldPoint(new(0f, Screen.height, 0f)).y;
    }

    void Update()
    {
        createIntervalTimer -= Time.deltaTime;

        if (createIntervalTimer <= 0f)
        {
            // 生成位置をカメラ内に収まる範囲でランダムに設定する
            Vector3 createPosition = Vector3.zero;
            createPosition.x = Random.Range(-cameraHalfSize.x, cameraHalfSize.x);
            createPosition.y = cameraHalfSize.y + heightDiff;

            // 対象オブジェクトを生成
            GameObject fallParticle = Instantiate(fallPrefab, createPosition, Quaternion.identity);

            // ランダムに設定された値を参照し、色を設定する
            if (Random.Range(0, 99) % 2 == 0)
            {
                fallParticle.GetComponent<FallParticleManager>().Initialize(GlobalVariables.color1, -createPosition.y);
            }
            else
            {
                fallParticle.GetComponent<FallParticleManager>().Initialize(GlobalVariables.color2, -createPosition.y);
            }

            // 生成間隔をランダムに設定する
            createIntervalTimer = Random.Range(createIntervalMin, createIntervalMax);
        }
    }
}
