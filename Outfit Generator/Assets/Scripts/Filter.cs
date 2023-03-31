using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Filter
{
    public string predicate;
    public string parameter;

    public Filter(string predicate, string parameter)
    {
        this.predicate = predicate;
        this.parameter = parameter;
    }
}
