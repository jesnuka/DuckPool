using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class DuckAC : Academy
{

    private DuckPool[] duckPools;

    public override void AcademyReset()
    {
        if (duckPools == null)
        {
            duckPools = FindObjectsOfType<DuckPool>();
        }

        foreach (DuckPool duckPool in duckPools)
        {
            duckPool.breadSpeed = resetParameters["bread_speed"];
            //duckPool.feedRadius = resetParameters["feed_radius"];
            duckPool.ResetArea();
        }
    }

}
