using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSectionDetector : MonoBehaviour
{
    [SerializeField] private Vector2 nowSectionIndex;
    private Vector3 nowSectionPosition;

    private Vector2 sectionSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Directon checkMoveOn()
    {

        return Directon.NONE;
    }

    public Vector2 SectionSize {
        set { sectionSize = value; }
    }

    public Vector2 NowSectionIndex {
        get { return nowSectionIndex; }
        set { nowSectionIndex = value; }
    }

    public Vector3 NowSectionPosition {
        get { return nowSectionPosition; }
        set
        {
            nowSectionPosition = value;
            transform.position = nowSectionPosition;
        }
    }
}
