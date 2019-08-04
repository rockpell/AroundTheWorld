using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float selfDestroyTime;
    private float curTime;
    [SerializeField] private float detectRange;
    [SerializeField] private Transform shootPosition;

    [SerializeField] private float decisionDegree;
    [SerializeField] private float currentDegree;
    [SerializeField] private float modifiedDegree;
    private bool isAvoid;

    private GameObject playerShip;

    private Vector3 modifiedDirection;
    
    void Start()
    {
    }
    
    void Update()
    {
        selfDestroy();
        if(playerShip == null)
        {
            playerShip = GameManager.Instance.ShipBody.gameObject;
        }
        else
            moveShip();
    }

    private void rotateShip()
    {
        Vector3 _direction = playerShip.transform.position - this.transform.position;
        //현재 각도
        decisionDegree = this.transform.rotation.eulerAngles.z;
        //변경해야 할 각도(현재 각도에서 얼마나 더해주면 되는지)
        currentDegree = Quaternion.FromToRotation(transform.up, _direction).eulerAngles.z;
        
        if(currentDegree < 180)
        {
            decisionDegree += 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
        else if(currentDegree > 180)
        {
            decisionDegree -= 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
    }
    //angle만큼 비스듬하게 이동을 함
    private void rotateShip(float angle)
    {
        Vector3 _direction = playerShip.transform.position - this.transform.position;
        //현재 각도
        decisionDegree = this.transform.rotation.eulerAngles.z;
        //변경해야 할 각도(현재 각도에서 얼마나 더해주면 되는지)
        currentDegree = Quaternion.FromToRotation(transform.up, _direction).eulerAngles.z + angle;
        currentDegree %= 360;
        if (currentDegree < 180)
        {
            decisionDegree += 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
        else if (currentDegree > 180)
        {
            decisionDegree -= 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
    }

    private void avoidObstacle()
    {
        //rotateShip(angle)의 angle을 정해줘야 함
        //angle은 z성분의 euler값
        //전방에 장애물이 있는지 판정
        //없으면 직진, 있으면 일정 각도로 회피 (여기서는 플레이어와 일직선상으로 장애물이 없도록 하는 것을 목적으로 함)
        //회전 자체는 rotateShip(angle)을 사용
        //+방향 또는 -방향으로 충돌이 일어나는지 탐지를 해야 함
        //두 방향 중 절대값이 더 작은 방향을 선택
        //회전 변환 = (xcos - ysin, xsin + ycos)
        
        //처음 호출된 상태면 modified가 0일꺼라서 그냥 정면
        float _angle = this.transform.rotation.eulerAngles.z;
        if(isAvoid == false)
        {
            //여기는 처음 호출 시 작동함
            //어느 방향으로 회전하면 회피가 가능한지 판단
            //회전이 끝나면 isAvoid를 false로 바꿔 다음 장애물에도 대응할 수 있도록 설정
            float _modifiedAngle = _angle + 10;
            Vector3 _direction = new Vector3(this.transform.position.x * Mathf.Cos(_modifiedAngle) - this.transform.position.y * Mathf.Sin(_modifiedAngle),
                this.transform.position.x * Mathf.Sin(_modifiedAngle) + this.transform.position.y * Mathf.Cos(_modifiedAngle), this.transform.position.z);
            while (isObstacle(_direction) == true)
            {
                //장애물 있는 경우 10도씩 올려가며 장애물 있는지 판정
                _modifiedAngle += 10;
                _direction.x = this.transform.position.x * Mathf.Cos(_modifiedAngle) - this.transform.position.y * Mathf.Sin(_modifiedAngle);
                _direction.y = this.transform.position.x * Mathf.Sin(_modifiedAngle) + this.transform.position.y * Mathf.Cos(_modifiedAngle);
                if (_modifiedAngle > _angle + 360)
                    break;
            }
            modifiedDegree = _modifiedAngle;
            float _posDifference = _angle - _modifiedAngle;
            if (_posDifference < 0)
                _posDifference = -_posDifference;

            _modifiedAngle = _angle - 10;

            _direction.x = this.transform.position.x * Mathf.Cos(_modifiedAngle) - this.transform.position.y * Mathf.Sin(_modifiedAngle);
            _direction.y = this.transform.position.x * Mathf.Sin(_modifiedAngle) + this.transform.position.y * Mathf.Cos(_modifiedAngle);

            while (isObstacle(_direction) == true)
            {
                //장애물 있는 경우 10도씩 내려가며 장애물 있는지 판정
                _modifiedAngle -= 10;
                _direction.x = this.transform.position.x * Mathf.Cos(_modifiedAngle) - this.transform.position.y * Mathf.Sin(_modifiedAngle);
                _direction.y = this.transform.position.x * Mathf.Sin(_modifiedAngle) + this.transform.position.y * Mathf.Cos(_modifiedAngle);
                if (_modifiedAngle < _angle - 360)
                    break;

            }
            float _negDifference = _angle - modifiedDegree;
            if (_negDifference < 0)
                _negDifference = -_negDifference;

            if(_posDifference > _negDifference)
            {
                modifiedDegree = _modifiedAngle;
            }
            isAvoid = true;
        }

        if (isObstacle(transform.up) == true)
        {
            //장애물 있음
            //여기서 일정각도로 회피
            rotateShip(modifiedDegree);
        }
        else
            rotateShip();
    }

    private void moveShip()
    {
        //raycast를 통해 플레이어와 해적선 사이에 장애물 있는지 판정, 회피 방향/플레이어 방향으로 회전
        if(isObstacle() == true)
        {
            avoidObstacle();
        }
        else
        {
            rotateShip();
        }

        if((playerShip.transform.position - this.transform.position).magnitude <= detectRange)
        {
            this.transform.position += transform.up * speed * Time.deltaTime;
        }
    }
    //현재 위치에서 플레이어 방향까지 장애물이 있는지 판정
    private bool isObstacle()
    {
        RaycastHit2D raycast = 
            Physics2D.Raycast(shootPosition.transform.position, (playerShip.transform.position - this.transform.position));
        raycast = Physics2D.BoxCast(shootPosition.transform.position, this.gameObject.transform.lossyScale, this.transform.rotation.eulerAngles.z, (playerShip.transform.position - this.transform.position));

        if(raycast.collider != null)
        {
            if ((raycast.collider.tag == "Player")||(raycast.collider.tag == "Typoon"))
            {
                return false;
            }
            else
            {
                isAvoid = false;
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    //특정 방향으로 바라봤을 때 장애물이 있는지 판정
    private bool isObstacle(Vector3 direction)
    {
        RaycastHit2D raycast =
            Physics2D.Raycast(this.transform.position, direction, (playerShip.transform.position - this.transform.position).magnitude);

        raycast = Physics2D.BoxCast(this.transform.position, this.gameObject.transform.lossyScale, this.transform.rotation.eulerAngles.z, direction, (playerShip.transform.position - this.transform.position).magnitude);
        if (raycast.collider != null)
        {
            if ((raycast.collider.tag == "Player") || (raycast.collider.tag == "Typoon"))
            {
                return false;
            }
            else
                return true;
        }
        else
        {
            return false;
        }
    }
    private void selfDestroy()
    {
        curTime += Time.deltaTime;
        if (curTime > selfDestroyTime)
            Destroy(this.gameObject);
    }

    private void shipPlunder()
    {
        if(GameManager.Instance.Food >= 30)
        {
            GameManager.Instance.Food -= 30;
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.Instance.NowGameEnding = GameEnding.PIRATE;
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            shipPlunder();
        }
    }
}
