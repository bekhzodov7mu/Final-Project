﻿using UnityEngine;
using System.Collections;

public class LittleEnemy : Enemy
{
    [Header("Buttet and barrel")]
    //[SerializeField] private Transform bullet = null;
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

    //Object Pooler
    ObjectPooler objectPooler;

    private void Start()
    {
        _health = Random.Range(10, 20);

        StartCoroutine(Reload());

        objectPooler = ObjectPooler.objectPoolerInstance;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            GameManager.gameManagerInstance.CurrentScore += 5;
            objectPooler.SpawnFromPool("LittleEnemyExplosion", transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        if (_reloaded)
        {
            objectPooler.SpawnFromPool("LittleEnemyBullet", barrel.transform.position, barrel.transform.rotation);
            //Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(2, 5);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
