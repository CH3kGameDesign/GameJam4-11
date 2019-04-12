using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DepthOfFieldChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DepthOfField DOF = null;
        GetComponent<PostProcessVolume>().profile.TryGetSettings<DepthOfField>(out DOF);

        DOF.focusDistance.value = Mathf.Abs(Camera.main.transform.parent.position.z);
	}
}
