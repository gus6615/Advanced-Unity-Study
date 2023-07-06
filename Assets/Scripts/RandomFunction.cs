using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFunction
{
    /// <summary>
    /// (_percent) 확률로 성공 여부를 반환하는 함수
    /// </summary>
    /// <param name="_percent">0 ~ 1의 float 값</param>
    /// <returns></returns>
    public static bool RandomFlag(float _percent)
    {
        return Random.value <= _percent;
    }

    public static int RandomFlag(params float[] _percents)
    {
        float[] percent = (float[])_percents.Clone();
        float value = Random.value;

        // 구간 작업
        for (int i = 1; i < percent.Length; i++)
            percent[i] += percent[i - 1];
        value *= percent[percent.Length - 1];

        // 체크
        for (int i = 1; i < percent.Length; i++)
            if (percent[i - 1] < value && value <= percent[i])
                return i;
        return 0;
    }
}
