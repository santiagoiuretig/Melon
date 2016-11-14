using UnityEngine;
using System.Collections;

public class OrangeHeadEnemy : EnemyBehaviour {

	public GameObject bullet;
	public float shootingRate;

	private Bullet bulletScript;
	private GameObject tmp;

	//Shoot
	public override void Action(){
		InvokeRepeating ("Shoot", 0, shootingRate);
	}

	private void Shoot(){
		if (seeingPlayer) {
			tmp = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			bulletScript = tmp.GetComponent<Bullet> ();
			bulletScript.direction = (player.transform.position - transform.position).normalized;
		}
	}
}
