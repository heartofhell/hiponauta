using UnityEngine;
using System.Collections;

public class L2PlayerBullet : MonoBehaviour {
	float speed;
	public GameObject ExplosionGO;// prefab da explosão

	// Use this for initialization
	void Start () {
		speed = 8f;
	}

	// Update is called once per frame
	void Update () {
		//Pegar a posição atual do tiro
		Vector2 position = transform.position;

		//Calcular a nova posição do tiro
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//Atualizar a posição do tiro
		transform.position = position;

		//este é o ponto direito superior da tela
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//se o tiro for para fora da tela, ele será destruido
		if (transform.position.y > max.y) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		//detecta a colisão do tiro do player com a nave inimiga
		if (col.tag == "EnemyShipTag") {
			//destroy o tiro apos a colisão
			Destroy(gameObject);

			GameObject explosion = (GameObject)Instantiate(ExplosionGO);

			//definir a posição da explosão
			explosion.transform.position = transform.position;
		}
	}
}
