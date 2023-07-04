using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// 플레이어 저장 데이터
/// </summary>
public struct PlayerData
{
    public long gold;
    public int[] jems;
    public int[] upgrades;

    public PlayerData(int _gold)
    {
        this.gold = _gold;
        this.jems = new int[Jem.JemNum];
        this.upgrades = new int[1];
    }
}

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl Instance;
    public static PlayerCtrl instance
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

    private const float CLICK_DEFAULT_TIME = 0.5f;

    public PlayerData playerData;
    public Transform jemTr;
    public GameObject jemPrefab;

    private float clickTime;
    private bool isClickOn;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData = new PlayerData(0);

        SetPlayerStat();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isClickOn)
            {
                Vector3 camaraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var jemObject = Instantiate(jemPrefab, new Vector3(camaraPos.x, camaraPos.y, 0f), Quaternion.identity , jemTr).GetComponent<JemObject>();
                jemObject.jemCode = RandomFunction.GetRandFlag(Jem.createPercents);
                StartCoroutine(ClickCoolTime());
            }
        }
    }

    IEnumerator ClickCoolTime()
    {
        isClickOn = true;
        yield return new WaitForSeconds(clickTime);
        isClickOn = false;
    }

    public void SetPlayerStat()
    {
        clickTime = CLICK_DEFAULT_TIME - playerData.upgrades[0] * 0.05f;
    }
}
