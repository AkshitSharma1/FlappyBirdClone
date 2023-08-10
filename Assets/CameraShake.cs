using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
	
	public Transform camTransform;

	
	[SerializeField] public float shakeDuration = 1f;

	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		SceneManager.sceneLoaded += SceneLoaded;
		if (camTransform == null)
		{
			camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
		}
	}

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
		camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

    public void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}