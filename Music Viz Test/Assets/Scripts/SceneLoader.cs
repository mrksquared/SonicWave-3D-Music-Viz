using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadRoom()
    {
        StartCoroutine(LoadYourAsyncScene("RoomCopy"));
    }

    public void LoadGetYourWish()
    {
        StartCoroutine(LoadYourAsyncScene("GetYourWish"));
    }

    public void LoadWallflower()
    {
        StartCoroutine(LoadYourAsyncScene("Wallflower"));
    }

    public void LoadAnotherLover()
    {
        StartCoroutine(LoadYourAsyncScene("AnotherLover"));
    }

    public void LoadMtjoy()
    {
        StartCoroutine(LoadYourAsyncScene("Mtjoy"));
    }

    public void LoadMinipops()
    {
        StartCoroutine(LoadYourAsyncScene("minipops"));
    }

   IEnumerator LoadYourAsyncScene(string name)
    {

        AsyncOperation asyncload = SceneManager.LoadSceneAsync(name);

        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
