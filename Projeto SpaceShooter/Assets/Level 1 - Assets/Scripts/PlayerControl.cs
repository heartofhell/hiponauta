using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public GameObject GameManagerGO; //referencia p/ o game manager 

	public GameObject PlayerBulletGO; // prefab do tiro do jogador
	public GameObject BulletPosition01;
	public GameObject BulletPosition02;
	public GameObject ExplosionGO;// prefab da explosão
	public float fireRate; //Taxa de tiros
	private float nextFire; //intervalo entre os tiros

	//referencia para o texte da vida do player
	public Text LivesUIText;

	const int maxLives = 3;
	int lives; //max de vidas

	public float speed;

	public void Init () {

		//voltar a nave ao centro quando play for apertado
		transform.position = new Vector2(0, -2.5F);

		lives = maxLives;

		//atualiza o numero de vidas
		LivesUIText.text = lives.ToString();

		//definir este playerGO ativo
		gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Atirar enquanto estiver sendo precionado o espaço (Time.time é contagem de "tempo em tempo")
		if (Input.GetButton("Space") && Time.time > nextFire) {
			//proximo time será feito no tempo atual+taxa de tiro, ou seja, daqui a "Taxa de tiro" segundos depois
			nextFire = Time.time + fireRate;

			//toca o som do tiro
			gameObject.GetComponent<AudioSource>().Play();

			//instanciar o primeiro tiro
			GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
			bullet01.transform.position = BulletPosition01.transform.position; //define a posição inicial do tiro

			//instanciar o segundo tiro
			GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
			bullet02.transform.position = BulletPosition02.transform.position; // define a posição inicial do tiro

		}

		float x = Input.GetAxisRaw("Horizontal"); //o valor será -1, 0 ou 1 (para esquerda, parado e direita)
		float y = Input.GetAxisRaw("Vertical"); // o valor será -1, 0 e 1 (para baixo, parado e para cima)

		Vector2 direction = new Vector2(x, y).normalized;

		Move(direction);
	}

	void Move (Vector2 direction) {
		//achar os limites da tela para o movimento do jogador
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //canto inferior esquerdo da tela
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //canto superior direito da tela

		max.x = max.x - 0.225f; //subtrai metade da largura da srpite do jogador
		min.x = min.x + 0.225f; //add metade da largura da sprite do jogador

		max.y = max.y - 0.285f; //subtrai metade da altura da sprite do jogador
		min.y = min.y + 0.285f; //add metade da largura da sprite do jogador

		//pegar a posição atual do player
		Vector2 pos = transform.position;

		//Calcular a nova posição
		pos += direction * speed * Time.deltaTime;

		//Confirmar que a nova posição nao seja fora da tela
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		//Atualizar a posição do jogador
		transform.position = pos;
	}

	void OnTriggerEnter2D (Collider2D col) {
		//detecta colisão da nave do player com a nave inimiga ou seus tiros
		if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "elementosComplementar")) {
			PlayExplosion();

			//subtrai um da vida
			lives--;

			//atualiza o contador de vidas
			LivesUIText.text = lives.ToString();

			if (lives == 0) {
				//muda o GMState para gameover state
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GamerOver);
			}
		}
	}

	//função para iniciar as explosões
	void PlayExplosion () {
		GameObject explosion = (GameObject)Instantiate(ExplosionGO);

		//definir a posição da explosão
		explosion.transform.position = transform.position;

	}
}

