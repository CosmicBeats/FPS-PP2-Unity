using UnityEngine;

public class FlashingLightNoLight : MonoBehaviour
{
	public Material lightMaterial;

	public float onDuration = 1.0f;
	public float offDuration = 1.0f;
	public bool startOn = true;

	public float minEmission = 0.0f;
	public float maxEmission = 1.0f;

	private float timer;
	private bool isOn;

	void Start()
	{
		isOn = startOn;

		lightMaterial.SetColor("_EmissionColor", Color.Lerp(new Color(minEmission, minEmission, minEmission), new Color(maxEmission, maxEmission, maxEmission), isOn ? 1.0f : 0.0f));
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (isOn && timer >= onDuration)
		{
			isOn = false;
			lightMaterial.SetColor("_EmissionColor", Color.Lerp(new Color(minEmission, minEmission, minEmission), new Color(maxEmission, maxEmission, maxEmission), isOn ? 1.0f : 0.0f));
			timer = 0;
		}
		else if (!isOn && timer >= offDuration)
		{
			isOn = true;
			lightMaterial.SetColor("_EmissionColor", Color.Lerp(new Color(minEmission, minEmission, minEmission), new Color(maxEmission, maxEmission, maxEmission), isOn ? 1.0f : 0.0f));
			timer = 0;
		}
	}
}

