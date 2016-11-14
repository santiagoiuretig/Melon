using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public delegate void BulletHit(GameObject target);
	public static event BulletHit OnBulletHit;

	public Vector3 direction;
	public float speed;

	void Update(){
		transform.Translate (direction * speed, Space.World);
	}

	void OnTriggerEnter2D(Collider2D cInfo){
		if (OnBulletHit != null) {
			OnBulletHit (cInfo.gameObject);
		}
		Destroy (this.gameObject);
	}
}
