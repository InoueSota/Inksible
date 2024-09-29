using System.IO;
using UnityEngine;

[System.Serializable]
public class ColorData
{
    public string[] MainColor;
    public string[] SubColor;

    public ColorData LoadColorData()
    {
        StreamReader reader;

        #if UNITY_EDITOR
            reader = new StreamReader("Assets/Resources/ColorData.json");
        #else
            reader = new StreamReader(Application.dataPath + "/StreamingAssets/ColorData.json");
        #endif

        string datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<ColorData>(datastr);
    }

    public void Save(ColorData _colorData)
    {
        string jsonstr = JsonUtility.ToJson(_colorData);
        StreamWriter writer;

        #if UNITY_EDITOR
            writer = new StreamWriter("Assets/Resources/ColorData.json", false);
        #else
            writer = new StreamWriter(Application.dataPath + "/StreamingAssets/ColorData.json", false);
        #endif

        writer.WriteLine(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public Color GetMainColor(ColorData _colorData, int _num)
    {
        // �ꎞ�ۑ��̐F���m�ۂ���
        Color tmpColor = Color.white;

        // �F��������
        ColorUtility.TryParseHtmlString(_colorData.MainColor[_num], out tmpColor);

        // �ϊ��ɐ������Ă�����e�F���A���s���Ă����甒�F���Ԃ����
        return tmpColor;
    }
    public Color GetSubColor(ColorData _colorData, int _num)
    {
        // �ꎞ�ۑ��̐F���m�ۂ���
        Color tmpColor = Color.white;

        // �F��������
        ColorUtility.TryParseHtmlString(_colorData.SubColor[_num], out tmpColor);

        // �ϊ��ɐ������Ă�����e�F���A���s���Ă����甒�F���Ԃ����
        return tmpColor;
    }
}