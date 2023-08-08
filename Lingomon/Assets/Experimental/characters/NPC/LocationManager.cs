using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance { get; private set; }
    private void Awake() { Instance = this; }

    private Dictionary<string, Vector3> locals = new Dictionary<string, Vector3>();

    public void addLocation(string npc, Vector3 local) {
        if (locals.ContainsKey(npc)) locals[npc] = local;
        else locals.Add(npc, local);
    }

    public Vector3 getLocation(string npc) {
        return locals.ContainsKey(npc) ? locals[npc] : (new Vector3(0f, 0f, 0f));
    }
}
