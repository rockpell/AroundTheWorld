using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] private float windDirection;
    [SerializeField] private WindSpeed windSpeed;

    [SerializeField] private float OriginRefreshTime;
    [SerializeField] private float typoonProbability;
    [SerializeField] private float typoonSpeed;
    private float refreshTime;
    private float currentTime;

    [SerializeField] private Vector3 typoonPosition;
    [SerializeField] private Vector3 typoonDestination;

    [SerializeField] private GameObject typoon;
    [SerializeField] private MapSectionDetector mapSectionDetector;

    void Start()
    {
        refreshTime = OriginRefreshTime;
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
    private Directon randomDirection()
    {
        switch (Random.Range(0, 8))
        {
            case 0:
                return Directon.UPLEFT;
            case 1:
                return Directon.UP;
            case 2:
                return Directon.UPRIGHT;
            case 3:
                return Directon.RIGHT;
            case 4:
                return Directon.DOWNRIGHT;
            case 5:
                return Directon.DOWN;
            case 6:
                return Directon.DOWNLEFT;
            case 7:
                return Directon.LEFT;
            default:
                return Directon.NONE;
        }
    }
    private Directon directionDetect(Directon origin, Directon direction)
    {
        return (origin == direction) ? Directon.NONE : direction;
    }
    private Directon randomDirection(Directon origin)
    {
        Directon _directon = Directon.NONE;

        while(_directon == Directon.NONE)
        {
            switch (Random.Range(0, 8))
            {
                case 0:
                    if (directionDetect(origin, Directon.UPLEFT) == Directon.UPLEFT)
                        return Directon.UPLEFT;
                    else
                        break;
                case 1:
                    if (directionDetect(origin, Directon.UP) == Directon.UP)
                        return Directon.UPLEFT;
                    else
                        break;
                case 2:
                    if (directionDetect(origin, Directon.UPRIGHT) == Directon.UPRIGHT)
                        return Directon.UPLEFT;
                    else
                        break;
                case 3:
                    if (directionDetect(origin, Directon.RIGHT) == Directon.RIGHT)
                        return Directon.UPLEFT;
                    else
                        break;
                case 4:
                    if (directionDetect(origin, Directon.DOWNRIGHT) == Directon.DOWNRIGHT)
                        return Directon.UPLEFT;
                    else
                        break;
                case 5:
                    if (directionDetect(origin, Directon.DOWN) == Directon.DOWN)
                        return Directon.UPLEFT;
                    else
                        break;
                case 6:
                    if (directionDetect(origin, Directon.DOWNLEFT) == Directon.DOWNLEFT)
                        return Directon.UPLEFT;
                    else
                        break;
                case 7:
                    if (directionDetect(origin, Directon.LEFT) == Directon.LEFT)
                        return Directon.UPLEFT;
                    else
                        break;
                default:
                    return Directon.NONE;
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
        Directon _directon = randomDirection();

        typoonPosition = mapSectionDetector.getPlayerAroundSectionPosition(_directon);
        typoonDestination = mapSectionDetector.getPlayerAroundSectionPosition(randomDirection(_directon));

        typoon.GetComponent<Typoon>().initialize(typoonPosition, typoonDestination, typoonSpeed);
        Instantiate(typoon);
    }
    private void refreshWind()
    {
        windDirection = Random.Range(0, 360);
        int _windSpeed = Random.Range(0, 3);
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
            default:
                break;
        }
    }

    public float WindDirection
    { get { return windDirection; } }
    public WindSpeed WindSpeed
    { get { return windSpeed; } }
}
