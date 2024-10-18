using DG.Tweening;
using UnityEngine;

public class FallParticleManager : MonoBehaviour
{
    // 自コンポーネント取得
    private SpriteRenderer spriteRenderer;

    [Header("落下パラメーター")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float easeTime;

    // Setter
    public void Initialize(Color _color, float _targetY)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _color;

        // Sequenceのインスタンスを作成
        var sequence = DOTween.Sequence();

        // Appendで動作を追加していく
        sequence.Append(transform.DOMoveY(_targetY, easeTime)).SetEase(easeType);
        // Joinはひとつ前の動作と同時に実行される
        sequence.Join(transform.DORotate(Vector3.forward * 360f, easeTime, RotateMode.FastBeyond360)).SetEase(easeType);

        // Playで実行
        sequence.Play().OnComplete(() => { Destroy(gameObject); });
    }
}
