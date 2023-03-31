using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product Image Table", menuName = "Product Image Table")]
public class ProductImageTable : ScriptableObject
{
    [System.Serializable]
    struct ProductImageBinding
    {
        public int productNumber;
        public Sprite image;
    }

    [SerializeField] ProductImageBinding[] imageBindings;

    Dictionary<int, Sprite> imageLookup = null;

    public Sprite GetProductImage(int productNumber)
    {
        if (imageLookup == null)
        {
            BuildLookup();
        }

        return imageLookup[productNumber];
    }

    private void BuildLookup()
    {
        imageLookup = new Dictionary<int, Sprite>();

        foreach(ProductImageBinding imageBinding in imageBindings)
        {
            imageLookup.Add(imageBinding.productNumber, imageBinding.image);
        }
    }
}
