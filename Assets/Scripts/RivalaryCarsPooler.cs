using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RivalaryCarsPooler : MonoBehaviour
{
    public static RivalaryCarsPooler intance;
    public List<GameObject> RivalaryCarsList = new List<GameObject>();
    private List<GameObject> RivalaryCarsPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (intance == null) intance = this;
        if (PlayerPrefs.GetString("GameMode") == GameConstants.Hard_SceneName)
        {
            InstantiateNewCarsInPool_Hard();
        }
        else
        {
            InstantiateNewCarsInPool_Easy();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetRandomRivalryCar()
    {
        bool Flag = false;
        GameObject RivalaryCar = RivalaryCarsPool[0];
        for (int i = 0; i < RivalaryCarsPool.Count; i++)
        {
            if (!RivalaryCarsPool[i].activeInHierarchy)
            {
                RivalaryCar = RivalaryCarsPool[i];
                Flag = true;
                break;
            }
        }
        if (Flag) return RivalaryCar;
        else
        {
            GameObject NewRivalryCar = Instantiate(RivalaryCarsList[Random.Range(0, RivalaryCarsList.Count)]);
            NewRivalryCar.SetActive(false);
            RivalaryCarsPool.Add(NewRivalryCar);
            return NewRivalryCar;
        }
    }
    private void InstantiateNewCarsInPool_Hard()
    {
        foreach (var Car in RivalaryCarsList)
        {
            GameObject RivalryCar = Instantiate(Car);
            RivalryCar.SetActive(false);
            RivalaryCarsPool.Add(RivalryCar);
        }
    }
    private void InstantiateNewCarsInPool_Easy()
    {
        foreach (var Car in RivalaryCarsList)
        {
            GameObject RivalryCar = Instantiate(Car);
            RivalryCar.SetActive(false);
            RivalaryCarsPool.Add(RivalryCar);
        }
    }
}


