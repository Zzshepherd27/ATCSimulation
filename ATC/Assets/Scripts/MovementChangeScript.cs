using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChangeScript : MonoBehaviour
{

    void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Planes");
        foreach(GameObject plane in planes)
        {
            if(plane.GetComponent<AirplaneMovement>().active == true)
            {
                Vector2 target = new Vector2(worldPos.x, worldPos.y);
                plane.GetComponent<AirplaneMovement>().getRigidBody().velocity = new Vector2(-(plane.transform.position.x - target.x), -(plane.transform.position.y - target.y)).normalized;
                target.x = target.x - plane.transform.position.x;
                target.y = target.y - plane.transform.position.y;
                plane.GetComponent<AirplaneMovement>().active = false;
                float newAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                plane.transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle - 90));
            }
        }
    }
}
