using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControllerTesting : MonoBehaviour
{
    GameObject healthBar;
    SpriteRenderer backgroundRenderer;
    SpriteRenderer healthRenderer;
    float waitTimer;
    float timer;
    int colorId;

    //initializatio
    private void Start()
    {
        colorId = Shader.PropertyToID("_Color");
        timer = 0.2f;
        waitTimer = 0.1f;
        healthBar = gameObject.transform.GetChild(0).gameObject;
        backgroundRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        healthRenderer = healthBar.GetComponent<SpriteRenderer>();

        // make the color transparent
        var tempColorHealthBar = healthRenderer.color;
        tempColorHealthBar.a = 0.5f;
        healthRenderer.color = tempColorHealthBar;

        var tempColorBackground = backgroundRenderer.color;
        tempColorBackground.a = 0.5f;
        backgroundRenderer.color = tempColorBackground;
    }

    // timer is ticking
    private void Update()
    {
        checkGotDamage();
    }

    // checking if there is damage, if yes show the 
    void checkGotDamage()
    {
        if (timer < waitTimer)
            timer += Time.deltaTime;

        var tempColorHealthBar = healthRenderer.color;
        tempColorHealthBar.a = 0.5f + (0.5f - (deltaTime(timer, waitTimer) * 0.5f) );
        healthRenderer.color = tempColorHealthBar;
    }

    // count the delta time between timer and wait time
    float deltaTime(float timer, float waitTime)
    {
        if (timer == 0f)
            return 0f;
        else if (timer >= waitTime)
            return 1f;
        else
            return timer / waitTime;
    }

    // controller for parent 
    public void gotDamaged(float currHealth, float maxHealth)
    {
        timer = 0f;
        Vector3 localScale = healthBar.transform.localScale;
        healthBar.transform.localScale = new Vector3(currHealth / maxHealth, localScale.y, localScale.z);
    }
}
