using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapSectionDetector detector = null;

    [SerializeField] private GameObject startSection = null;
    [SerializeField] private GameObject endSection = null;
    [SerializeField] private GameObject[] middleSection = null;

    [SerializeField] private GameObject startPoint = null;

    [SerializeField] private Vector3 zeroPosition = Vector3.zero;
    [SerializeField] private Vector2 sectionSize = Vector2.zero;

    //[SerializeField] private int endGenerateCount = 3;

    private Dictionary<int, List<int>> coordinateRecords;

    //private int[] generateCount = new int[4]; // 각 방향 생성 개수, 위쪽 오른쪽 아래쪽 왼쪽 순서

    private bool isEndGame = false;

    void Start()
    {
        coordinateRecords = new Dictionary<int, List<int>>();
        if (startPoint != null)
        {
            zeroPosition = startPoint.transform.position;
        }
        if(startPoint == null)
        {
            Instantiate(startSection, zeroPosition,  Quaternion.identity);
        }

        firstCreateSections();

        detector.NowSectionIndex = Vector2.zero;
        detector.NowSectionPosition = zeroPosition;
        detector.SectionSize = sectionSize;

        addCoordinate(0, 0); // 최초 생성 좌표 등록
    }

    // Update is called once per frame
    void Update()
    {
        moveOnSection(detector.checkMoveOn());
    }

    private void moveOnSection(Direction directon)
    {
        switch (directon)
        {
            case Direction.UP:
                detector.NowSectionPosition += new Vector3(0, sectionSize.y, 0);
                detector.NowSectionIndex += Vector2.up;

                createSection(Direction.UPLEFT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.UP, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.UPRIGHT, detector.NowSectionIndex, detector.NowSectionPosition);

                break;
            case Direction.RIGHT:
                detector.NowSectionPosition += new Vector3(sectionSize.x, 0, 0);
                detector.NowSectionIndex += Vector2.right;

                createSection(Direction.UPRIGHT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.RIGHT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.DOWNRIGHT, detector.NowSectionIndex, detector.NowSectionPosition);
                break;
            case Direction.DOWN:
                detector.NowSectionPosition += new Vector3(0, -sectionSize.y, 0);
                detector.NowSectionIndex += Vector2.down;

                createSection(Direction.DOWNLEFT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.DOWN, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.DOWNRIGHT, detector.NowSectionIndex, detector.NowSectionPosition);
                break;
            case Direction.LEFT:
                detector.NowSectionPosition += new Vector3(-sectionSize.x, 0, 0);
                detector.NowSectionIndex += Vector2.left;

                createSection(Direction.UPLEFT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.LEFT, detector.NowSectionIndex, detector.NowSectionPosition);
                createSection(Direction.DOWNLEFT, detector.NowSectionIndex, detector.NowSectionPosition);
                break;
        }
    }

    private void firstCreateSections()
    {
        // x증가 오른쪽, y증가 위쪽
        for(int i = 1; i < 9; i++)
        {
            createSection((Direction)i, detector.NowSectionIndex, detector.NowSectionPosition);
        }
    }

    private void createLeftSections(Vector3 targetPosition)
    {
        Vector3[] _positions = new Vector3[3];

        _positions[0] = new Vector3(targetPosition.x - sectionSize.x, targetPosition.y + sectionSize.y, 0); // 왼쪽 위
        _positions[1] = new Vector3(targetPosition.x - sectionSize.x, targetPosition.y, 0); // 왼쪽
        _positions[2] = new Vector3(targetPosition.x - sectionSize.x, targetPosition.y - sectionSize.y, 0); // 왼쪽 아래

        createSections(middleSection[1], _positions);
    }

    private void createUpSections(Vector3 targetPosition)
    {
        Vector3[] _positions = new Vector3[3];

        _positions[0] = new Vector3(targetPosition.x - sectionSize.x, targetPosition.y + sectionSize.y, 0); // 왼쪽 위
        _positions[1] = new Vector3(targetPosition.x, targetPosition.y + sectionSize.y, 0); // 위쪽
        _positions[2] = new Vector3(targetPosition.x + sectionSize.x, targetPosition.y + sectionSize.y, 0); // 오른쪽 위

        createSections(middleSection[2], _positions);
    }

    private void createRightSections(Vector3 targetPosition)
    {
        Vector3[] _positions = new Vector3[3];

        _positions[0] = new Vector3(targetPosition.x + sectionSize.x, targetPosition.y + sectionSize.y, 0); // 오른쪽 위
        _positions[1] = new Vector3(targetPosition.x + sectionSize.x, targetPosition.y, 0); // 오른쪽
        _positions[2] = new Vector3(targetPosition.x + sectionSize.x, targetPosition.y - sectionSize.y, 0); // 오른쪽 아래

        createSections(middleSection[3], _positions);
    }

    private void createDownSections(Vector3 targetPosition)
    {
        Vector3[] _positions = new Vector3[3];

        _positions[0] = new Vector3(targetPosition.x - sectionSize.x, targetPosition.y - sectionSize.y, 0); // 왼쪽 아래
        _positions[1] = new Vector3(targetPosition.x, targetPosition.y - sectionSize.y, 0); // 아래쪽
        _positions[2] = new Vector3(targetPosition.x + sectionSize.x, targetPosition.y - sectionSize.y, 0); // 오른쪽 아래

        createSections(middleSection[3], _positions);
    }

    private void createSections(GameObject[] createObjects, Vector3[] targetPositions)
    {
        for (int i = 0; i < targetPositions.Length; i++)
        {
            Instantiate(createObjects[i], targetPositions[i], Quaternion.identity);
        }
    }

    private void createSections(GameObject createObjects, Vector3[] targetPositions)
    {
        for (int i = 0; i < targetPositions.Length; i++)
        {
            Instantiate(createObjects, targetPositions[i], Quaternion.identity);
        }
    }

    private void createSection(Direction directon, Vector2 nowIndex, Vector3 nowPosition)
    {
        Vector3 _targetPosition = Vector3.zero;

        _targetPosition = getCreateSectionPosition(directon, nowIndex, nowPosition);

        if (_targetPosition != Vector3.zero)
        {
            if(!isEndGame && GameManager.Instance.isGenerateCountEnd())
            {
                isEndGame = true;
                Instantiate(endSection, _targetPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(randMiddleSection(), _targetPosition, Quaternion.identity);
            }
            
        }
    }


    private GameObject randMiddleSection()
    {
        int _index = Random.Range(0, middleSection.Length);

        return middleSection[_index];
    }

    private Vector3 getCreateSectionPosition(Direction directon, Vector2 nowIndex, Vector3 nowPosition)
    {
        Vector3 _result = Vector3.zero;

        switch (directon)
        {
            case Direction.UPLEFT:
                if (addCoordinate((int)nowIndex.x - 1, (int)nowIndex.y + 1))
                {
                    _result = new Vector3(nowPosition.x - sectionSize.x, nowPosition.y + sectionSize.y, 0);
                }
                break;
            case Direction.UP:
                if (addCoordinate((int)nowIndex.x, (int)nowIndex.y + 1))
                {
                    _result = new Vector3(nowPosition.x, nowPosition.y + sectionSize.y, 0);
                    GameManager.Instance.addGenerateCount(0, 1); // 각 방향별 생성 갯수
                }
                break;
            case Direction.UPRIGHT:
                if (addCoordinate((int)nowIndex.x + 1, (int)nowIndex.y + 1))
                {
                    _result = new Vector3(nowPosition.x + sectionSize.x, nowPosition.y + sectionSize.y, 0);
                }
                break;
            case Direction.RIGHT:
                if (addCoordinate((int)nowIndex.x + 1, (int)nowIndex.y))
                {
                    _result = new Vector3(nowPosition.x + sectionSize.x, nowPosition.y, 0);
                    GameManager.Instance.addGenerateCount(1, 1); // 각 방향별 생성 갯수
                }
                break;
            case Direction.DOWNRIGHT:
                if (addCoordinate((int)nowIndex.x + 1, (int)nowIndex.y - 1))
                {
                    _result = new Vector3(nowPosition.x + sectionSize.x, nowPosition.y - sectionSize.y, 0);
                }
                break;
            case Direction.DOWN:
                if (addCoordinate((int)nowIndex.x, (int)nowIndex.y - 1))
                {
                    _result = new Vector3(nowPosition.x, nowPosition.y - sectionSize.y, 0);
                    GameManager.Instance.addGenerateCount(2, 1); // 각 방향별 생성 갯수
                }
                break;
            case Direction.DOWNLEFT:
                if (addCoordinate((int)nowIndex.x - 1, (int)nowIndex.y - 1))
                {
                    _result = new Vector3(nowPosition.x - sectionSize.x, nowPosition.y - sectionSize.y, 0);
                }
                break;
            case Direction.LEFT:
                if (addCoordinate((int)nowIndex.x - 1, (int)nowIndex.y))
                {
                    _result = new Vector3(nowPosition.x - sectionSize.x, nowPosition.y, 0);
                    GameManager.Instance.addGenerateCount(3, 1); // 각 방향별 생성 갯수
                }
                break;
        }

        return _result;
    }

    private bool addCoordinate(int x, int y)
    {
        if (coordinateRecords.ContainsKey(x))
        {
            if (coordinateRecords[x].Contains(y))
                return false;
            else
                coordinateRecords[x].Add(y);
        }
        else
        {
            List<int> _temp = new List<int>();
            _temp.Add(y);
            coordinateRecords.Add(x, _temp);
        }

        return true;
    }
}
