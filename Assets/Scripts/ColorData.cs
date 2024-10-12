using UnityEngine;

public class ColorData
{
    public Color[] mainColor;
    public Color[] subColor;

    public void Initialize()
    {
        mainColor = new Color[3];
        ColorUtility.TryParseHtmlString("#FF3399", out mainColor[0]);
        ColorUtility.TryParseHtmlString("#614942", out mainColor[1]);
        ColorUtility.TryParseHtmlString("#614942", out mainColor[2]);

        subColor = new Color[3];
        ColorUtility.TryParseHtmlString("#FF00FF", out subColor[0]);
        ColorUtility.TryParseHtmlString("#D29C6E", out subColor[1]);
        ColorUtility.TryParseHtmlString("#D29C6E", out subColor[2]);
    }

    public Color GetMainColor(int _num)
    {
        return mainColor[_num];
    }
    public Color GetSubColor(int _num)
    {
        return subColor[_num];
    }
}