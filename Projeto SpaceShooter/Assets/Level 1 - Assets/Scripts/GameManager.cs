using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	//referencias para os game objects
	public GameObject capa;				//referencia para a capa
	public GameObject playButton;		//referencia para o botão de play
	public GameObject interfaceJogo;	//referencia à interface do game
	public GameObject playerShip;		//referencia ão jogador
	public GameObject enemySpawner;		//referencia p/ o spawn de inimigos 
	public GameObject spawnElementos;	//referencia ao spawn de elementos 
	public GameObject GameOverGO;		//referencia para o game over
	public GameObject scoreUITextGO;	//referencia para o score
	public GameObject scoreUI;			//referencia para a UI do score
	public GameObject livesUITextGO;	//referencia para o score
	public GameObject livesUI;          //referencia para a UI do score
	
	private int ScoreToNextLevel = 2000;//Var de condição para o ChangeLevel 

	public enum GameManagerState {
		Opening,
		Gameplay,
		GamerOver,
		ChangeLevel,
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;
		InvokeRepeating("SubScoreInGame", 5f, 5f);
	}

	void Update () {
		//reduz a pontuação periodicamente

		//chamada do ChangeLevel caso atinja "ScoreToNextLevel" pontos
		if (scoreUITextGO.GetComponent<GameScore>().Score >= ScoreToNextLevel) {
			SetGameManagerState(GameManagerState.ChangeLevel);
		}

		if (scoreUITextGO.GetComponent<GameScore>().Score <= 0) {
			scoreUITextGO.GetComponent<GameScore>().Score = 0;
		}
	}

	// função para atualizar o GMState
	void UpdateGameManagerState () {
		switch (GMState) {
			case GameManagerState.Opening:

				//Esconde a interface
				interfaceJogo.SetActive(false);

				//esconder Game Over
				GameOverGO.SetActive(false);

				//deixar o botão play ativo
				playButton.SetActive(true);

				//ativa a capa do jogo
				capa.SetActive(true);

				break;

			case GameManagerState.Gameplay:

				//mostra botão Score
				interfaceJogo.SetActive(true);

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

				//inicia o spawn de objetos
				spawnElementos.GetComponent<SpawnIlha>().chamaSpawn();

				break;

			case GameManagerState.GamerOver:

				//parar o spaw de inimigos
				enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

				//mostrar o game over
				GameOverGO.SetActive(true);

				//desabilita a nave do player
				playerShip.SetActive(false);

				//Cancela o spawn de objetos
				spawnElementos.GetComponent<SpawnIlha>().CancelInvoke();

				//mudar o GMState para Opening depois de 5 segundos
				Invoke("ChangeToOpeningState", 10f);

				break;
			case GameManagerState.ChangeLevel:

				//parar o spaw de inimigos
				enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

				//desabilita a nave do player
				playerShip.SetActive(false);

				//Cancela o spawn de objetos
				spawnElementos.GetComponent<SpawnIlha>().CancelInvoke();

				SceneManager.LoadScene("Level 2");

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
		SetGameManagerState(GameManagerState.Gameplay);
	}

	//função p/ mudar o GMState p/ Opening State
	public void ChangeToOpeningState () {
		SetGameManagerState(GameManagerState.Opening);
	}

	public void SubScoreInGame () {
		scoreUITextGO.GetComponent<GameScore>().Score -= 50;
	}
}
