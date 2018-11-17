using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cub : MonoBehaviour
{
	float speedo;
	float amount;
	float offset;
	float offset2;

	void Start()
	{
		//speedo = Random.Range(-20f, 20f);
		speedo = Random.Range(0f, 0.05f);
		amount = Random.Range(-720f, 720f);
		offset = Random.Range(0f, Mathf.PI * 2);
		offset2 = Random.Range(0f, 360f);
	}

	void Update()
	{
		//transform.Rotate(0, 0, speedo * Time.deltaTime);
		transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(speedo * (Time.time + offset)) * amount + offset2);
	}
}
