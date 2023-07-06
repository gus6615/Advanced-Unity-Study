using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject shopCanvas;

    [SerializeField]
    private GameObject shopSlotPrefab;

    [SerializeField]
    private Transform shopContentTr;

    public UIBox[] menuButtons;
    public TMP_Text goldText;

    private int menuIndex;
    private bool isOnShop;

    UIBox temp_UIBox;
    UIBox[] temp_UIBoxes;

    private void Start()
    {
        shopCanvas.SetActive(false);
        SetGoldText();
    }

    public void OnOffShopButton()
    {
        isOnShop = !isOnShop;
        shopCanvas.SetActive(isOnShop);

        if (isOnShop)
        {
            Time.timeScale = 0f;

            menuIndex = 0;
            SetMenuButton(menuIndex);
            SetContent(menuIndex);
        }
        else
            Time.timeScale = 1.0f;
    }

    private void SetGoldText()
    {
        goldText.SetText("Gold : " + DataCtrl.instance.playerData.gold + " (KRW)");
    }

    private void SetMenuButton(int _index)
    {
        for (int i = 0; i < menuButtons.Length; i++)
            menuButtons[i].images[0].color = new Color(0f, 0f, 0f, 0.2f);
        menuButtons[_index].images[0].color = new Color(0f, 0f, 0f, 0.1f);
    }

    private void SetContent(int _index)
    {
        switch (_index)
        {
            case 0: // Sell Content
                temp_UIBoxes = shopContentTr.GetComponentsInChildren<UIBox>();
                for (int i = 0; i < temp_UIBoxes.Length; i++)
                    Destroy(temp_UIBoxes[i].gameObject);

                for (int i = 0; i < Jem.JemNum; i++)
                {
                    if (DataCtrl.instance.playerData.jems[i] > 0)
                    {
                        temp_UIBox = Instantiate(shopSlotPrefab, shopContentTr).GetComponent<UIBox>();
                        temp_UIBox.images[0].sprite = DataCtrl.instance.jems[i].sprite;
                        temp_UIBox.texts[0].text = "x " + DataCtrl.instance.playerData.jems[i];
                    }
                }
                break;
            case 1: // Buy Content
                break;
        }
    }

    public void OnMenuButton()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            temp_UIBox = EventSystem.current.currentSelectedGameObject.GetComponent<UIBox>();
            if (temp_UIBox != null)
            {
                menuIndex = temp_UIBox.index;
                SetMenuButton(menuIndex);
                SetContent(menuIndex);
            }
        }
    }

    public void OnSellButton()
    {
        long getGold = 0;
        float plusPercent = 1f + 0.1f * DataCtrl.instance.playerData.upgrades[1];

        for (int i = 0; i < Jem.JemNum; i++)
        {
            getGold += DataCtrl.instance.playerData.jems[i] * DataCtrl.instance.jems[i].price;
            DataCtrl.instance.playerData.jems[i] = 0;
        }

        getGold = (long)(getGold * plusPercent);
        DataCtrl.instance.playerData.gold += getGold;
        SetContent(menuIndex);
        SetGoldText();
    }
}
