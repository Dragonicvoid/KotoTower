using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAddedBehaviour : MonoBehaviour
{
    // audioClip
    [SerializeField] List<AudioClip> listOfAudio = null;

    // variable
    Vector3 currPosition;
    AudioSource audioSource;
    TextMesh textMesh;
    bool activated;
    float floatingTimer;
    float timer;

    // animation
    private void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            float delta = deltaTime(timer, floatingTimer);

            this.gameObject.transform.position = new Vector3(currPosition.x, currPosition.y + Mathf.Pow(delta, 2) + currPosition.z);

            if (timer >= floatingTimer)
                Destroy(this.gameObject);
        }
    }

    // counts the delta time
    float deltaTime(float value, float max)
    {
        if (value == 0)
            return 0;
        else if (value < max)
            return (value / max);
        else
            return 1f;
    }

    // change text and play sound and other when the money is added
    public void activatePlus(int money, Vector3 position)
    {
        //initialization variables
        audioSource = this.gameObject.GetComponent<AudioSource>();
        textMesh = this.gameObject.GetComponentInChildren<TextMesh>();
        textMesh.gameObject.GetComponent<MeshRenderer>().sortingOrder = 10;
        audioSource.clip = listOfAudio[0];
        textMesh.color = Color.green;
        activated = false;
        timer = 0f;
        floatingTimer = 1f;

        // change the text, and play sound
        this.gameObject.transform.position = position;
        currPosition = position;
        activated = true;
        textMesh.text = "+" + money.ToString();
        audioSource.Stop();
        audioSource.Play();
    }

    // change text and play sound and other when the money is subtracted
    public void activateMinus(int money, Vector3 position)
    {
        //initialization variables
        audioSource = this.gameObject.GetComponent<AudioSource>();
        textMesh = this.gameObject.GetComponentInChildren<TextMesh>();
        textMesh.gameObject.GetComponent<MeshRenderer>().sortingOrder = 10;
        audioSource.clip = listOfAudio[1];
        textMesh.color = Color.red;
        activated = false;
        timer = 0f;
        floatingTimer = 1f;

        // change the text, and play sound
        this.gameObject.transform.position = position;
        currPosition = position;
        activated = true;
        textMesh.text = "-" + money.ToString();
        audioSource.Stop();
        audioSource.Play();
    }

    // change text and play sound and other when the money is refunded
    public void activateRefund(int money, Vector3 position)
    {
        //initialization variables
        audioSource = this.gameObject.GetComponent<AudioSource>();
        textMesh = this.gameObject.GetComponentInChildren<TextMesh>();
        textMesh.gameObject.GetComponent<MeshRenderer>().sortingOrder = 10;
        audioSource.clip = listOfAudio[2];
        textMesh.color = Color.green;
        activated = false;
        timer = 0f;
        floatingTimer = 1f;

        // change the text, and play sound
        this.gameObject.transform.position = position;
        currPosition = position;
        activated = true;
        textMesh.text = "+" + money.ToString();
        audioSource.Stop();
        audioSource.Play();
    }
}
