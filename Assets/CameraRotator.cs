using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	void Update () {
		transform.eulerAngles = new Vector3(
			Remap(Mathf.Sin(Time.time), -1f, 1f, 10f, 35f), 0f, 0f);
	}

	public static float Remap(float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
