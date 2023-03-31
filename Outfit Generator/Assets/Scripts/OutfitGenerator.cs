using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitGenerator : MonoBehaviour
{
    [SerializeField] Catalogue catalogue;

    ProductEntry top, bottom, accessory;

    List<ProductEntry> tops = new List<ProductEntry>();
    List<ProductEntry> bottoms = new List<ProductEntry>();
    List<ProductEntry> accessories = new List<ProductEntry>();

    List<Filter> activeFilters = new List<Filter>();

    public event Action onOutfitChanged;

    private void OnEnable()
    {
        catalogue.onCatalogueInitialized += Catalogue_OnCatalogueInitialized;
    }

    private void Start()
    {
        UpdateProductLists();
    }

    void UpdateProductLists()
    {
        tops.Clear();
        bottoms.Clear();

        Filter topFilter = new Filter("ProductType", "Top");
        Filter bottomFilter = new Filter("ProductType", "Bottom");
        Filter accessoryFilter = new Filter("ProductType", "Accessory");

        activeFilters.Add(topFilter);

        tops = catalogue.GetProductsWithFilter(activeFilters);

        activeFilters.Remove(topFilter);
        activeFilters.Add(bottomFilter);

        bottoms = catalogue.GetProductsWithFilter(activeFilters);

        activeFilters.Remove(bottomFilter);
        activeFilters.Add(accessoryFilter);

        accessories = catalogue.GetProductsWithFilter(activeFilters);

        activeFilters.Remove(accessoryFilter);
    }

    public void GenerateOutift()
    {
        top = tops[UnityEngine.Random.Range(0, tops.Count)];
        bottom = bottoms[UnityEngine.Random.Range(0, bottoms.Count)];
        accessory = accessories[UnityEngine.Random.Range(0, accessories.Count)];

        onOutfitChanged?.Invoke();
    }

    public ProductEntry GetTop()
    {
        return top;
    }

    public ProductEntry GetBottom()
    {
        return bottom;
    }

    public ProductEntry GetAccessory()
    {
        return accessory;
    }

    public void AddFilter(Filter filter)
    {
        activeFilters.Add(filter);
        UpdateProductLists();
    }

    public void RemoveFilter(Filter filter)
    {
        activeFilters.Remove(filter);
        UpdateProductLists();
    }

    private void Catalogue_OnCatalogueInitialized()
    {
        UpdateProductLists();
        GenerateOutift();
    }

    private void OnDisable()
    {
        catalogue.onCatalogueInitialized -= Catalogue_OnCatalogueInitialized;
    }
}
