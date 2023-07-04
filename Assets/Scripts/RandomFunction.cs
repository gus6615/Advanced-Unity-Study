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

        // 확률 배열을 누적형으로 변경
        for (int i = 1; i < percents.Length; i++)
            percents[i] += percents[i - 1];

        // 랜덤값 범위를 (0, 1)에서 (0, percents)로 확장
        value *= percents[percents.Length - 1];

        // 랜덤 Index 반환
        for (int i = 1; i < percents.Length; i++)
            if (percents[i - 1] <= value && value < percents[i])
                return i;
        return 0;
    }
}
