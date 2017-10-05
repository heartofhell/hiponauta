using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {
	public GameObject EnemyBulletGO; //este é o prefab do tiro inimigo

	// Use this for initialization
	void Start () {
		//disparar tiro inimigo a cada 1s
		Invoke("FireEnemyBullet", 1f);

	}

	// Update is called once per frame
	void Update () {

	}

	//função para o inimigo atirar
	void FireEnemyBullet () {
		//pegar uma referencia da nave do jogador
		GameObject playerShip = GameObject.Find("PlayerGO");

		if (playerShip != null) //se o player não estiver morto
		{
			//instanciar um tiro inimigo
			GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

			//definir a posição inicial do tiro
			bullet.transform.position = transform.position;

			//calcular a direção mirando a nave do jogador
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			//definir a direção do tiro
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
		}
	}
}
