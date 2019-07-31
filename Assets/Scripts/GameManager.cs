using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance{get{ return instance;}}

    [SerializeField] private int endGenerateCount = 3;
    private int[] generateCount = new int[4]; // 각 방향 생성 개수, 위쪽 오른쪽 아래쪽 왼쪽 순서

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getGenerateCount(int index)
    {
        return generateCount[index];
    }

    public void addGenerateCount(int index, int value)
    {
        generateCount[index] += value;
    }

    public bool isGenerateCountEnd()
    {
        if (generateCount[0] > endGenerateCount || generateCount[1] > endGenerateCount ||
                generateCount[2] > endGenerateCount || generateCount[3] > endGenerateCount)
            return true;
        return false;
    }
}
