using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlashing : MonoBehaviour {
	
		public Light lightSource;
		public Material material;
		public Color lightColor;
		public Color materialColor;
		public float minIntensity;
		public float maxIntensity;

		float flashingTimer;

		void Update()
		{
			flashingTimer += Time.deltaTime;

			if (flashingTimer >= 0.2f)
			{
				ChangeIntensityAndColor();
				flashingTimer = 0.1f;
			}
		}

		void ChangeIntensityAndColor()
		{
			float newIntensity = Random.Range(minIntensity, maxIntensity);
			lightSource.intensity = newIntensity;
			material.SetColor("_EmissionColor", materialColor * newIntensity);
			lightSource.color = lightColor;
		}
	}
