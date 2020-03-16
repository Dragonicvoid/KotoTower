using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOnTowerTesting : MonoBehaviour
{
    string colorId = "_Color";
    GameObject circle;

    // Start is called before the first frame update
    void Start()
    {
        // creating circle inside this object
        this.circle = new GameObject();
        this.circle.SetActive(false);
        this.circle.name = "circle";
        this.circle.transform.position = this.transform.position + new Vector3(0, 0, 1);
        this.circle.transform.parent = this.transform;
        LineRenderer lineCircle = circle.AddComponent(typeof(LineRenderer)) as LineRenderer;
        lineCircle = createCircle(this.gameObject.GetComponent<ShootTargetTesting>().getRadius(), lineCircle, Color.red);
    }


    // create a circle around tower
    LineRenderer createCircle(float radius, LineRenderer line, Color color)
    {
        float segment = 360f;
        line.sortingOrder = 1;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.material.color = color;
        line.SetVertexCount(2);
        line.useWorldSpace = false;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = Mathf.FloorToInt(segment + 1f);

        int pointCount = Mathf.FloorToInt(segment + 1f);
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segment);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
        return line;
    }

    public void setCircleToActive(bool isActive)
    {
        if(this.circle != null)
            this.circle.SetActive(isActive);
    }
}
