using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDataTest : MonoBehaviour
{
    long findId;
    string description = "DEFAULT";
    Text text;

    bool isDoneScanData = false;

    private void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        StartCoroutine(getDataTest());
    }

    private void Update()
    {
        if (isDoneScanData)
        {
            text.text = description;
            isDoneScanData = false;
        }
    }

    private void resetField()
    {
        description = "DEFAULT";
    }

    public void setId(long id)
    {
        findId = id;
        isDoneScanData = false;
    }

    public void getData()
    {
        isDoneScanData = false;
        StartCoroutine(getDataTest());
    }

    public void stopAllGetData()
    {
        StopAllCoroutines();
        isDoneScanData = false;
    }

    IEnumerator getDataTest()
    {
        WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/testGetData/testGetData.php");
        yield return www;

        if (www.text == null || "".Equals(www.text))
        {
            description = "error : " + "\n server is offline";
            isDoneScanData = true;
        }
        else if (www.text[0] == '0')
        {
            string[] data = www.text.Split('\t');
            description = "Deskripsi : " + data[1] + " \n" + "Tanggal : " + data[2];
            isDoneScanData = true;
        }
        else
        {
            description = "FOUND ERROR NO " + www.text;
            isDoneScanData = true;
        }
    }
}
