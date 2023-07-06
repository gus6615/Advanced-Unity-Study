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
        get { return Instance; }
    }

    public PlayerData playerData;
    public List<Jem> jems= new List<Jem>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerData = new PlayerData(0);
        for (int i = 0; i < Jem.JemNum; i++)
            jems.Add(new Jem(i));
    }
}

public struct PlayerData
{
    public long gold;
    public int[] jems;
    public int[] upgrades;

    public PlayerData(long _gold)
    {
        gold = _gold;
        jems = new int[Jem.JemNum];
        upgrades = new int[4];
    }
}

public class Jem
{
    public static int JemNum = 12;
    public static float[] percents = { 0.5f, 0.25f, 0.1f, 0.05f, 0.025f, 0.025f, 0.01f, 0.005f, 0.005f, 0.005f, 0.0025f, 0.001f };
    private static int[] prices = { 100, 200, 400, 1000, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int jemCode;
    public Sprite sprite;
    public int price;

    public Jem(int _jemCode)
    {
        jemCode = _jemCode;
        sprite = Resources.LoadAll<Sprite>("Jem")[jemCode];
        price = prices[jemCode];
    }
}

public class Upgrade
{
    public static int upgradeNum = 3;
}