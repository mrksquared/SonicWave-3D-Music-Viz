using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumGenerator : MonoBehaviour
{
    public int numSamples = 1024;

    public AudioSource audioSource;
    public float[] curSpectrum;
    public float timeStamp;

    public delegate void OnNewSpectrum();
    public static event OnNewSpectrum NewSpectrum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float[] spectrum = new float[numSamples];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        curSpectrum = spectrum;
        timeStamp = audioSource.time;
        NewSpectrum.Invoke();
    }

    public float[] getCurSpectrum()
    {
        return curSpectrum;
    }

    public float getCurTimeStamp()
    {
        return timeStamp;
    }
}
