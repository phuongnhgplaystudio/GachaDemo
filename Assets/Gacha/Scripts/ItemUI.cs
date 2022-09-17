using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemUI : MonoBehaviour
{
    public Image icon;
    public Text rarityText;
    public Text itemName;

    public void SetInfo(Rarity rarityType)
    {

        switch (rarityType)
        {
            case Rarity.Normal:
                {
                    icon.color = Color.gray;
                    rarityText.color = Color.black;
                    itemName.color = Color.black;
                    rarityText.text = Rarity.Normal.ToString();
                    itemName.text = GachaController.Instance.normalHeroes[UnityEngine.Random.Range(0, GachaController.Instance.normalHeroes.Count)].ToString();
                    break;
                }
            case Rarity.Rare:
                {
                    icon.color = Color.blue;
                    rarityText.color = Color.black;
                    itemName.color = Color.black;
                    rarityText.text = Rarity.Rare.ToString();
                    itemName.text = GachaController.Instance.rareHeroes[UnityEngine.Random.Range(0, GachaController.Instance.rareHeroes.Count)].ToString();
                    break;
                }
            case Rarity.Legend:
                {
                    icon.color = Color.red;
                    rarityText.color = Color.red;
                    itemName.color = Color.red;
                    rarityText.text = Rarity.Legend.ToString();
                    itemName.text = GachaController.Instance.legendHeroes[UnityEngine.Random.Range(0, GachaController.Instance.legendHeroes.Count)].ToString();
                    break;
                }
        }
        transform.DOLocalRotate(new Vector3(0, 0, 0), 0.75f).SetEase(Ease.OutQuad);

        //Them item vao database o day
        //
    }
}
