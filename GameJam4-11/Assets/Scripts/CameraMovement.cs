using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [Header ("Transforms")]
    public List<Transform> CameraHooks = new List<Transform>();
    [Header("Variables")]
    public float zoomMultiplier;
    public float zoomMin;
    [Header("Speed")]
    public float camMovSpeed;

    private Vector3 tarPos;
    private float tarZoom;

	// Use this for initialization
	void Start () {
        Invoke("GetPlayerObjects", 0.01f);
	}

    private void GetPlayerObjects ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            CameraHooks.Add(players[i].transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
        CamMov();
        CamZoom();

        transform.position = Vector3.Lerp(transform.position, new Vector3(tarPos.x, tarPos.y, -tarZoom * zoomMultiplier), camMovSpeed * Time.deltaTime);
    }

    private void CamMov ()
    {
        tarPos = Vector3.zero;

        for (int i = 0; i < CameraHooks.Count; i++)
        {
            tarPos += CameraHooks[i].position;
        }
        tarPos /= CameraHooks.Count;
    }

    private void CamZoom ()
    {
        tarZoom = 0;
        for (int i = 0; i < CameraHooks.Count; i++)
        {
            for (int j = 0; j < CameraHooks.Count; j++)
            {
                if (j != i)
                {
                    float tempZoom = Vector3.Distance(CameraHooks[j].position, CameraHooks[i].position);
                    if (tempZoom > tarZoom)
                        tarZoom = tempZoom;
                }
            }
        }
        if (tarZoom < zoomMin)
            tarZoom = zoomMin;
    }
}
