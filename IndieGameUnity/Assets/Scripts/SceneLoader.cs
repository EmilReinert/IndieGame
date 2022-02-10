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

        if (other.gameObject == player)
            NextScene();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SceneManager.LoadScene(5);
    }

    public void NextScene()
    {
        StartCoroutine(Next());
    }
    public void Startt()
    {
        StartCoroutine(Next()); 
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
