using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System;

public class GetDataTest : MonoBehaviour
{
    long findId;
    string description = "DEFAULT";
    Text text;

    bool isDoneScanData = false;

    private void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (GameManager.instance.hasLogin)
            text.text = "Logged";
        else
            text.text = "not logged";

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
        getDataTest();
    }

    public void stopAllGetData()
    {
        StopAllCoroutines();
        isDoneScanData = false;
    }

    void getDataTest()
    {
        // WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/testGetData/testGetData.php");
        // yield return www;

        // if (www.text == null || "".Equals(www.text))
        // {
        //     description = "error : " + "\n server is offline";
        //     isDoneScanData = true;
        // }
        if (true)
        {
            string[] data = {"Null", "Null", "Null"};
            description = "Deskripsi : " + data[1] + " \n" + "Tanggal : " + data[2];
            isDoneScanData = true;
        }
    }

    IEnumerator testHashing(string s)
    {
        yield return null;
        SHA1 hash = SHA1.Create();
        byte[] data1 = hash.ComputeHash(Encoding.UTF8.GetBytes(s));

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < data1.Length; i++)
            sb.Append(data1[i].ToString("x2"));

        description = sb.ToString();
        isDoneScanData = true;
    }
}
