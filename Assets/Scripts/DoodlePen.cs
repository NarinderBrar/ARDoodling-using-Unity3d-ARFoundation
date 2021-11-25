using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DoodlePen : MonoBehaviour
{
    public Camera camera_;

    public DoodleLine linePrefab = null;
    public Color color;

    private float myLineWidth = 0.01f;
    private int mySortingOrder = 1;

    public float offset = 1.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SpawnNewLine();
    }

    public void SpawnNewLine()
    {
        if (linePrefab !=null)
        {
            var newLine = Instantiate(linePrefab);

            newLine.raycastDelegate = GetPoint;
            newLine.gameObject.SetActive(true);

            newLine.lineColour = color;
            newLine.SetLineOrder(mySortingOrder);
            newLine.ChangeLineWidth(myLineWidth);

            newLine.gameObject.transform.parent = transform;
            
            mySortingOrder++;
        }
    }

    bool GetPoint(out Vector3 hitPosition)
    {
        Ray ray = camera_.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(camera_.transform.position, ray.direction*10, Color.yellow);

        hitPosition = camera_.transform.position + ray.direction * offset;
        return true;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
