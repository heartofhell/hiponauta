using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	float speed; //velocidade do tiro
	Vector2 _direction; //direção do tiro
	bool isReady; //para saber quando a direção do tiro é difinida

	//definir valores padrão na função Awake
	void Awake () {
		speed = 5f;
		isReady = false;
	}

	// Use this for initialization
	void Start () {

	}

	//função para definir a direção do tiro
	public void SetDirection (Vector2 direction) {
		_direction = direction.normalized; //definir a direção normalizada, para pegar um "unit vector"

		isReady = true; //definir marcador para verdadeiro
	}

	// Update is called once per frame
	void Update () {
		if (isReady) {
			//pegar a posição atual do tiro
			Vector2 position = transform.position;

			//calcular a nova posição do tiro
			position += _direction * speed * Time.deltaTime;

			//atualizar a posição do tiro
			transform.position = position;

			//para remover o tiro do jogo
			//se o tiro sair da tela

			//este é o ponto inferior esquerdo da tela
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

			//este é o ponto superior direito da tela
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

			//se o tiro for para fora da tela, então ele é destruido
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			   (transform.position.y < min.y) || (transform.position.y > max.y)) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		//detecta a colisão do tiro do inimigo com a nave do player
		if (col.tag == "PlayerShipTag") {
			//destrói o tiro apos a colisão
			Destroy(gameObject);
		}
	}
}
