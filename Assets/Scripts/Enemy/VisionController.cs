using UnityEngine;
using System.Collections;

public class VisionController : MonoBehaviour {

	public float visionAngle;
	public float visionRange;

	private LineRenderer lineRenderer;
	private NavMeshController navMeshController;

	private float alpha;
	private float hip;
	private float distance;
	private Vector3 heading;

	void Awake(){
		navMeshController = GetComponent<NavMeshController> ();
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update(){
		DrawVisionTriangle ();
	}

	public bool CanSeePlayer(out RaycastHit hit, bool lookAtPlayer = false){
		Vector3 direction;
		if (lookAtPlayer) {
			direction = navMeshController.player.position - this.transform.position;
		} else if (Vector3.Angle (transform.right, navMeshController.player.position - transform.position) < visionAngle / 2 && CheckDistance ()) {
			direction = navMeshController.player.position - this.transform.position;
		} else {
			direction = this.transform.forward;
		}
		Debug.DrawRay (this.transform.position, direction * visionRange, Color.red, 0f);
		return Physics.Raycast (this.transform.position, direction * visionRange, out hit, visionRange) && hit.collider.CompareTag ("Player");
	}

	private void DrawVisionTriangle()
	{
		alpha = visionAngle;
		hip = (2 * (visionRange * Mathf.Tan((alpha / 2) * Mathf.Deg2Rad)));
		lineRenderer.SetPosition(1, new Vector3(0, 0, visionRange));
		lineRenderer.SetWidth(0, hip);
	}

	private bool CheckDistance()
	{
		heading = navMeshController.player.position - transform.position;
		distance = heading.magnitude;
		if (visionRange >= distance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
