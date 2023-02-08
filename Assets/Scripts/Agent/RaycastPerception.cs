using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastPerception : Perception
{
    public Transform raycastTransform;
    [Range(2, 50)] public int numRaycast = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
        foreach (Vector3 direction in directions)
        {
            // cast ray from transform position towards direction 

            Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);
            Debug.DrawRay(ray.origin,ray.direction * distance);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, distance))
            {
                // don't perceive self 
                if (raycastHit.collider.gameObject == gameObject) continue;
                // check for tag match 

                if (tagName == "" || raycastHit.collider.CompareTag(tagName))
                {
                     Debug.DrawRay(ray.origin,ray.direction * distance, Color.red);
                    // add game object if ray hit and tag matches 
                    result.Add(raycastHit.collider.gameObject);
                }
            }

        }

        // sort results by distance 
        result.Sort(CompareDistance);

        return result.ToArray();
    }
}