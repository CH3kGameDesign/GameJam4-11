using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 rotSpeed;

	// Use this for initialization
	void Start () {
        if (rotSpeed == Vector3.zero)
            rotSpeed = new Vector3(0, 0, Random.Range(-60, 60));
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles += rotSpeed * Time.deltaTime;
	}
}
