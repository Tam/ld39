using System;
using System.Collections;
using UnityEngine;

public class Battery : MonoBehaviour
{
	[SerializeField]
	[Range(0f, 1f)]
	private float _batteryLevel = 1f;
	
	private Coroutine _fillCoroutine;
	private Coroutine _lightsCoroutine;

	public float BatteryLevel {
		get { return _batteryLevel; }
		set
		{
			float diff = Math.Abs(_batteryLevel - value);
			_changeSpeed = 1f + (1f - diff) * 0.25f;
			
			_batteryLevel = value;
			if (_fillCoroutine != null) StopCoroutine(_fillCoroutine);
			_fillCoroutine = StartCoroutine(UpdateFill(value));
		} 
	}

	private float _intensityMultiplier = 1f;

	private float _changeSpeed = 1f;
	private GameObject _fill;
	private Light[] _lights;

	private void Awake ()
	{
		_fill = transform.Find("FillOffset").gameObject;
		_lights = transform.GetComponentsInChildren<Light>();
	}

	private void Start()
	{
		float z = _batteryLevel == 0f ? 0f : 1f;
		
		_fill.transform.localScale = new Vector3(1, _batteryLevel, z);
	}

	public void Highlight(float amount = 5f)
	{
		_intensityMultiplier = amount;
		if (_lightsCoroutine != null) StopCoroutine(_lightsCoroutine);
		_lightsCoroutine = StartCoroutine(UpdateLights());
	}

	public void StopHighlight()
	{
		_intensityMultiplier = 1f;
		if (_lightsCoroutine != null) StopCoroutine(_lightsCoroutine);
		_lightsCoroutine = StartCoroutine(UpdateLights());
	}

	private IEnumerator UpdateFill(float batteryLevel)
	{
		Vector3 startScale = _fill.transform.localScale;

		float currentTime = 0f;

		float z = 1f;
		
		do
		{
			if (currentTime > _changeSpeed && _batteryLevel == 0f)
				z = 0f;

			float step = Mathf.SmoothStep(0f, 1f, currentTime / _changeSpeed);
			
			Vector3 ls = Vector3.Lerp(
				startScale,
				new Vector3(1, batteryLevel, 1),
				step
			);

			ls.z = z;
			
			_fill.transform.localScale = ls;

			currentTime += Time.deltaTime;

			yield return null;
		} while (currentTime <= _changeSpeed);
	}

	private IEnumerator UpdateLights()
	{
		float[] startIntesities = new float[_lights.Length];

		int i = 0;
		foreach (Light l in _lights)
		{
			startIntesities[i] = l.intensity;
			i++;
		}

		float currentTime = 0f;

		do
		{
			float step = Mathf.SmoothStep(0f, 1f, currentTime / _changeSpeed);

			i = 0;
			foreach (Light l in _lights)
			{
				l.intensity = Mathf.Lerp(
					startIntesities[i],
					_batteryLevel * _intensityMultiplier,
					step
				);
				i++;
			}

			currentTime += Time.deltaTime;

			yield return null;
		} while (currentTime <= _changeSpeed);
	}
	
}
