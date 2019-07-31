using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSectionDetector : MonoBehaviour
{
    [SerializeField] private Vector2 nowSectionIndex; // MapGenerator에서 값을 받아온다.
    private Vector3 nowSectionPosition; // MapGenerator에서 값을 받아온다.
    private Vector2 sectionSize; // MapGenerator에서 값을 받아온다.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Directon checkMoveOn()
    {
        // transform.position과 현재 위치한 Section의 경계 위치를 비교하여 다른 Section으로 이동하였다면
        // 기존 Section을 기준으로 이동한 Section의 방향을 반환한다.
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
        // 플레이어 현재 위치를 기준으로 매개변수(방향) 값에 따라 위치한 Section의 위치값을 반환한다.
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
