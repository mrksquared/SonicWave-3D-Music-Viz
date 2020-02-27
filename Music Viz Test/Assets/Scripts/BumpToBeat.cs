using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpToBeat : MonoBehaviour
{
    bool small = true;
    private Vector3 velocity = Vector3.zero;

    public FrequencyType frequencyType;

    public enum FrequencyType
    {
        BASS, MIDS, TREBLE
    };



    private void OnEnable()
    {
        switch (frequencyType) {
            case FrequencyType.BASS:
                analyzeBass.BassPeak += setBig;

                break;
            case FrequencyType.MIDS:
                analyzeMids.MidsPeak += setBig;
                break;
            case FrequencyType.TREBLE:
                analyzeHighs.HighPeak += setBig;
                break;

         }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (small)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(.5f, .5f, .5f), ref velocity, 2f, 2f);
        }
    }

    public void setBig()
    {
       
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void setSmall()
    {
        
    }
}
