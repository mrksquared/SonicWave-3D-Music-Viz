using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpToBeat : MonoBehaviour
{
    bool small = true;
    private Vector3 velocity = Vector3.zero;
    private float colVelocity = 0f;

    MeshRenderer rend;

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

    private void OnDisable()
    {
        switch (frequencyType)
        {
            case FrequencyType.BASS:
                analyzeBass.BassPeak -= setBig;

                break;
            case FrequencyType.MIDS:
                analyzeMids.MidsPeak -= setBig;
                break;
            case FrequencyType.TREBLE:
                analyzeHighs.HighPeak -= setBig;
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.shader = Shader.Find("Custom/Glow");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (small)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(.5f, .5f, .5f), ref velocity, 2f, 2f);
            for (int i = 0; i < rend.materials.Length; i++)
            {
                Vector4 curCol = rend.materials[i].GetVector("_Color");
                float curAlpha = curCol[3];
                rend.materials[i].SetVector("_Color", new Vector4(curCol[0], curCol[1], curCol[2],
                    Mathf.SmoothDamp(curAlpha, 0, ref colVelocity, .4f)));
            }
        }
    }

    public void setBig()
    {
        setOpaque();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void setOpaque()
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].SetVector("_Color", rend.materials[i].GetVector("_Color") + new Vector4(0,0,0,1f));
        }
    }
}
