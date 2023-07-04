using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFunction : MonoBehaviour
{
    public static bool GetRandFlag(float _percent)
    {
        return Random.value <= _percent;
    }

    public static int GetRandFlag(params float[] _percents)
    {
        float[] percents = (float[])_percents.Clone();
        float value = Random.value;

        // Ȯ�� �迭�� ���������� ����
        for (int i = 1; i < percents.Length; i++)
            percents[i] += percents[i - 1];

        // ������ ������ (0, 1)���� (0, percents)�� Ȯ��
        value *= percents[percents.Length - 1];

        // ���� Index ��ȯ
        for (int i = 1; i < percents.Length; i++)
            if (percents[i - 1] <= value && value < percents[i])
                return i;
        return 0;
    }
}
