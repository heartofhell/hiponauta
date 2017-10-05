using UnityEngine;
using System.Collections;

public class BossGun : MonoBehaviour {
	public GameObject EnemyBulletGO; //este é o prefab do tiro inimigo
	public GameObject BossObj;
	public float fireRate; //Taxa de tiros
	private float nextFire; //intervalo entre os tiros
	BossController _Boss;

	// Use this for initialization
	void Start () {
		_Boss = BossObj.GetComponent<BossController>();
	}

	// Update is called once per frame
	void Update () {

		//if (_Boss.StartLv2== true)
		//{
		if (Time.time > nextFire) {
			//proximo time será feito no tempo atual+taxa de tiro, ou seja, daqui a "Taxa de tiro" segundos depois
			nextFire = Time.time + fireRate;

			//disparar tiro inimigo a cada 1s
			FireEnemyBullet();
		}
		//}


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
