using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIlha : MonoBehaviour {

	public GameObject ilha;
	public GameObject nuvem;
	public GameObject posicao;
	public GameObject passarinho;
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

	}
	
	// Update is called once per frame
	void Update () {
		variacaoPosicao = new Vector2 (Random.Range (posicao.transform.position.x ,Camera.main.aspect * Camera.main.orthographicSize + halfSpawnerWidth), posicao.transform.position.y);
	}
		
	public void chamaSpawn(){
		InvokeRepeating ("spawnObject", 2f, 7f);
		InvokeRepeating ("spawnPassarinhos", 2f, 5f);
	}

	public void spawnObject(){

		spawnRange = Random.Range (0, 10);

		if(spawnRange >= 5){

			Instantiate (ilha, variacaoPosicao, Quaternion.identity);
			//spanwTime = Random.Range (2f, 7f);	
		}else{

			Instantiate (nuvem, variacaoPosicao, Quaternion.identity);
		}
	}

	public void spawnPassarinhos(){

		spawnRange = Random.Range (0, 10);

		if(spawnRange >= 5){
			Instantiate (passarinho, variacaoPosicao, Quaternion.identity);
		}
	}

	public void ParaSpawn(){
		CancelInvoke ("spawnPassarinhos");
		CancelInvoke ("spawnObject");
	}
}

