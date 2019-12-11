using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class Bread : MonoBehaviour
{
    public float breadSpeed;

    private float randomizedSpeed = 0f;
    private float nextActionTime = -1f;
    private Vector3 targetPosition;

    private void FixedUpdate()
    {
        if (breadSpeed > 0f)
        {
            Float();
        }

    }

    private void Float()
    {
        if (Time.fixedTime >= nextActionTime)
        {
            // randomize speed
            randomizedSpeed = breadSpeed * UnityEngine.Random.Range(.5f, 1.5f);

            targetPosition = DuckPool.ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f);
            targetPosition = new Vector3(targetPosition.x, 0f ,targetPosition.z);

            //transform.rotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);

            float timeToGetThere = Vector3.Distance(transform.position, targetPosition) / randomizedSpeed;

            nextActionTime = Time.fixedTime + +timeToGetThere;


        }
        else
        {
            Vector3 moveVector = randomizedSpeed * transform.forward * Time.fixedDeltaTime;
            if (moveVector.magnitude <= Vector3.Distance(transform.position, targetPosition))
            {
                transform.position = new Vector3(transform.position.x + moveVector.x, transform.position.y, transform.position.z + moveVector.z);
            }
            else
            {
                transform.position = targetPosition;
                nextActionTime = Time.fixedTime;
            }
        }
    }
}
