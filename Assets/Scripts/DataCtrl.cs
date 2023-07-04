using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCtrl : MonoBehaviour
{
    private static DataCtrl Instance;
    public static DataCtrl instance
    {
        set
        {
            if (Instance == null)
                Instance = value;
        }
        get
        {
            return Instance;
        }
    }

    public List<Jem> jems; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        jems = new List<Jem>();
        for (int i = 0; i < Jem.JemNum; i++)
            jems.Add(new Jem(i));
    }
}

public class Jem
{
    public static int JemNum = 12;
    public static float[] createPercents = { 0.6f, 0.25f, 0.1f, 0.05f };
    private static int[] prices = { 100, 200, 400, 1000, 2000, 3000, 5000, 10000, 20000, 30000, 50000, 100000 };

    public Sprite sprite;
    public int jemCode;
    public int price;

    public Jem(int _jemCode)
    {
        jemCode = _jemCode;
        sprite = Resources.LoadAll<Sprite>("Jem")[jemCode];
        price = prices[jemCode];
    }
}