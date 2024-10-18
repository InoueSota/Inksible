using UnityEngine;

public class InkSpawnManager : MonoBehaviour
{
    [Header("�����Ώ�")]
    [SerializeField] private GameObject inkPrefab;

    [Header("�����Ԋu")]
    [SerializeField] private float createIntervalMax;
    [SerializeField] private float createIntervalMin;
    private float createIntervalTimer;

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
            createPosition.y = Random.Range(-cameraHalfSize.y, cameraHalfSize.y);

            // ��������]�ʂ������_���ɐݒ肷��
            float createRotationZ = Random.Range(0f, 360f);

            // �ΏۃI�u�W�F�N�g�𐶐�
            GameObject inkParticle = Instantiate(inkPrefab, createPosition, Quaternion.Euler(0f, 0f, createRotationZ));

            // �����_���ɐݒ肳�ꂽ�l���Q�Ƃ��A�F��ݒ肷��
            if (Random.Range(0, 99) % 2 == 0)
            {
                inkParticle.GetComponent<InkParticleManager>().Initialize(GlobalVariables.color1);
            }
            else
            {
                inkParticle.GetComponent<InkParticleManager>().Initialize(GlobalVariables.color2);
            }

            // �����Ԋu�������_���ɐݒ肷��
            createIntervalTimer = Random.Range(createIntervalMin, createIntervalMax);
        }
    }
}
