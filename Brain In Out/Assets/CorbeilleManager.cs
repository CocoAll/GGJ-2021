using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorbeilleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boullettePrefab;

    private List<GameObject> listBoullette = new List<GameObject>();
    [SerializeField]
    private Transform spawner;

    public void GenerateBoullette()
    {
        float xPos = Random.Range(spawner.position.x - 0.25f, spawner.position.x + 0.25f);
        GameObject go = Instantiate(boullettePrefab, new Vector3(xPos, spawner.position.y, spawner.position.z), spawner.rotation, spawner);
        listBoullette.Add(go);
    }

    public void DespawnBoullette()
    {
        if (listBoullette.Count == 0) return;
        GameObject go = listBoullette[Random.Range(0, listBoullette.Count)];
        listBoullette.Remove(go);
        Destroy(go);
    }
}
