using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GachaController : MonoBehaviour
{
    public static GachaController Instance;

    public GameObject itemUIPrefab;
    public Transform contentView;

    public Text scrollsText;
    public Button gacha1Button;
    public Button gacha10Button;

    public int scrollsAmount;

    [Header("Gacha config")]
    public GachaConfig gachaConfig;

    [Header("Item/Heroes group")]
    public List<Item> normalHeroes;
    public List<Item> rareHeroes;
    public List<Item> legendHeroes;

    private int drawedCount;

    public bool enableClick = true;

    private void Awake()
    {
        drawedCount = 0; //Neu lam game that thi phai save gia tri nay vao json/prefs

        Instance = this;

        gacha1Button.onClick.AddListener(() =>
        {
            if (!enableClick) return;
            Gacha(1);
        });
        gacha10Button.onClick.AddListener(() =>
        {
            if (!enableClick) return;
            Gacha(10);
        });
    }

    public void Gacha(int drawCount)
    {
        scrollsAmount -= drawCount;
        scrollsText.text = scrollsAmount.ToString();
        enableClick = false;
        foreach(Transform c in contentView)
        {
            Destroy(c.gameObject);
        }
        DG.Tweening.Sequence s = DOTween.Sequence();

        for (int i = 1; i <= drawCount; i++)
        {
            int tempI = i;
            drawedCount++;
            s.AppendInterval(0.1f);
            s.AppendCallback(() =>
            {
                ItemUI itemUI = UnityEngine.Object.Instantiate(itemUIPrefab, contentView).GetComponent<ItemUI>();
                Rarity itemRarity = DrawFromConfig();
                if (drawedCount >= gachaConfig.minDrawsSecureToGetLegend)
                {
                    itemRarity = Rarity.Legend;
                    drawedCount = 0;
                }
                itemUI.SetInfo(itemRarity);
                if(tempI == drawCount)
                {
                    enableClick = true;
                }
            });
        }
    }

    public Rarity DrawFromConfig()
    {
        float randomFloat = UnityEngine.Random.Range(0f, 100f);
        Dictionary<Rarity, float> rarityRelatives = new Dictionary<Rarity, float>();
        float currentValue = 0f;
        int currentRarityTypeIndex = 0;
        foreach (var i in gachaConfig.rarityConfigs)
        {
            currentValue += i.percentage;
            rarityRelatives[(Rarity)currentRarityTypeIndex] = currentValue;
            currentRarityTypeIndex++;
        }
        foreach (var i in rarityRelatives)
        {
            Debug.Log(i.Key + ": " + i.Value);
        }
        //Check
        float progressValue = 0f;
        foreach (var i in rarityRelatives)
        {
            if (randomFloat >= progressValue && randomFloat <= i.Value)
            {
                return i.Key;
            }
            else
            {
                progressValue = i.Value;
            }
        }
        return (Rarity)0;
    }
}

[Serializable]
public class GachaConfig
{
    public List<RarityConfig> rarityConfigs;
    public int minDrawsSecureToGetLegend; //bao dam sau bao nhieu lan draw thi ra legend
}

[Serializable]
public class RarityConfig
{
    public Rarity rarityType;
    public float percentage;
}

public enum Rarity
{
    Normal = 0,
    Rare = 1,
    Legend = 2
}

public enum Item
{
    Edward,
    Mafew,
    Charles,
    John,
    Peter,
    Vladmir,
    Davidson,

    Miltida,
    RobertLionHeart,
    Wellington,
    Barbaross,
    JohnAdam,
    MacNara,

    Ceasar,
    GhenhisKhan,
    Alexander,
    SinSuiHuang,
    SunWu,
    Napoleon,
    Washington
}
