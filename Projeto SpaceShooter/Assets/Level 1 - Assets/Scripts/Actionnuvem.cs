using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actionnuvem : MonoBehaviour {

	public Rigidbody2D corpo;
	public GameObject nuvem;
	public float velocidade;
	public float halfSpawnerWidth;
	public Vector2 screenSize;
	// Use this for initialization
	void Start () {
		halfSpawnerWidth = transform.localScale.x / 2;
		screenSize = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize + halfSpawnerWidth, Camera.main.orthographicSize);
	}

	// Update is called once per frame
	void Update () {

		moveBackForth ();
		corpo.AddForce (new Vector2(0, -velocidade));
		destroyObject ();

	}

	void moveBackForth(){

		if(transform.position.x < -screenSize.x){
			transform.position = new Vector2 (-screenSize.x, transform.position.y);
			//moveLeft = false;
			//moveRight = true;
		}else if(transform.position.x > screenSize.x){

			transform.position = new Vector2 (screenSize.x, transform.position.y);
			//moveLeft = true;
			//moveRight = false;
		}
	}

	void destroyObject(){

		if(transform.position.y+2 < -screenSize.y){
			Destroy(nuvem);	
		}
	}
}
