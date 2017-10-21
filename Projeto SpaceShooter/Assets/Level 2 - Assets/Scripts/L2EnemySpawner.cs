using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2EnemySpawner : MonoBehaviour {

	public GameObject EnemyGO; //prefab do inimigo

	float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//função para spawnar inimigos
	void SpawnEnemy () {
		//este é o canto inferior esquerdo da tela
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//este é o canto superior direito da tela
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//instanciar um inimigo
		GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
		anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

		//cedula para prox spawn inimigo
		ScheduleNextEnemySpaw();

	}

	void ScheduleNextEnemySpaw () {
		float spawnInNSeconds;

		if (maxSpawnRateInSeconds > 1f) {
			//pegar um numero entre 1 e maxSpawnRateInSeconds
			spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
		} else
			spawnInNSeconds = 1f;

		Invoke("SpawnEnemy", spawnInNSeconds);
	}

	//função para aumentar a dificuldade do jogo
	void IncreaseSpawnRate () {
		if (maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;

		if (maxSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseSpawnRate");
	}

	//função para começar o spawn de inimigos
	public void ScheduleEnemySpawner () {
		
		//resetando o spawn rate
		maxSpawnRateInSeconds = 5f;

		Invoke("SpawnEnemy", maxSpawnRateInSeconds);

		//aumentar o spawnrate a cada 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 0f);
	}

	//função p/ parar o spawn de inimigos
	public void UnscheduleEnemySpawner () {
		CancelInvoke("SpawnEnemy");
		CancelInvoke("IncreaseSpawnRate");
	}
}
