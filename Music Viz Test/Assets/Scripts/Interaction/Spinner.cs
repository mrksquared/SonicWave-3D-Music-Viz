using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;

    public Vector3 averageCubeVelocity;

    public bool useColor = false;
    public Color cubeColor;

    public float squareWidth;

    public float timeBetweenBursts;
    public float numCubesInBurst;
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
            Vector3 flowerCenter = transform.position + new Vector3(Random.Range(-squareWidth, squareWidth), Random.Range(-squareWidth, squareWidth), Random.Range(-squareWidth, squareWidth));

            for (int i = 0; i < numCubesInBurst; i++)
            {
                Vector3 variance = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f));
                Vector3 rotationVariance = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f));

                GameObject newCube1 = Instantiate(cube1,
                    flowerCenter,
                    transform.rotation);
                newCube1.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                newCube1.GetComponent<Rigidbody>().angularVelocity = rotationVariance;
                Destroy(newCube1, 10);

                GameObject newCube2 = Instantiate(cube2,
                    flowerCenter,
                    transform.rotation);
                newCube2.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                newCube2.GetComponent<Rigidbody>().angularVelocity = rotationVariance;
                Destroy(newCube2, 10);

                GameObject newCube3 = Instantiate(cube3,
                    flowerCenter,
                    transform.rotation);
                newCube3.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                newCube3.GetComponent<Rigidbody>().angularVelocity = rotationVariance;
                Destroy(newCube3, 10);

                GameObject newCube4 = Instantiate(cube4,
                    flowerCenter,
                    transform.rotation);
                newCube4.GetComponent<Rigidbody>().velocity = Vector3.Scale(averageCubeVelocity, variance);
                newCube4.GetComponent<Rigidbody>().angularVelocity = rotationVariance;
                Destroy(newCube4, 10);

                if (useColor)
                {
                    Color newColor = cubeColor - new Color(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), 0);

                    newCube1.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                    newCube2.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                    newCube3.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                    newCube4.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
                }

            }

            yield return new WaitForSecondsRealtime(timeBetweenBursts);

        }
    }
}
