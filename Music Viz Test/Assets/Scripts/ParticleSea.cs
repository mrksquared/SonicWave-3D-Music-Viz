using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ParticleSea : MonoBehaviour {

	public PropagationType propagation;
	public Camera cam;
	public bool render;
	public ParticleSystem particleSystem;
	public Text particlesText;
	public Vector3 meshOffset;
	public int x_meshResolution;
	public int y_meshResolution;
	public float x_meshSpacing;
	public float y_meshSpacing;
	public float meshHeightScale;
	public float perlinNoiseScale;
	public float posRandomness;
	public Vector2 perlinNoiseOffset;
	public Gradient colorsGradient;
	public bool useGradientColors;
	public Color defaultColor;
	public float x_randomLimit;
	public float y_randomLimit;

    float originalHeight;
    float heightVelocity = 0f;

    float originalNoise;
    float noiseVeloicty = 0f;

	private Vector3[] positions;
	private bool colorsAssigned = false;
	private ParticleSystem.Particle[] particlesArray;
	private float xPos;
	private float yPos;
	private float zPos;
	public float xAnimSpeed;
	public float yAnimSpeed;
	private int frameCount;
	private bool hasAnythingChanged;

	public enum PropagationType {
		Regular,
		Random
	}

    private void OnEnable()
    {
        PrecannedExperience.Beat1 += PulseHeight;
        PrecannedExperience.Beat3 += PulseNoise;
        
    }

    private void OnDisable()
    {
        PrecannedExperience.Beat1 -= PulseHeight;
        PrecannedExperience.Beat3 -= PulseNoise;
    }


    public void PerlinNoiseScale(float noise) {
		if(noise > 0f)
			perlinNoiseScale = noise * 100;
	}

	public void HeightModifier(float height) {
		if(height > 0f)
			meshHeightScale = height * 5;
	}

	public void Spacing(float spacing) {
		hasAnythingChanged = true;
		if(spacing > 0f) {
			x_meshSpacing = spacing;
			y_meshSpacing = spacing;
        }
	}

	public void Render(bool isActive) {
		render = isActive;
	}

	public void UseGradient(bool isActive) {
		useGradientColors = isActive;
		colorsAssigned = false;
	}

	public void Bloom(bool isActive) {
		cam.GetComponent<BloomOptimized>().enabled = isActive;
	}

	public void Vignetting(bool isActive) {
		cam.GetComponent<VignetteAndChromaticAberration>().enabled = isActive;
	}

	public void XAnimSpeed(float speed) {
		xAnimSpeed = speed;
	}

	public void YAnimSpeed(float speed) {
		yAnimSpeed = speed;
	}

	IEnumerator Animate() {
		while(true) {
			perlinNoiseOffset = new Vector2(perlinNoiseOffset.x + xAnimSpeed, perlinNoiseOffset.y + yAnimSpeed);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public void Resolution(float i) {
		x_meshResolution = (int) i;
		y_meshResolution = (int) i;
		particlesArray = new ParticleSystem.Particle[x_meshResolution * y_meshResolution];
		positions = new Vector3[x_meshResolution * y_meshResolution];
		RegenerateRandomPositionsArray(x_meshResolution * y_meshResolution);
		particleSystem.maxParticles = x_meshResolution * y_meshResolution;
		particlesText.text = "Particles count: "+(y_meshResolution * y_meshResolution).ToString();
		hasAnythingChanged = true;
		
	}

	void Start() {
		particlesArray = new ParticleSystem.Particle[x_meshResolution * y_meshResolution];
		positions = new Vector3[x_meshResolution * y_meshResolution];
		RegenerateRandomPositionsArray(x_meshResolution * y_meshResolution);
		StartCoroutine("Animate");
		colorsAssigned = false;
		hasAnythingChanged = true;
		particlesText.text = "Particles count: 40000";
        originalHeight = meshHeightScale;
        originalNoise = perlinNoiseScale;
	}

	void Update () {
		if(render && frameCount % 2 == 0) {
				if(hasAnythingChanged) meshOffset = new Vector3(x_meshSpacing * x_meshResolution * -0.5f, y_meshSpacing * y_meshResolution * -0.5f, 0f);
				hasAnythingChanged = false;
				particleSystem.GetParticles(particlesArray);

				for(int i = 0; i < x_meshResolution; i++) {
					for(int j = 0; j < y_meshResolution; j++) {
                    float xPos2 = (perlinNoiseOffset.x + i) / perlinNoiseScale * .5f;
                    float yPos2 = (perlinNoiseOffset.y + j) / perlinNoiseScale * 1.2f;



                    xPos = (perlinNoiseOffset.x + i) / perlinNoiseScale;
						yPos = (perlinNoiseOffset.y + j) / perlinNoiseScale;
						zPos = Mathf.PerlinNoise(xPos, yPos) + Mathf.PerlinNoise(xPos2, yPos2) - .5f;

						//Set position based on these three
						particlesArray[i * x_meshResolution + j].position = new Vector3(i * x_meshSpacing + meshOffset.x, j * y_meshSpacing + meshOffset.y, zPos * meshHeightScale);
						
						if(useGradientColors) particlesArray[i * x_meshResolution + j].color = colorsGradient.Evaluate(zPos);
						else if(!colorsAssigned) particlesArray[i * x_meshResolution + j].color = defaultColor;
						colorsAssigned = true;
					}
			}
			particleSystem.SetParticles(particlesArray, particlesArray.Length);
		}

        

		frameCount++;
		colorsAssigned = true;


	}

    private void FixedUpdate()
    {
        meshHeightScale = Mathf.SmoothDamp(meshHeightScale, originalHeight, ref heightVelocity, 1f);
        perlinNoiseScale = Mathf.SmoothDamp(perlinNoiseScale, originalNoise, ref noiseVeloicty, 3f);
    }



    private void RegenerateRandomPositionsArray(int arraySize) {
		for(int i = 0; i < arraySize; i++) {
			positions[i] = new Vector3(Random.Range(-x_randomLimit, x_randomLimit), Random.Range(-y_randomLimit, y_randomLimit), 0);
		}
	}

    public void PulseHeight()
    {
        meshHeightScale = 1f;
    }

    public void PulseNoise()
    {
        perlinNoiseScale = 6f;
    }
}
