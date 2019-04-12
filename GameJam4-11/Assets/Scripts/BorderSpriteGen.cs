using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSpriteGen : MonoBehaviour {

    public List<GameObject> borderSpritePrefabs = new List<GameObject>();

	// Use this for initialization
	void Start () {
        GenerateSprites();
	}

    private void GenerateSprites ()
    {
        int sprNum = Random.Range(0, borderSpritePrefabs.Count);
        Instantiate(borderSpritePrefabs[sprNum], new Vector3(transform.position.x, transform.position.y, borderSpritePrefabs[sprNum].transform.position.z), transform.rotation, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
