using UnityEngine;

public class InkSpawnManager : MonoBehaviour
{
    [Header("生成対象")]
    [SerializeField] private GameObject inkPrefab;

    [Header("生成間隔")]
    [SerializeField] private float createIntervalMax;
    [SerializeField] private float createIntervalMin;
    private float createIntervalTimer;

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
            createPosition.y = Random.Range(-cameraHalfSize.y, cameraHalfSize.y);

            // 生成時回転量をランダムに設定する
            float createRotationZ = Random.Range(0f, 360f);

            // 対象オブジェクトを生成
            GameObject inkParticle = Instantiate(inkPrefab, createPosition, Quaternion.Euler(0f, 0f, createRotationZ));

            // ランダムに設定された値を参照し、色を設定する
            if (Random.Range(0, 99) % 2 == 0)
            {
                inkParticle.GetComponent<InkParticleManager>().Initialize(GlobalVariables.color1);
            }
            else
            {
                inkParticle.GetComponent<InkParticleManager>().Initialize(GlobalVariables.color2);
            }

            // 生成間隔をランダムに設定する
            createIntervalTimer = Random.Range(createIntervalMin, createIntervalMax);
        }
    }
}
