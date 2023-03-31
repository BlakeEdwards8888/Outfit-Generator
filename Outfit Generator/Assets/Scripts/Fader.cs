using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CatalogueLoader catalogueLoader;

    private void Awake()
    {
        catalogueLoader = FindObjectOfType<CatalogueLoader>();
    }

    private void OnEnable()
    {
        catalogueLoader.onFinishedLoading += CataLogueLoader_OnFinishedLoading;
    }

    private void CataLogueLoader_OnFinishedLoading()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
    }

    private void OnDisable()
    {
        catalogueLoader.onFinishedLoading -= CataLogueLoader_OnFinishedLoading;
    }
}
