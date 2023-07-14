using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<Lingomon> wildLingomons;

    public Lingomon GetRandomWildLingomon()
    {
        var wildLingomon =  wildLingomons[Random.Range(0, wildLingomons.Count)];
        wildLingomon.Init();
        return wildLingomon;
    }
}
