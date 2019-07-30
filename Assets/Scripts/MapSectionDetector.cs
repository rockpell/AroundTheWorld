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
        Directon _directon = Directon.NONE;

        if (transform.position.x < nowSectionPosition.x - (sectionSize.x/2) ) // 왼쪽 경계선
        {
            _directon = Directon.LEFT;
        }
        else if(transform.position.x > nowSectionPosition.x + (sectionSize.x / 2)) // 오른쪽 경계선
        {
            _directon = Directon.RIGHT;
        }
        else if (transform.position.y > nowSectionPosition.y + (sectionSize.y / 2)) // 위쪽 경계선
        {
            _directon = Directon.UP;
        }
        else if (transform.position.y < nowSectionPosition.y - (sectionSize.y / 2)) // 아래쪽 경계선
        {
            _directon = Directon.DOWN;
        }

        return _directon;
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
        set { nowSectionPosition = value; }
    }
}
