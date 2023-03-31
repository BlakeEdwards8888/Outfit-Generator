using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterToggle : MonoBehaviour
{
    [SerializeField] Filter filter;

    OutfitGenerator outfitGenerator;

    private void Start()
    {
        outfitGenerator = FindObjectOfType<OutfitGenerator>();
    }

    public void ToggleFilter()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle.isOn)
        {
            outfitGenerator.AddFilter(filter);
        }
        else
        {
            outfitGenerator.RemoveFilter(filter);
        }
    }
}
