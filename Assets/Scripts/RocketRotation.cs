using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotation : MonoBehaviour {
    private Rigidbody2D rigi;
    // Use this for initialization
    void Start () {
     
        rigi = GetComponentInParent<Rigidbody2D>();
    }


   

    // Update is called once per frame
    void Update () {
        Vector2 v = rigi.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

       // transform.Rotate(Vector3.forward);
    }
}
