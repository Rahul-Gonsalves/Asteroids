using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Points : MonoBehaviour
{
    [SerializeField] Text text;
    int points = 0;
    private void FixedUpdate()
    {
        text.text = "Points: "+points;
    }
    public void addPoints(int point)
    {
        points += point;
    }
}
