using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEmitter : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    public Vector3 averageCubeVelocity;

    public float timeBetweenBursts;
    public float numCubesInBurst;

    public float squareWidth;

    public bool useColor = false;
    public Color cubeColor; 

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
                Vector3 variance = new Vector3(Random.Range(.8f, 1.2f), Random.Range(.8f, 1.2f), Random.Range(.8f, 1.2f));

                GameObject newCube1 = Instantiate(cube1,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube1.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                Destroy(newCube1, 10);

                GameObject newCube2 = Instantiate(cube2,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube2.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                Destroy(newCube2, 10);

                GameObject newCube3 = Instantiate(cube3,
                    transform.position +
                    new Vector3(Random.Range(-squareWidth, squareWidth), 0,
                    Random.Range(-squareWidth, squareWidth)),
                    transform.rotation);
                newCube3.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                Destroy(newCube3, 10);

                if (useColor)
                {
                    Color newColor = cubeColor - new Color(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), 0);

                    newCube1.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                    newCube2.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                    newCube3.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                }

            }

            yield return new WaitForSecondsRealtime(timeBetweenBursts);

        }
    }
}
