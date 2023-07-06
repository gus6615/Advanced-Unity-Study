using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradeCanvas;

    [SerializeField]
    private Transform upgradeContentTr;

    public TMP_Text goldText;

    private bool isOnUpgrade;

    UIBox temp_UIBox;
    UIBox[] temp_UIBoxes;

    private void Start()
    {
        upgradeCanvas.SetActive(false);
    }

    public void OnOffUpgradeButton()
    {
        isOnUpgrade = !isOnUpgrade;
        upgradeCanvas.SetActive(isOnUpgrade);

        if (isOnUpgrade)
        {
            Time.timeScale = 0f;

            SetContent();
        }
        else
            Time.timeScale = 1.0f;
    }

    private void SetGoldText()
    {
        goldText.SetText("Gold : " + DataCtrl.instance.playerData.gold + " (KRW)");
    }

    private void SetContent()
    {
        temp_UIBoxes = upgradeContentTr.GetComponentsInChildren<UIBox>();

        temp_UIBoxes[0].texts[0].SetText("(Lv." + DataCtrl.instance.playerData.upgrades[0] + ") Speed Up");
        temp_UIBoxes[0].texts[2].SetText(Mathf.Pow(2, DataCtrl.instance.playerData.upgrades[0]) * 100 + "\n(KRW)");
        temp_UIBoxes[1].texts[0].SetText("(Lv." + DataCtrl.instance.playerData.upgrades[1] + ") Jem Price Up");
        temp_UIBoxes[1].texts[2].SetText(Mathf.Pow(2, DataCtrl.instance.playerData.upgrades[1]) * 100 + "\n(KRW)");
        temp_UIBoxes[2].texts[0].SetText("(Lv." + DataCtrl.instance.playerData.upgrades[2] + ") Plus One Up");
        temp_UIBoxes[2].texts[2].SetText(Mathf.Pow(2, DataCtrl.instance.playerData.upgrades[2]) * 100 + "\n(KRW)");

        SetGoldText();
    }

    public void OnBuyButton()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            temp_UIBox = EventSystem.current.currentSelectedGameObject.GetComponentInParent<UIBox>();
            if (temp_UIBox != null)
            {
                int index = temp_UIBox.index;
                int price = (int)Mathf.Pow(2, DataCtrl.instance.playerData.upgrades[index]) * 100;

                if (DataCtrl.instance.playerData.gold >= price)
                {
                    DataCtrl.instance.playerData.upgrades[index]++;
                    DataCtrl.instance.playerData.gold -= price;
                    SetContent();
                }
            }
        }
    }
}
