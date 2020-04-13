using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecannedExperience : MonoBehaviour
{
    public AudioSource music;

    public int bpm;
    public float waitToStartTime = 0f;

    int beat = 0;
    float secondsBetweenBeat;

    public delegate void OnBeat1();
    public static event OnBeat1 Beat1;

    public delegate void OnBeat2();
    public static event OnBeat2 Beat2;

    public delegate void OnBeat3();
    public static event OnBeat3 Beat3;

    public delegate void OnBeat4();
    public static event OnBeat4 Beat4;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        secondsBetweenBeat = 1f / (bpm / 60f);

        StartCoroutine(PulseOnBeat());
        music.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PulseOnBeat()
    {
        yield return new WaitForSecondsRealtime(waitToStartTime);
        while (true)
        {
            beat++;
            if (beat == 1) { Beat1.Invoke(); }
            else if (beat == 2) { Beat2.Invoke(); }
            else if (beat == 3) { Beat3.Invoke(); }
            else {
                Beat4.Invoke();
                beat = 0;
            }

            yield return new WaitForSecondsRealtime(secondsBetweenBeat);
        }


    }
}
