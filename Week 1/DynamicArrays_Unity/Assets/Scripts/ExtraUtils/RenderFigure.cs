using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RenderFigure : MonoBehaviour
{
    private OrderedDynamicArray<Rectangle> rectangles = new OrderedDynamicArray<Rectangle>();
    private List<GameObject> lines = new List<GameObject>();
    [SerializeField] private List<Transform> positions = new List<Transform>();
    
    [SerializeField] private GameObject linePrefab;
    private void Start()
    {
        rectangles.Add(new Rectangle(3,2));
        rectangles.Add(new Rectangle(2,2));
        rectangles.Add(new Rectangle(1,2));

        for (int i = 0; i < rectangles.Count; i++)
        {
            lines.Add(Instantiate(linePrefab));
        }
        

        for (int i = 0; i < lines.Count; i++)
        {
            Vector2 size=new Vector2(rectangles.GetElement(i).Width,rectangles.GetElement(i).Height);
            lines[i].GetComponent<LineManager>().DrawRectangle(positions[i].transform.position, size);
        }
    }
}
