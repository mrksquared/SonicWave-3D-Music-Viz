using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitCubes : MonoBehaviour
{
    public Rigidbody basscube;
    public Rigidbody midcube;
    public Rigidbody treblecube;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BurstCubes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BurstCubes()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            
            Rigidbody a = Instantiate(basscube, transform.position, transform.rotation);
            a.velocity = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));

            Rigidbody b = Instantiate(midcube, transform.position, transform.rotation);
            b.velocity = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));

            Rigidbody c = Instantiate(treblecube, transform.position, transform.rotation);
            c.velocity = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
        }

    }
}
