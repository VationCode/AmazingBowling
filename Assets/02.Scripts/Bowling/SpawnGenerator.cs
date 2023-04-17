using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100;

    private List<GameObject> props = new List<GameObject>();

    void Start()
    {
        area = GetComponent<BoxCollider>();
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
        area.enabled = false;
    }

    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];

        Vector3 spawnPos = GetRandomPos();
        GameObject instanceObj = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        props.Add(instanceObj);
    }

    private Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x/2f, size.x/2f);
        float posY = basePos.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    public void Reset() //라운드 넘어갔을 때
    {
        for (int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPos();
            props[i].SetActive(true);
        }
    }
}
