using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {

	void LateUpdate () {
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel("Menu");
			}
	}
}
