using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour {

    [Header("Variables")]
    public float rotMax;
    public float scaleMax;
    [Header("Speed")]
    public float rotSpeed;
    public float scaleSpeed;

    private bool rotRight;
    private bool scaleUp;
    private float rotZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rotRight == true)
            rotZ += rotSpeed * Time.deltaTime;
        else
            rotZ -= rotSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0,rotZ);

        if (scaleUp == true)
            transform.localScale += new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, 0);
        else
            transform.localScale -= new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, 0);

        if (rotZ > rotMax)
            rotRight = false;

        if (rotZ < -rotMax)
            rotRight = true;

        if (transform.localScale.x > scaleMax)
            scaleUp = false;
        if (transform.localScale.x < 1)
            scaleUp = true;
    }
}
