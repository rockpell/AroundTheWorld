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

    public Vector3 getPlayerAroundSectionPosition(Directon directon)
    {
        Vector3 _result = Vector3.zero;

        switch (directon)
        {
            case Directon.UPLEFT:
                _result = new Vector3(nowSectionPosition.x - sectionSize.x, nowSectionPosition.y + sectionSize.y, 0);
                break;
            case Directon.UP:
                _result = new Vector3(nowSectionPosition.x, nowSectionPosition.y + sectionSize.y, 0);
                break;
            case Directon.UPRIGHT:
                _result = new Vector3(nowSectionPosition.x + sectionSize.x, nowSectionPosition.y + sectionSize.y, 0);
                break;
            case Directon.RIGHT:
                _result = new Vector3(nowSectionPosition.x + sectionSize.x, nowSectionPosition.y, 0);
                break;
            case Directon.DOWNRIGHT:
                _result = new Vector3(nowSectionPosition.x + sectionSize.x, nowSectionPosition.y - sectionSize.y, 0);
                break;
            case Directon.DOWN:
                _result = new Vector3(nowSectionPosition.x, nowSectionPosition.y - sectionSize.y, 0);
                break;
            case Directon.DOWNLEFT:
                _result = new Vector3(nowSectionPosition.x - sectionSize.x, nowSectionPosition.y - sectionSize.y, 0);
                break;
            case Directon.LEFT:
                _result = new Vector3(nowSectionPosition.x - sectionSize.x, nowSectionPosition.y, 0);
                break;
        }

        return _result;
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
