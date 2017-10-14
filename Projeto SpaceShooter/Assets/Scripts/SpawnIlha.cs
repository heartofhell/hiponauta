using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIlha : MonoBehaviour {

	public GameObject ilha;
	public GameObject nuvem;
	public GameObject posicao;
	//tutorial
	public float halfSpawnerWidth;
	public Vector2 screenSize;
	//public bool moveRight;
	//public bool moveLeft;
	private int spawnRange;
	private float spanwTime;
	private Vector2 variacaoPosicao;


	// Use this for initialization
	void Start () {
		halfSpawnerWidth = transform.localScale.x / 2;
		screenSize = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize + halfSpawnerWidth, Camera.main.orthographicSize);
		InvokeRepeating ("spawnObject", 2f, 7f);

	}
	
	// Update is called once per frame
	void Update () {
		variacaoPosicao = new Vector2 (Random.Range (posicao.transform.position.x ,Camera.main.aspect * Camera.main.orthographicSize + halfSpawnerWidth), posicao.transform.position.y);
	}
		

	void spawnObject(){

		spawnRange = Random.Range (0, 10);

		if(spawnRange >= 4){

			GameObject tempIlha;
			tempIlha = Instantiate (ilha, variacaoPosicao, Quaternion.identity);
			//spanwTime = Random.Range (2f, 7f);	
		}else{

			GameObject tempNuvem;
			tempNuvem = Instantiate (nuvem, variacaoPosicao, Quaternion.identity);
		}
	}
}

