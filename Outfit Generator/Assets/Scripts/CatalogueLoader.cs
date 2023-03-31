using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class CatalogueLoader : MonoBehaviour
{
    [SerializeField] string endpoint;
    [SerializeField] string token;

    [TextArea(10, 50)]
    [SerializeField] string baseQuery;
    [SerializeField] Catalogue catalogue;

    public event Action onFinishedLoading;

    void Start()
    {
        StartCoroutine(Upload(GenerateQuery()));
    }

    string GenerateQuery()
    {
        JSONObject newQuery = new JSONObject();

        newQuery.Add("query", baseQuery);

        return newQuery.ToString();
    }

    IEnumerator Upload(string query)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(endpoint, query))
        {            
            www.method = "POST"; //method has to be changed like this otherwise Unity will corrupt the query
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", $"Bearer {token}");

            yield return www.SendWebRequest();

            JSONNode result = JSON.Parse(www.downloadHandler.text);
            catalogue.BuildCatalogue(result);

            onFinishedLoading?.Invoke();
        }
    }
}
