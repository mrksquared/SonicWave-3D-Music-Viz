using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastInteraction : MonoBehaviour
{
    public float timer;
    public bool selecting;



    public float sightlength = 10;

    private void OnEnable()
    {
        
        SceneManager.sceneLoaded += ResetVariables;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetVariables;
    }


    private void Awake()
    {
        selecting = false;
        timer = 0;
    }

    private void ResetVariables(Scene scene, LoadSceneMode mode)
    {
        selecting = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selecting)
        {
            timer += Time.deltaTime;
        }

        RaycastHit seen;
        Ray raydirection = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(raydirection, out seen, sightlength))
        {
            if (seen.collider.tag =="Navigation")
            {
                selecting = true;
                Debug.Log("Button hit");
                seen.collider.gameObject.GetComponent<Button>().Select();
                if (timer >= 2)
                {
                    
                    seen.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            } else
            {
                selecting = false;
                timer = 0;
            }
        }

        Debug.DrawRay(transform.position, transform.forward, Color.red, 1);
    }
}
