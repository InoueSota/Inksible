using UnityEngine;

public class FallManager : MonoBehaviour
{
    [Header("�����Ώ�")]
    [SerializeField] private GameObject fallPrefab;

    [Header("�����Ԋu")]
    [SerializeField] private float createIntervalMax;
    [SerializeField] private float createIntervalMin;
    private float createIntervalTimer;

    [Header("�����̂��炵")]
    [SerializeField] private float heightDiff;

    // �����͈�
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
            // �����ʒu���J�������Ɏ��܂�͈͂Ń����_���ɐݒ肷��
            Vector3 createPosition = Vector3.zero;
            createPosition.x = Random.Range(-cameraHalfSize.x, cameraHalfSize.x);
            createPosition.y = cameraHalfSize.y + heightDiff;

            // �ΏۃI�u�W�F�N�g�𐶐�
            GameObject fallParticle = Instantiate(fallPrefab, createPosition, Quaternion.identity);

            // �����_���ɐݒ肳�ꂽ�l���Q�Ƃ��A�F��ݒ肷��
            if (Random.Range(0, 99) % 2 == 0)
            {
                fallParticle.GetComponent<FallParticleManager>().Initialize(GlobalVariables.color1, -createPosition.y);
            }
            else
            {
                fallParticle.GetComponent<FallParticleManager>().Initialize(GlobalVariables.color2, -createPosition.y);
            }

            // �����Ԋu�������_���ɐݒ肷��
            createIntervalTimer = Random.Range(createIntervalMin, createIntervalMax);
        }
    }
}
