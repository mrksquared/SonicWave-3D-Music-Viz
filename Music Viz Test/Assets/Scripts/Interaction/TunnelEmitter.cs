using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEmitter : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    public float timeBetweenBursts;
    public float numCubesInBurst;

    public float squareWidth;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BurstCubes());
    }

    

    private IEnumerator BurstCubes() {
        while(true)
        {
            for (int i = 0; i < numCubesInBurst; i++)
            {
                GameObject newCube1 = Instantiate(cube1,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube1.GetComponent<Rigidbody>().velocity = new Vector3(0, Random.Range(-2f, -.5f), 0);
                Destroy(newCube1, 10);

                GameObject newCube2 = Instantiate(cube2,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube1.GetComponent<Rigidbody>().velocity = new Vector3(0, Random.Range(-2f, -.5f), 0);
                Destroy(newCube2, 10);

                GameObject newCube3 = Instantiate(cube3,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube3.GetComponent<Rigidbody>().velocity = new Vector3(0, Random.Range(-2f, -.5f), 0);
                Destroy(newCube3, 10);
            }

            yield return new WaitForSecondsRealtime(timeBetweenBursts);

        }
    }
}
