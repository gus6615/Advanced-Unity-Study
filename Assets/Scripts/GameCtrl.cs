using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCtrl : MonoBehaviour, IPointerClickHandler
{
    private static GameCtrl Instance;
    public static GameCtrl instance
    {
        set 
        {
            if (Instance == null)
                Instance = value; 
        }
        get { return Instance; }
    }

    private const float CLICK_COOLTIME = 0.5f;

    /// <summary>
    /// 생성 될 광물 오브젝트 Prefab 
    /// </summary>
    [SerializeField]
    private GameObject jemObjectPrefab;

    /// <summary>
    /// 생성 될 광물 부모 Transform
    /// </summary>
    [SerializeField]
    private Transform jemObjectTr;

    public bool isCanClick;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isCanClick = true;
    }

    private void CreateJemObject(Vector2 _createPos)
    {
        JemObject jemObject = Instantiate(jemObjectPrefab, _createPos, Quaternion.identity, jemObjectTr).GetComponent<JemObject>();
        int jemCode = RandomFunction.RandomFlag(Jem.percents);
        jemObject.jemCode = jemCode;

        DataCtrl.instance.playerData.jems[jemCode]++;
    }

    IEnumerator ClickCoolTime()
    {
        float coolTime = CLICK_COOLTIME - DataCtrl.instance.playerData.upgrades[0] * 0.05f;
        if (coolTime < 0.1f)
            coolTime = 0.1f;

        isCanClick = false;
        yield return new WaitForSeconds(coolTime);
        isCanClick = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCanClick)
        {
            // 마우스 좌클릭 했을 때
            for (int i = 0; i < 1 + DataCtrl.instance.playerData.upgrades[2]; i++)
                CreateJemObject(Camera.main.ScreenToWorldPoint(eventData.position));
            StartCoroutine(ClickCoolTime());
        }
    }
}
