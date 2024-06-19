using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    Sensor sensor;

    [SerializeField]
     SpriteRenderer doorClosed;
    [SerializeField]
    Sprite doorOpened;

    public bool isOpened = false;

    [SerializeField]
    string sceneName;

    public void ChangeSprite()
    {
        doorClosed.sprite = doorOpened;
    }

    private void Update()
    {
        if (!isOpened && sensor.sensorState)
        {
            ChangeSprite();
            isOpened = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpened)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("next level");
        }
    }
}
