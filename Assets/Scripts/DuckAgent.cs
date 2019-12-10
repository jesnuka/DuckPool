using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensor;
using System;

public class DuckAgent : Agent
{
    public GameObject heartPrefab;
    //public GameObject 

    private bool isFull;
    private DuckPool duckPool;
    private Animator animator;
    //private RayPerceptionSensorComponent3D rayPerception;
    private RayPerception3D rayPerception;
    //private GameObject baby;
    

    public override void AgentAction(float[] vectorAction)
    {
        float forward = vectorAction[0];
        float leftOrRight = 0f;
        if (vectorAction[1] == 1f)
        {
            leftOrRight = -1f;
        }
        else if (vectorAction[1] == 2f)
        {
            leftOrRight = 1f;
        }
        animator.SetFloat("Vertical", forward);
        animator.SetFloat("Horizontal", leftOrRight);

        //negat. reward every step
        AddReward(-1f / agentParameters.maxStep);
        


    }

    public override void AgentReset()
    {
        //isFull = false;
        duckPool.ResetArea();

    }

    public override void CollectObservations()
    {
        // has the duck eaten
        //AddVectorObs(isFull)

        //direction duck is facing
        AddVectorObs(transform.forward);

        // rayperception (sight)
        // raydistance: how far to raycast
        // rayAngles: Angles to raycast (0 is right, 90 is forward, 180  is left)
        // detectableobjects: list of tags which correspond to object types agent can see
        // startoffset: starting height offset of ray from center of agent
        // endoffset: ending height offset of ray from center of agent
        float rayDistance = 20f;
        float[] rayAngles = { -30f, -60f, -90f, -120f, -150f };
        string[] detectableObjects = { "bread", "wall" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));




    }

    private void Start()
    {
        duckPool = GetComponentInParent<DuckPool>();

        animator = GetComponent<Animator>();
        rayPerception = GetComponent<RayPerception3D>();

    }

    private void FixedUpdate()
    {
        //if (Vector3.Distance(transform.position, ))
        //EatBread();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("bread"))
        {
            EatBread(collision.gameObject);
        }
    }

    private void EatBread(GameObject bread)
    {
        duckPool.RemoveBread(bread);
        AddReward(1f);

        // spawn heart
        GameObject heart = Instantiate<GameObject>(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.position = transform.position + Vector3.up*0.5f;
        heart.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f);
        Destroy(heart, 2f);

        

    }

    public override float[] Heuristic()
    {
        float[] playerInput = { 0f, 0f };

        if (Input.GetKey(KeyCode.W))
        {
            playerInput[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerInput[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerInput[1] = 2;
        }
        return playerInput;
    }
}
