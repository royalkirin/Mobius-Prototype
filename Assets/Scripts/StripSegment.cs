using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A minor optimization script to reduce time needed to calculate ditance from player
public class StripSegment : MonoBehaviour
{
    StripSegment[] AdjacentSegments;

    // Start is called before the first frame update
    void Start()
    {
        AdjacentSegments = new StripSegment[2];

        Transform strip = transform.parent;

        for (int i = 0; i < strip.GetChildCount(); i++)
        {
            Transform t = strip.GetChild(i);
            if (t != this.transform)
            {
                if (AdjacentSegments[0] == null || Vector3.Distance(this.transform.position, t.position) < Vector3.Distance(this.transform.position, AdjacentSegments[0].transform.position))
                {
                    AdjacentSegments[0] = t.gameObject.GetComponent<StripSegment>();
                }

                if (AdjacentSegments[1] == null || Vector3.Distance(this.transform.position, AdjacentSegments[0].transform.position) < Vector3.Distance(this.transform.position, AdjacentSegments[1].transform.position))
                {
                    StripSegment tmp = AdjacentSegments[0];
                    AdjacentSegments[0] = AdjacentSegments[1];
                    AdjacentSegments[1] = tmp;
                }
            }
        }
    }

    public Transform GetAdjacentSegments(int index)
    {
        if(index < 2)
        {
            return AdjacentSegments[index].transform;
        }
        else
        {
            Debug.Log("You done goofed");
            return transform;
        }
    }
}
