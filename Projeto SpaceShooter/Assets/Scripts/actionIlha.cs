using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionIlha : MonoBehaviour {

	public Rigidbody2D corpo;
	public float velocidade;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		corpo.AddForce (new Vector2(0, -velocidade));
	}
}
