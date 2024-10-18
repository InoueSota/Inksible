using DG.Tweening;
using UnityEngine;

public class FallParticleManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private SpriteRenderer spriteRenderer;

    [Header("�����p�����[�^�[")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float easeTime;

    // Setter
    public void Initialize(Color _color, float _targetY)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _color;

        // Sequence�̃C���X�^���X���쐬
        var sequence = DOTween.Sequence();

        // Append�œ����ǉ����Ă���
        sequence.Append(transform.DOMoveY(_targetY, easeTime)).SetEase(easeType);
        // Join�͂ЂƂO�̓���Ɠ����Ɏ��s�����
        sequence.Join(transform.DORotate(Vector3.forward * 360f, easeTime, RotateMode.FastBeyond360)).SetEase(easeType);

        // Play�Ŏ��s
        sequence.Play().OnComplete(() => { Destroy(gameObject); });
    }
}
