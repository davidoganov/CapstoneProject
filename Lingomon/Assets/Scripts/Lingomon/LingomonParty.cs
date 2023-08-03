using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LingomonParty : MonoBehaviour
{
    [SerializeField] List<Lingomon> lingomons;

    private void Start()
    {
        foreach (var lingomon in lingomons)
        {
            lingomon.Init();
        }
    }

    public Lingomon GetHealthyLingomon()
    {
        return lingomons.Where(x => x.HP > 0).FirstOrDefault();
    }

    public Lingomon GetLingomon(int index)
    {
        return index >= lingomons.Count ? null : lingomons[index];
    }
}
