using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//referencias para os game objects
	public GameObject Boss;
	public GameObject capa;
	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner;//referencia p/ o spawn de inimigos 
	public GameObject GameOverGO;//referencia para o game over
	public GameObject scoreUITextGO; //referencia para o score
	public GameObject scoreUI; //referencia para a UI do score
	public GameObject livesUITextGO; //referencia para o score
	public GameObject livesUI; //referencia para a UI do score
	public int SubScore; //Var para subtrair pontos do Score
	public int ScoreToBoss; //Var de condição para o Boss Nascer
	bool block = false; //Var para spawnar apenas 1 Boss

	public enum GameManagerState {
		Opening,
		Gameplay,
		GamerOver,
		Boss,
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;
		//esconde botão Score
		scoreUITextGO.SetActive(false);
		scoreUI.SetActive(false);
		livesUITextGO.SetActive(false);
		livesUI.SetActive(false);

	}

	void Update () {
		//reduz a pontuação periodicamente
		scoreUITextGO.GetComponent<GameScore>().Score -= SubScore;

		//spawn do Boss caso atinja X pontos
		if (scoreUITextGO.GetComponent<GameScore>().Score >= ScoreToBoss) {
			GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Boss);
		}
	}

	// função para atualizar o GMState
	void UpdateGameManagerState () {
		switch (GMState) {
			case GameManagerState.Opening:

				//esconder Game Over
				GameOverGO.SetActive(false);

				//deixar o botão play ativo
				playButton.SetActive(true);
				
				//ativa a capa do jogo
				capa.SetActive(true);
				break;

			case GameManagerState.Gameplay:

				//mostra botão Score
				scoreUITextGO.SetActive(true);
				scoreUI.SetActive(true);
				livesUITextGO.SetActive(true);
				livesUI.SetActive(true);

				//resetar o score
				scoreUITextGO.GetComponent<GameScore>().Score = 0;

				//esconde o botão play
				playButton.SetActive(false);
				
				//desativa a capa do jogo
				capa.SetActive(false);

				//habilita a nave do player
				playerShip.GetComponent<PlayerControl>().Init();

				//começar o spawn de inimigos
				enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

				break;

			case GameManagerState.GamerOver:

				//parar o spaw de inimigos
				enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

				//mostrar o game over
				GameOverGO.SetActive(true);

				block = false;

				//mudar o GMState para Opening depois de 8 segundos
				Invoke("ChangeToOpeningState", 5f);

				break;
			case GameManagerState.Boss:

				//parar o spaw de inimigos
				enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
				if (block == false) {
					GameObject anBoss = (GameObject)Instantiate(Boss);
					anBoss.transform.position = new Vector2(0, 5);
					block = true;
				}

				break;
		}
	}

	//função para definir o GMState
	public void SetGameManagerState (GameManagerState state) {
		GMState = state;
		UpdateGameManagerState();
	}

	//o botão play chamará esta função quando pressionado
	public void StartGamePlay () {
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState();

	}

	//função p/ mudar o GMState p/ Opening State
	public void ChangeToOpeningState () {
		SetGameManagerState(GameManagerState.Opening);
	}
}
