using UnityEngine;
using System.Collections;

public class PathDrawer : MonoBehaviour
{
    public float startWidth;
    public float endWidth;
    public Color[] startColors;
    public Color[] endColors;
    public Material defaultMaterial;

    private Vector2 startPos;
    private Vector2 endPos;
    private int lineCount = 0;

    void Awake()
    {
        ActionManager.OnActionSelected += OnActionSelected;
    }

    private void OnActionSelected(ActionManager.Action action, Vector2 position)
    {
        endPos = position;
        if (lineCount > 0)
        {
            startPos = position;
        }

        DrawPath();
    }

    void OnDestroy()
    {
        ActionManager.OnActionSelected -= OnActionSelected;
    }

    void Start()
    {
        lineCount = 0;
        startPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void DrawPath()
    {
        GameObject go = new GameObject();
        go.name = "LinePath";
        go.transform.SetParent(transform);

        LineRenderer lineRenderer = go.AddComponent<LineRenderer>();
        lineRenderer.material = defaultMaterial;
        lineRenderer.SetPositions(new Vector3[] { startPos, endPos });
        lineRenderer.SetColors(startColors[lineCount], endColors[lineCount]);
        lineRenderer.SetWidth(startWidth, endWidth);
        lineRenderer.SetVertexCount(2);

        lineCount++;
    }
}
