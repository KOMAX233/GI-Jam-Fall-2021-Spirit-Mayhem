using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loneliness : MonoBehaviour
{
    public Player player;
    public float maxLoneliness;
    public int spirits;
    public accompanimentBar accompanimentbar;
    public float accompaniment => player.allSpirits.Count * 50 + player.MyHealth.damageTaken;

    void Start() {
        maxLoneliness = player.MyHealth.maxHealth;
        accompanimentbar.setInitialAccompaniment(accompaniment);
        accompanimentbar.setMaxAccompaniment(maxLoneliness);
    }

    void Update() {
        accompanimentbar.setAccompaniment(accompaniment);
        Debug.Log(accompaniment);
    }
    
}