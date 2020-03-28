using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class enemyDetectBehaviourScript : MonoBehaviour
{
    [Range(0, 360)]
    public float Alertangle;
    [Range(0, 10)]
    public float AlertRadius;

    public bool Alertistrue = false;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Alert();
    }

    private void OnDrawGizmos()
    {
        Color color = Handles.color;
        Handles.color = Color.blue;
        Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
        Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
        Handles.color = color;
    }

    void Alert()
    {
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        Vector3 vector3 = target.transform.position - this.transform.position;
        float angle = Vector3.Angle(vector3, this.transform.forward);
        if (distance <= AlertRadius && angle <= Alertangle)
        {
            print("Alertistrue is True.");
            Alertistrue = true;
        }
        else
        {
            print("Alertistrue is False.");
            Alertistrue = false;
        }
    }
}
