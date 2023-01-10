using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Perception : MonoBehaviour
{
    [Range(1, 40)] public float distance = 1.0f;
    [Range(0, 180)] public float maxAngle = 45.0f;

    public string tagName = "";

    public GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            if (tagName == "" || collider.CompareTag(tagName))
            {

                result.Add(collider.gameObject);
            }

            // calculate angle from transform forward vector to direction of game object 
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float cos = Vector3.Dot(transform.forward, direction);
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

            if (angle <= maxAngle)
            {
                result.Add(collider.gameObject);
            }
        }
        return result.ToArray();
    }


}
