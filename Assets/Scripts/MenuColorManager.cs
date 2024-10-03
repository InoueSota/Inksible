using UnityEngine;
using UnityEngine.UI;

public class MenuColorManager : MonoBehaviour
{
    [Header("ImageŽæ“¾")]
    [SerializeField] private Image stageNumberBack;
    [SerializeField] private Image stageNumberFrame;
    [SerializeField] private Image tabBackGround;
    [SerializeField] private Image tabFrame;
    [SerializeField] private Image returnBackGround;
    [SerializeField] private Image restartBackGround;
    [SerializeField] private Image stageSelectBackGround;

    public void SetColor(Color _brightColor, Color _originalColor, Color _darkColor)
    {
        stageNumberBack.color = _brightColor;
        stageNumberFrame.color = _originalColor;
        tabFrame.color = _darkColor;
        tabBackGround.color = _originalColor;
        returnBackGround.color = _brightColor;
        restartBackGround.color = _brightColor;
        stageSelectBackGround.color = _brightColor;
    }
}
