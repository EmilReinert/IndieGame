using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject == player)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
