using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2GameManager : MonoBehaviour {

	//referencias para os game objects
	public GameObject L2PlayerShip;     //referencia para a nave do player
	public GameObject L2EnemySpawner;   //referencia p/ o spawn de inimigos 
	public GameObject GameOverGO;       //referencia para o game over
	public GameObject scoreUITextGO;  //referencia para o score
	public GameObject scoreUI;          //referencia para a UI do score
	public GameObject livesUI;          //referencia para a UI de vidas

	public enum GameManagerState {
		Gameplay,
		GamerOver,
		ChangeLevel,
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		SetGameManagerState(GameManagerState.Gameplay);
	}

	void Update () {
		if (scoreUITextGO.GetComponent<L2GameScore>().Score >= 2000) {
			SetGameManagerState(GameManagerState.ChangeLevel);
		}
	}

	// função para atualizar o GMState
	void UpdateGameManagerState () {
		switch (GMState) {
			case GameManagerState.Gameplay:

				//mostra botão Score
				scoreUI.SetActive(true);
				livesUI.SetActive(true);

				//esconde o gameover
				GameOverGO.SetActive(false);

				//habilita a nave do player
				L2PlayerShip.GetComponent<L2PlayerControl>().Init();

				//começar o spawn de inimigos
				L2EnemySpawner.GetComponent<L2EnemySpawner>().ScheduleEnemySpawner();

				break;

			case GameManagerState.GamerOver:

				//parar o spaw de inimigos
				L2EnemySpawner.GetComponent<L2EnemySpawner>().UnscheduleEnemySpawner();

				//mostrar o game over
				GameOverGO.SetActive(true);

				L2PlayerShip.SetActive(false);

				//Volta para o Menu
				Invoke("BackToTheMenu", 10f);

				break;
			case GameManagerState.ChangeLevel:

				//parar o spaw de inimigos
				L2EnemySpawner.GetComponent<L2EnemySpawner>().UnscheduleEnemySpawner();

				//desabilita a nave do player
				L2PlayerShip.SetActive(false);

				SceneManager.LoadScene("Level 1");

				break;
		}
	}

	//função para definir o GMState
	public void SetGameManagerState (GameManagerState state) {
		GMState = state;
		UpdateGameManagerState();
	}

	public void BackToTheMenu () {
		SetGameManagerState(GameManagerState.Gameplay);
	}
}
