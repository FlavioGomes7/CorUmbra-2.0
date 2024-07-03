using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buoncyObject : MonoBehaviour
{
    [SerializeField]
    float buoyancy_force;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y_pos = transform.position.y;
        if (y_pos < 0.099f)
        {
            rig.AddForce(transform.up * buoyancy_force);
        }
    }
}