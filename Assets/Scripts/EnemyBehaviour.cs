using UnityEngine;
using System.Collections;

public class EnemyBehaviour : Character
{
    //Walk
    public Vector3 from;
    public Vector3 to;
    public float pauseInterval;
    public bool seeingPlayer;
    public float lapse;

    private float pauseTimer = 0f;
    private Vector3 tmpPos;
    private Vector3 tmpRot;

    //Vision
    public float angle;
    public float range;
    public bool renderFieldOfView;

    private bool flagAction;
    private LineRenderer lineRenderer;
    protected GameObject player;
    private float alpha;
    private float hip;
    private Vector3 heading;
    private float distance;

    void Awake()
    {
        Bullet.OnBulletHit += OnBulletHit;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (renderFieldOfView)
        {
            lineRenderer.enabled = true;
            DrawVisionTriangle();
        }
        else
        {
            lineRenderer.enabled = false;
        }

        CheckIfSeeing();

        if (!seeingPlayer)
        {
            Walk(from, to, lapse);
            flagAction = false;
        }
        else if (!flagAction)
        {
            Action();
            flagAction = true;
        }
    }

    void OnDestroy()
    {
        Bullet.OnBulletHit -= OnBulletHit;
    }

    public void Walk(Vector3 _from, Vector3 _to, float _lapse)
    {
        StartMoving(_from, _to, _lapse);
        if (transform.position == to)
        {
            pauseTimer += Time.deltaTime;

            if (pauseTimer >= pauseInterval)
            {
                InvertPositions();
                pauseTimer = 0f;
            }
        }
    }

    public virtual void Action() { }

    private void InvertPositions()
    {
        tmpPos.x = to.x;
        to.x = from.x;
        from.x = tmpPos.x;

        tmpPos.y = to.y;
        to.y = from.y;
        from.y = tmpPos.y;

        if (from != to)
        {
            tmpRot.z = transform.eulerAngles.z;
            tmpRot.z += 180;
            transform.eulerAngles = tmpRot;
        }
    }

    private void CheckIfSeeing()
    {
        if (player)
        {
            if (Vector3.Angle(transform.right, player.transform.position - transform.position) < angle / 2 && CheckDistance())
            {
                if (!seeingPlayer)
                {
                    seeingPlayer = true;
                }
            }
            else
            {
                if (seeingPlayer)
                {
                    seeingPlayer = false;
                }
            }
        }
        else
        {
            seeingPlayer = false;
        }
    }

    private void DrawVisionTriangle()
    {
        alpha = angle;
        hip = (2 * (range * Mathf.Tan((alpha / 2) * Mathf.Deg2Rad)));
        lineRenderer.SetPosition(1, new Vector3(range, 0, 0));
        lineRenderer.SetWidth(0, hip);
    }

    private bool CheckDistance()
    {
        heading = player.transform.position - transform.position;
        distance = heading.magnitude;
        if (range >= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnBulletHit(GameObject target)
    {
        if (target == this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

}
