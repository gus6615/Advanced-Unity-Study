using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFunction
{
    /// <summary>
    /// (_percent) Ȯ���� ���� ���θ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_percent">0 ~ 1�� float ��</param>
    /// <returns></returns>
    public static bool RandomFlag(float _percent)
    {
        return Random.value <= _percent;
    }

    public static int RandomFlag(params float[] _percents)
    {
        float[] percent = (float[])_percents.Clone();
        float value = Random.value;

        // ���� �۾�
        for (int i = 1; i < percent.Length; i++)
            percent[i] += percent[i - 1];
        value *= percent[percent.Length - 1];

        // üũ
        for (int i = 1; i < percent.Length; i++)
            if (percent[i - 1] < value && value <= percent[i])
                return i;
        return 0;
    }
}
