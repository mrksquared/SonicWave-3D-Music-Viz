using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitCubes : MonoBehaviour
{
    public Rigidbody basscube;
    public Rigidbody midcube;
    public Rigidbody treblecube;

    //Time before cubes despawn; 0 = infinite
    public float lifetime = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BurstCubes());
        if (lifetime < 0)
        {
            lifetime = 0;
        }
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
            a.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Destroy(a.gameObject, lifetime);
            

            Rigidbody b = Instantiate(midcube, transform.position, transform.rotation);
            b.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Destroy(b.gameObject, lifetime);

            Rigidbody c = Instantiate(treblecube, transform.position, transform.rotation);
            c.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Destroy(c.gameObject, lifetime);
        }

    }
}
