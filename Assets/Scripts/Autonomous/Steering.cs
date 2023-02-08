using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Steering
{

    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));
        return force;
    }
    public static Vector3 Flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));
        return force;
    }
    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 desired = direction.normalized * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        // randomly adjust angle +/- displacement 
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.data.wanderDisplacement, agent.data.wanderDisplacement);

        // create rotation quaternion around y-axis (up) 
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);

        // calculate point on circle radius 
        Vector3 point = rotation * (Vector3.forward * agent.data.wanderRadius);

        // set point in front of agent at distance length 
        Vector3 forward = agent.transform.forward * agent.data.wanderDistance;

        Vector3 force = CalculateSteering(agent, forward + point);

        force.y = 0;
        return force;
    }
    public static Vector3 Seperation(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        // accumulate separation vector of neighbors 
        foreach (GameObject neighbor in neighbors)
        {
            // create separation direction (neighbor position <- agent position) 
            Vector3 direction = neighbor.transform.position - agent.transform.position;
            if (direction.magnitude < radius)
            {
                // scale direction by distance (closer = stronger) 
                separation += direction.normalized / direction.magnitude;
            }
        }

        // steer toward separation 
        Vector3 force = CalculateSteering(agent, separation);

        return force;
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors)
    {
        Vector3 averageVelocity = Vector3.zero;
        // accumulate velocity of neighbors (velocity = forward direction movement) 
        int count = 0;
        foreach (GameObject neighbor in neighbors)
        {
            // need to get the Agent component of the game object and then movement velocity 
            Agent neighborAgent = neighbor.GetComponent<Agent>();
            if (neighborAgent != null)
            {
                averageVelocity += neighborAgent.movement.velocity;
                count++;
            }
        }
        // calculate the average by dividing the average velocity by the number of 
        // neighbors
        if (count > 0)
            averageVelocity /= count;

        // steer towards the average velocity of the neighbors 
        Vector3 force = CalculateSteering(agent, averageVelocity);

        return force;
    }


    public static Vector3 Cohesion(Agent agent, GameObject[] neighbors)
    {
        Vector3 center = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Length;

        Vector3 force = CalculateSteering(agent, center - agent.transform.position);

        return force;
    }

}
