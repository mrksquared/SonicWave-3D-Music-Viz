using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToCreateEmitter : MonoBehaviour
{
    public Rigidbody emitter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody p = Instantiate(emitter, 
                transform.position + new Vector3(Random.Range(-5f,5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), 
                transform.rotation);

            p.angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
        }
    }
}
