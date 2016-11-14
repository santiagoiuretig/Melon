using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private float lapseToComplete;
    private Vector2 from;
    private Vector2 to;
    private float t;

    protected void StartMoving(Vector2 _from, Vector2 _to, float _lapse)
    {
        from = _from;
        to = _to;
        lapseToComplete = _lapse;

        KeepMoving();
    }

    protected bool KeepMoving()
    {
        if (transform.position.x != to.x || transform.position.y != to.y)
        {
            LerpPosition(from, to, lapseToComplete);
        }

        return IsMoving();
    }

    protected bool IsMoving()
    {
        return t < lapseToComplete;
    }

    private void LerpPosition(Vector3 _from, Vector3 _to, float _lapse)
    {
        if (t < _lapse)
        {
            t += Time.deltaTime;
            transform.position = CalcLerpPosition(_from, _to, _lapse, t);
        }
        else
        {
            t = 0;
        }
    }

    private Vector2 CalcLerpPosition(Vector3 _from, Vector3 _to, float _lapse, float _t)
    {
        Vector2 retVal = Vector2.zero;

        retVal.x = Mathf.Lerp(_from.x, _to.x, t / _lapse);
        retVal.y = Mathf.Lerp(_from.y, _to.y, t / _lapse);

        return retVal;
    }
}
