using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] private float windDirection;
    [SerializeField] private WindSpeed windSpeed;

    [SerializeField] private float originRefreshTime;
    [SerializeField] private float typoonProbability;
    [SerializeField] private float typoonSpeed;
    private float refreshTime;
    private float currentTime;

    [SerializeField] private Vector3 typoonPosition;
    [SerializeField] private Vector3 typoonDestination;

    [SerializeField] private GameObject typoon;
    [SerializeField] private MapSectionDetector mapSectionDetector;

    [SerializeField] private GameObject windDirectionImage;
    [SerializeField] private UnityEngine.UI.Text windSpeedText;

    private bool isTypoon;

    void Start()
    {
        refreshTime = originRefreshTime;
        refreshWind();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > refreshTime)
        {
            refreshWind();
            currentTime = 0;
            if(Random.Range(0,1.0f) < typoonProbability)
            {
                typoonGenerator();
            }
        }
    }
    private Direction randomDirection()
    {
        switch (Random.Range(0, 8))
        {
            case 0:
                return Direction.UPLEFT;
            case 1:
                return Direction.UP;
            case 2:
                return Direction.UPRIGHT;
            case 3:
                return Direction.RIGHT;
            case 4:
                return Direction.DOWNRIGHT;
            case 5:
                return Direction.DOWN;
            case 6:
                return Direction.DOWNLEFT;
            case 7:
                return Direction.LEFT;
            default:
                return Direction.NONE;
        }
    }
    private Direction directionDetect(Direction origin, Direction direction)
    {
        return (origin == direction) ? Direction.NONE : direction;
    }
    private Direction randomDirection(Direction origin)
    {
        Direction _directon = Direction.NONE;

        while(_directon == Direction.NONE)
        {
            switch (Random.Range(0, 8))
            {
                case 0:
                    if (directionDetect(origin, Direction.UPLEFT) == Direction.UPLEFT)
                        return Direction.UPLEFT;
                    else
                        break;
                case 1:
                    if (directionDetect(origin, Direction.UP) == Direction.UP)
                        return Direction.UP;
                    else
                        break;
                case 2:
                    if (directionDetect(origin, Direction.UPRIGHT) == Direction.UPRIGHT)
                        return Direction.UPRIGHT;
                    else
                        break;
                case 3:
                    if (directionDetect(origin, Direction.RIGHT) == Direction.RIGHT)
                        return Direction.RIGHT;
                    else
                        break;
                case 4:
                    if (directionDetect(origin, Direction.DOWNRIGHT) == Direction.DOWNRIGHT)
                        return Direction.DOWNRIGHT;
                    else
                        break;
                case 5:
                    if (directionDetect(origin, Direction.DOWN) == Direction.DOWN)
                        return Direction.DOWN;
                    else
                        break;
                case 6:
                    if (directionDetect(origin, Direction.DOWNLEFT) == Direction.DOWNLEFT)
                        return Direction.DOWNLEFT;
                    else
                        break;
                case 7:
                    if (directionDetect(origin, Direction.LEFT) == Direction.LEFT)
                        return Direction.LEFT;
                    else
                        break;
                default:
                    return Direction.NONE;
            }
        }
        return _directon;
    }
    public void typoonGenerator()
    {
        //태풍이 생성될 타일에서 플레이어쪽으로 이동하도록
        //이동 속도는 바람 속도 천천히 떨어지는 방식?
        //바람 변경 주기는 지속적으로 변하되 속도는 
        //태풍 영역 안에서만 바람 방향에 영향을 주도록 설정
        //이 함수에서는 태풍을 생성하고 이동 방향을 지정하는 정도로만
        Direction _directon = randomDirection();

        typoonPosition = mapSectionDetector.getPlayerAroundSectionPosition(_directon);
        typoonDestination = mapSectionDetector.getPlayerAroundSectionPosition(randomDirection(_directon));

        typoon.GetComponent<Typoon>().initialize(typoonPosition, typoonDestination, typoonSpeed);
        Instantiate(typoon);
    }
    private void refreshWind()
    {
        windDirection = Random.Range(0, 360);
        int _windSpeed = Random.Range(0, 3);
        if (isTypoon)
            _windSpeed = 3;
        switch(_windSpeed)
        {
            case 0:
                //대충 세기 1정도
                windSpeed = global::WindSpeed.NORMAL;
                break;
            case 1:
                //세기 2정도
                windSpeed = global::WindSpeed.MIDDLE;
                break;
            case 2:
                //세기 3정도
                windSpeed = global::WindSpeed.STRONG;
                break;
            case 3:
                windSpeed = global::WindSpeed.TYPOON;
                break;
        }
        windSpeedText.text = windSpeed.ToString();
        windDirectionImage.transform.rotation = Quaternion.Euler(0, 0, windDirection);
    }

    public float WindDirection
    { get { return windDirection; } }
    public WindSpeed WindSpeed
    { get { return windSpeed; } }
    public float RefreshTime
    { set { refreshTime = value; } }
    public float OriginRefreshTime
    { get { return originRefreshTime; } }
    public bool IsTypoon
    { set { isTypoon = value; } }
    public void RefreshWind()
    {
        refreshWind();
    }
}
