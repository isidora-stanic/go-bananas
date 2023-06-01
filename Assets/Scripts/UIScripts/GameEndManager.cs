using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private AllMenusManager allMenusManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsDead())
        {
            allMenusManager.LoadLoseScreen();
        }
        else if (AllEnemiesAreDead())
        {
            allMenusManager.LoadWinScreen();
        }
    }

    public bool AllEnemiesAreDead()
    {
        return enemies.ToList().All(x => x.activeSelf == false);
    }

    public bool PlayerIsDead()
    {
        return player.activeSelf == false;
    }
}
