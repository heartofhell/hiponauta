using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIlha : MonoBehaviour {

	public GameObject ilha;
	public GameObject posicao;
	public float delay;
	private bool spawn;

	// Use this for initialization
	void Start () {
		delay = 1;
		spawn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(spawn == true){
			Instantiate (ilha, posicao.transform.position, Quaternion.identity);
			spawn = false;
		}
	}
}
