using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
public class ProductEntry
{
    public int productNumber;
    public string productName;
    public float price;
    public string gender;
    public string productType;

    public ProductEntry(JSONNode data)
    {
        productNumber = data["productNumber"];
        productName = data["name"];
        price = data["price"];
        gender = data["gender"];
        productType = data["productType"];
    }

    public bool MatchesFilter(List<Filter> filters)
    {
        foreach (Filter filter in filters)
        {
            if (filter.predicate == "ProductType" && productType != filter.parameter) return false;

            if (filter.predicate == "Gender" && gender != filter.parameter) return false;

            if (filter.predicate == "Price")
            {
                if (filter.parameter == "<=25" && price > 25) return false;

                if (filter.parameter == ">=26" && price < 26) return false;
            }
        }

        return true;
    }
}

public class Catalogue : MonoBehaviour
{
    List<ProductEntry> productEntries = new List<ProductEntry>();

    public event Action onCatalogueInitialized;

    public void BuildCatalogue(JSONNode catalogueJSON)
    {
        foreach (JSONNode entry in catalogueJSON["data"]["cataloguesList"]["items"])
        {
            productEntries.Add(new ProductEntry(entry));
        }

        onCatalogueInitialized?.Invoke();
    }

    public ProductEntry GetRandomEntry()
    {
        return productEntries[UnityEngine.Random.Range(0, productEntries.Count)];
    }

    public List<ProductEntry> GetProductsWithFilter(List<Filter> filters)
    {
        List<ProductEntry> result = new List<ProductEntry>();

        foreach(ProductEntry entry in productEntries)
        {
            if (entry.MatchesFilter(filters)) result.Add(entry);
        }

        return result;
    }
}
