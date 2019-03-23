﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

	public GameObject[] cowPrefabs = null;

	public float cowSpawnTimer = 0f;

	public float range;

    // Start is called before the first frame update
    void Start()
    {
    	cowPrefabs = Resources.LoadAll("Prefab/Cows", typeof(GameObject)).Cast<GameObject>().ToArray();
        for (int i = 0; i < Random.Range(25, 40); ++i) {
        	Instantiate(
        		cowPrefabs[Random.Range(0, cowPrefabs.Length)],
        		new Vector3(
        			this.transform.position.x + Random.Range(-range, range), 
        			this.transform.position.y, 
        			this.transform.position.z + Random.Range(-range, range)
        		), 
        		Quaternion.identity
        	);
        }
        cowSpawnTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        cowSpawnTimer -=Time.deltaTime;
        if (cowSpawnTimer <= 0f) {
        	cowSpawnTimer = 5f;
        	Instantiate(
        		cowPrefabs[Random.Range(0, cowPrefabs.Length)], new Vector3(
        			this.transform.position.x + Random.Range(-range, range), 
        			this.transform.position.y, 
        			this.transform.position.z + Random.Range(-range, range)
        		),
        		Quaternion.identity
        	);
        }
    }
}