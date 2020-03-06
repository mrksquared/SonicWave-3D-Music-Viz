using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analyzeBass : MonoBehaviour
{
    public List<SpectralFluxInfo> spectralFluxSamples = new List<SpectralFluxInfo>();
    public int thresholdWindowSize = 30;
    public int thresholdMultiplier = 1;

    int indexToProcess;

    public static int numSamples = 1024;
    float[] curSpectrum = new float[numSamples];
    float[] prevSpectrum = new float[numSamples];

    float[] curMidsSpectrum = new float[numSamples];
    float[] prevMidsSpectrum = new float[numSamples];

    public SpectrumGenerator spectrumGenerator;

    public delegate void OnBassPeak();
    public static event OnBassPeak BassPeak;


    public class SpectralFluxInfo
    {
        public float time;
        public float spectralFlux;
        public float threshold;
        public float prunedSpectralFlux;
        public bool isPeak;
    }

    private void OnEnable()
    {
        SpectrumGenerator.NewSpectrum += analyzeSpectrum;


    }

    private void OnDisable()
    {
        SpectrumGenerator.NewSpectrum -= analyzeSpectrum;
    }

    // Start is called before the first frame update
    void Start()
    {
        indexToProcess = thresholdWindowSize / 2;

        if (spectrumGenerator == null)
        {
            spectrumGenerator = GetComponent<SpectrumGenerator>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //analyzeSpectrum(spectrum, audioSource.time);




        //float targetFrequency = 234f;
        //float hertzPerBin = (float)AudioSettings.outputSampleRate / 2f / 1024;
        //int targetIndex = (int)(targetFrequency / hertzPerBin);

        //string outString = "";
        //for (int i = targetIndex - 3; i <= targetIndex + 3; i++)
        //{
        //    outString += string.Format("| Bin {0} : {1}Hz : {2} |   ", i, i * hertzPerBin, curSpectrum[i]);
        //}

        //Debug.Log(outString);

    }

    public void analyzeSpectrum()
    {

        float time = spectrumGenerator.getCurTimeStamp();


        float[] bass = new float[4];


        System.Array.Copy(spectrumGenerator.getCurSpectrum(), bass, 4);


        // Set spectrum
        setCurSpectrum(bass);

        // Get current spectral flux from spectrum
        SpectralFluxInfo curInfo = new SpectralFluxInfo();
        curInfo.time = time;
        curInfo.spectralFlux = calculateRectifiedSpectralFlux();
        spectralFluxSamples.Add(curInfo);

        // We have enough samples to detect a peak
        if (spectralFluxSamples.Count >= thresholdWindowSize)
        {


            // Get Flux threshold of time window surrounding index to process
            spectralFluxSamples[indexToProcess].threshold = getFluxThreshold(indexToProcess);

            // Only keep amp amount above threshold to allow peak filtering
            spectralFluxSamples[indexToProcess].prunedSpectralFlux = getPrunedSpectralFlux(indexToProcess);

            // Now that we are processed at n, n-1 has neighbors (n-2, n) to determine peak
            int indexToDetectPeak = indexToProcess - 1;

            bool curPeak = isPeak(indexToDetectPeak);

            if (curPeak)
            {
                spectralFluxSamples[indexToDetectPeak].isPeak = true;
            }
            if (spectralFluxSamples[indexToDetectPeak].isPeak)
            {
                BassPeak.Invoke();
            }
            Debug.Log(spectralFluxSamples[indexToDetectPeak].isPeak);
            indexToProcess++;

        }
        else
        {
            Debug.Log(string.Format("Not ready yet.  At spectral flux sample size of {0} growing to {1}", spectralFluxSamples.Count, thresholdWindowSize));
        }
    }


    float calculateRectifiedSpectralFlux()
    {
        float sum = 0f;

        // Aggregate positive changes in spectrum data
        for (int i = 0; i < numSamples; i++)
        {
            sum += Mathf.Max(0f, curSpectrum[i] - prevSpectrum[i]);
        }
        return sum;
    }

    public void setCurSpectrum(float[] spectrum)
    {
        curSpectrum.CopyTo(prevSpectrum, 0);
        spectrum.CopyTo(curSpectrum, 0);
    }

    public void setCurMidsSpectrum(float[] spectrum)
    {
        curMidsSpectrum.CopyTo(prevMidsSpectrum, 0);
        spectrum.CopyTo(curMidsSpectrum, 0);
    }

    float getFluxThreshold(int spectralFluxIndex)
    {
        // How many samples in the past and future we include in our average
        int windowStartIndex = Mathf.Max(0, spectralFluxIndex - thresholdWindowSize / 2);
        int windowEndIndex = Mathf.Min(spectralFluxSamples.Count - 1, spectralFluxIndex + thresholdWindowSize / 2);

        // Add up our spectral flux over the window
        float sum = 0f;
        for (int i = windowStartIndex; i < windowEndIndex; i++)
        {
            sum += spectralFluxSamples[i].spectralFlux;
        }

        // Return the average multiplied by our sensitivity multiplier
        float avg = sum / (windowEndIndex - windowStartIndex);
        return avg * thresholdMultiplier;
    }


    float getPrunedSpectralFlux(int spectralFluxIndex)
    {
        return Mathf.Max(0f, spectralFluxSamples[spectralFluxIndex].spectralFlux - spectralFluxSamples[spectralFluxIndex].threshold);
    }



    bool isPeak(int spectralFluxIndex)
    {
        if (spectralFluxSamples[spectralFluxIndex].prunedSpectralFlux > spectralFluxSamples[spectralFluxIndex + 1].prunedSpectralFlux &&
            spectralFluxSamples[spectralFluxIndex].prunedSpectralFlux > spectralFluxSamples[spectralFluxIndex - 1].prunedSpectralFlux)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
