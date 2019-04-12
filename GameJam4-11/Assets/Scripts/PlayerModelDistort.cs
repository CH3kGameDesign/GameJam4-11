using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelDistort : MonoBehaviour {

    public float distortMultiplier;
    private float pastY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float yScale = Mathf.Abs(pastY - transform.position.y) * distortMultiplier;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1 - (yScale/2), yScale + 1, 1 - (yScale / 2)), 0.5f);
        pastY = transform.position.y;
	}
}
