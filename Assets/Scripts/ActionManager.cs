using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{
    public enum Action
    {
        Walk = 1,
        Attack = 2,
        Shoot = 3
    }

    public delegate void DoAction(Action action, Vector2 position);
    public static event DoAction OnActionSelected;

    public Canvas actionMenu;
    public EventSystem eventSystem;
    public float offsetY;
    public float offsetX;

    private Camera mainCamera;
    private Vector2 clickedMousePos;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (actionMenu.isActiveAndEnabled && eventSystem.currentSelectedGameObject == null)
            {
                actionMenu.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(1) && !PlayerBehaviour.isPlayingActions)
        {
            actionMenu.gameObject.SetActive(true);
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(actionMenu.transform as RectTransform, Input.mousePosition, actionMenu.worldCamera, out pos);
            actionMenu.transform.position = actionMenu.transform.TransformPoint(pos + Vector2.up * offsetY + Vector2.right * offsetX);
            clickedMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    /// <summary>
    /// Realiza la accion seleccionada
    /// </summary>
    /// <param name="actionIndex">Indice de accion que corresponde al enum de Action</param>
    public void OnActionClick(int actionIndex)
    {
        actionMenu.gameObject.SetActive(false);

        if (OnActionSelected != null)
        {
            OnActionSelected((Action)actionIndex, clickedMousePos);
        }
    }
}
