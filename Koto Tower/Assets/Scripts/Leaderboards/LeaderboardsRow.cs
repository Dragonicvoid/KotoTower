using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardsRow : MonoBehaviour
{
    [SerializeField] Text peringkatText = null;
    [SerializeField] Text namaText = null;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text tanggalText = null;

    // change text row
    public void changeText(string peringkat, string nama, string score, string tanggal)
    {
        peringkatText.text = peringkat;
        namaText.text = nama;
        scoreText.text = score;
        tanggalText.text = tanggal;
    }
}
