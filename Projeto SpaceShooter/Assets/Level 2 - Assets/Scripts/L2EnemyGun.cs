using UnityEngine;
using System.Collections;

public class L2EnemyGun : MonoBehaviour {
	public GameObject EnemyBulletGO; //este é o prefab do tiro inimigo

	// Use this for initialization
	void Start () {

		//disparar tiro inimigo a cada 1s
		InvokeRepeating("FireEnemyBullet", 1f, .5f);
	}

	// Update is called once per frame
	void Update () {

	}

	//função para o inimigo atirar
	void FireEnemyBullet () {
		//pegar uma referencia da nave do jogador
		GameObject playerShip = GameObject.FindGameObjectWithTag("PlayerShipTag");

		//se o player não estiver morto
		if (playerShip != null) {
			//instanciar um tiro inimigo
			GameObject bullet = Instantiate(EnemyBulletGO);

			//definir a posição inicial do tiro
			bullet.transform.position = transform.position;

			//calcular a direção mirando a nave do jogador
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			//definir a direção do tiro
			bullet.GetComponent<L2EnemyBullet>().SetDirection(direction);
		}
	}
}
