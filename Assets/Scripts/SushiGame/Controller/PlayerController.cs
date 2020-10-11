using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private Transform _centre;
    [SerializeField] private float _startWeight;
    [SerializeField] private float _startEnergy;

    public float Weight
    {
        get => _startWeight;
        set => _startWeight = value;
    }

    public float Energy
    {
        get => _startEnergy;
        set => _startEnergy = value;
    }

    private void Update()
    {
        var xMov = Input.GetAxis("Horizontal");
        var yMov = Input.GetAxis("Vertical");
        var nextPos = transform.position + new Vector3(xMov, yMov, 0).normalized * (_speed * Time.deltaTime);
        
        //this is bugged af, makes it reset to left or right. NEEDS FIXING. Really bad
        if (xMov != 0 || yMov != 0) transform.up = new Vector3(xMov, yMov, 0);
        
        if (Vector3.Distance(_centre.position, nextPos) > _maxDistance) return;
        
        transform.position += new Vector3(xMov, yMov, 0).normalized * (_speed * Time.deltaTime);
    }
    
    [Header("Debug Circle")]
    [SerializeField] private int _segments = 32;
    [SerializeField] private Color _color = Color.blue;

    private void OnDrawGizmos()
    {
        DrawEllipse(_centre.position, _centre.forward, _centre.up, _maxDistance, _maxDistance, _segments, _color);
    }
 
    private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0)
    {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(forward, up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;
 
        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;
 
            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }
 
            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }
}
