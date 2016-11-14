using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	public GameObject player;
	public float angle;
	public float range;
	public LineRenderer lineRenderer;

	private bool isLooking = false;

	private float alpha;
	private float hip;
	private Vector3 heading;
	private float distance;

	void Update () {
		DrawVisionTriangle ();

		//If looking at player...
		if (Vector3.Angle (transform.right, player.transform.position - transform.position) < angle / 2 && CheckDistance()) {
			if (!isLooking) {
				Debug.Log ("In");
				isLooking = true;
			}
		}
		else {
			if (isLooking) {
				Debug.Log ("Out");
				isLooking = false;
			}
		}
	}

	private void DrawVisionTriangle(){
		alpha = angle;
		hip = (2 * (range * Mathf.Tan ((alpha / 2) * Mathf.Deg2Rad)));
		lineRenderer.SetPosition (1, new Vector3 (range, 0, 0));
		lineRenderer.SetWidth (0, hip);
	}

	private bool CheckDistance(){
		heading = player.transform.position - transform.position;
		distance = heading.magnitude;
		if (range >= distance) {
			return true;
		} else {
			return false;
		}
	}

/*
	public int segments;
    public float xradius;
    public float yradius;
       
    void Start ()
    {

       
		lineRenderer.SetVertexCount (segments + 1);
		lineRenderer.useWorldSpace = false;
        CreatePoints ();
    }
   
   
    void CreatePoints ()
    {
        float x;
        float y;
        float z = 0f;
       
        float angle = 20f;
       
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;
                   
			lineRenderer.SetPosition (i,new Vector3(x,y,z) );
                   
            angle += (360f / segments);
        }
    }
*/

}
