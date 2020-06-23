using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Property : MonoBehaviour
{
    [SerializeField] private Text propertyText;
    void Start()
    {
           propertyText.text="Made by Aldhair Vera";
    }
}
