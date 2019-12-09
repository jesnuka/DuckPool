using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;
using System;

public class DuckPool : Area
{
    public DuckAgent duckAgent;
    public GameObject duckBaby;
    public Bread breadPrefab;
    public TextMeshPro rewardText;

    [HideInInspector]
    public float breadSpeed = 0f;
    [HideInInspector]
    public float feedRadius = 1f;

    private List<GameObject> breadList;

    public override void ResetArea()
    {
        RemoveAllBread();
        PlaceDuck();
        PlaceBaby();
        SpawnBread(4, breadSpeed);
    }

    public void RemoveBread(GameObject bread)
    {
        breadList.Remove(bread);
        Destroy(bread);

    }
    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;

        if (maxRadius > minRadius)
        {
            radius = UnityEngine.Random.Range(minRadius, maxRadius);
        }

        return center + Quaternion.Euler(0f, UnityEngine.Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
    }

    private void RemoveAllBread()
    {
        if (breadList != null)
        {
            for ( int i = 0; i < breadList.Count; i++)
            {
                if (breadList[i] != null)
                {
                    Destroy(breadList[i]);
                }
            }
        }

        breadList = new List<GameObject>();
    }

    private void PlaceDuck()
    {
        duckAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 7f) + Vector3.up * .5f;
        duckAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }

    private void PlaceBaby()
    {
        duckBaby.transform.position = duckAgent.transform.position;
        duckBaby.transform.rotation = duckAgent.transform.rotation;
    }

    private void SpawnBread(int count, float breadSpeed)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject breadObject = Instantiate<GameObject>(breadPrefab.gameObject);
            breadObject.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
            breadObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            breadObject.transform.parent = transform;
            breadList.Add(breadObject);
            breadObject.GetComponent<Bread>().breadSpeed = breadSpeed;
        }
    }
    private void Update()
    {
        rewardText.text = duckAgent.GetReward().ToString();
    }
}
