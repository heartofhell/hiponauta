using UnityEngine;
using System.Collections;

public class L2EnemyControl : MonoBehaviour {
	private GameObject scoreUITextGO; //referencia ao score
	public GameObject ExplosionGO;// prefab da explosão

	private float speed; //velocidade do inimigo

	// Use this for initialization
	void Start () {
		speed = 2f; //difinir velocidade

		//define o texto do score
		scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}

	// Update is called once per frame
	void Update () {
		//pegar posição atual do inimigo
		Vector2 position = transform.position;

		//calcular a nova posição do inimigo
		position = new Vector2(position.x, position.y - speed * Time.deltaTime);

		//atualizar a posição do inimigo
		transform.position = position;

		//este é o ponto inferior esquerdo da tela
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//se o inimigo sair da tela por baixo, entao é destruido
		if (transform.position.y < min.y) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		//detecta a colisão da nave inimiga com a nave do player ou seus tiros
		if ((col.tag == "PlayerShipTag") || col.tag == "PlayerBulletTag") {
			PlayExplosion();

			//add X pontos para o score
			scoreUITextGO.GetComponent<L2GameScore>().Score += 100;

			//destrói a nave inimiga
			Destroy(gameObject);
		}
	}

	//função para iniciar a explosão
	void PlayExplosion () {
		GameObject explosion = Instantiate(ExplosionGO);

		//definir a posição da explosão
		explosion.transform.position = transform.position;
	}
}
