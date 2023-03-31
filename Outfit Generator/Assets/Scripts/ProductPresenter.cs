using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductPresenter : MonoBehaviour
{
    enum ProductType
    {
        Top,
        Bottom,
        Accessory
    }

    [SerializeField] ProductImageTable productImageTable;
    [SerializeField] Image productImage;
    [SerializeField] TMP_Text productNameText;
    [SerializeField] TMP_Text productPriceText;
    [SerializeField] ProductType productType;

    OutfitGenerator outfitGenerator;
    private void Awake()
    {
        outfitGenerator = FindObjectOfType<OutfitGenerator>();
    }

    private void OnEnable()
    {
        outfitGenerator.onOutfitChanged += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        ProductEntry myProduct = null;

        switch (productType)
        {
            case ProductType.Accessory:
                myProduct = outfitGenerator.GetAccessory();
                break;
            case ProductType.Top:
                myProduct = outfitGenerator.GetTop();
                break;
            case ProductType.Bottom:
                myProduct = outfitGenerator.GetBottom();
                break;
        }

        productImage.sprite = productImageTable.GetProductImage(myProduct.productNumber);
        productNameText.text = myProduct.productName.ToLower();
        productPriceText.text = $"${myProduct.price}";
    }

    private void OnDisable()
    {
        outfitGenerator.onOutfitChanged -= UpdateDisplay;
    }
}
