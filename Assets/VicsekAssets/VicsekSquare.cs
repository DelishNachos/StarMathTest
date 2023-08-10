using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VicsekSquare : MonoBehaviour
{
    
    public Vector3[] split()
    {
        Vector3[] pos = new Vector3[5];
        Vector3 currentScale = transform.localScale;
        Vector3 scale = currentScale / 3;
        pos[0] = new Vector3(transform.position.x, transform.position.y + (currentScale.y / 2) - (scale.y / 2), 0);
        pos[1] = new Vector3(transform.position.x - (currentScale.y / 2) + (scale.y / 2), transform.position.y, 0);
        pos[2] = transform.position;
        pos[3] = new Vector3(transform.position.x + (currentScale.y / 2) - (scale.y / 2), transform.position.y, 0);
        pos[4] = new Vector3(transform.position.x, transform.position.y - (currentScale.y / 2) + (scale.y / 2), 0);
        return pos;
    }

    public Vector3[] splitDiagonal()
    {
        Vector3[] pos = new Vector3[5];
        Vector3 currentScale = transform.localScale;
        Vector3 scale = currentScale / 3;
        pos[0] = new Vector3(transform.position.x - (currentScale.y / 2) + (scale.y / 2), transform.position.y + (currentScale.y / 2) - (scale.y / 2), 0);
        pos[1] = new Vector3(transform.position.x + (currentScale.y / 2) - (scale.y / 2), transform.position.y + (currentScale.y / 2) - (scale.y / 2), 0);
        pos[2] = transform.position;
        pos[3] = new Vector3(transform.position.x - (currentScale.y / 2) + (scale.y / 2), transform.position.y - (currentScale.y / 2) + (scale.y / 2), 0);
        pos[4] = new Vector3(transform.position.x + (currentScale.y / 2) - (scale.y / 2), transform.position.y - (currentScale.y / 2) + (scale.y / 2), 0);
        return pos;
    }
}
