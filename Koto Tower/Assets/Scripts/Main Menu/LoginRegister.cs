using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class LoginRegister : MonoBehaviour
{
    // login field
    [SerializeField] InputField usernameLoginField = null;
    [SerializeField] InputField passwordLoginField = null;

    // register field
    [SerializeField] InputField usernameRegisterField = null;
    [SerializeField] InputField passwordRegisterField = null;
    [SerializeField] InputField confirmPasswordRegisterField = null;
    
    // is sending the input field done?
    bool doneSending;

    private void Start()
    {
        doneSending = false;
    }

    // check validity for login
    bool checkLogin()
    {
        // if any field is empty
        if ("".Equals(usernameLoginField.text) || "".Equals(passwordLoginField.text))
            return false;

        // if any field is not alpha numeric
        if (!isAlphaNumer(usernameLoginField.text) || !isAlphaNumer(passwordLoginField.text))
            return false;

        // if username is shorter than 3
        if (usernameLoginField.text.Length < 3)
            return false;

        // if password length is shorter than 3
        if (passwordLoginField.text.Length < 3)
            return false;

        return true;
    }

    // check validity for register
    bool checkRegister()
    {
        // if any field is empty
        if ("".Equals(usernameRegisterField.text) || "".Equals(passwordRegisterField.text) || "".Equals(confirmPasswordRegisterField.text))
        {
            Debug.Log("empty");
            return false;
        }
            
        // if any field is not alpha numeric
        if (!isAlphaNumer(usernameRegisterField.text) || !isAlphaNumer(passwordRegisterField.text) || !isAlphaNumer(confirmPasswordRegisterField.text))
        {
            Debug.Log("alpha numeric");
            return false;
        }

        // if username is shorter than 3
        if (usernameRegisterField.text.Length < 3)
        {
            Debug.Log("lenght user");
            return false;
        }

        // if password length is shorter than 3
        if (passwordRegisterField.text.Length < 3)
        {
            Debug.Log("length pass");
            return false;
        }

        // if confimation is not the same
        if (!confirmPasswordRegisterField.text.Equals(passwordRegisterField.text))
        {
            Debug.Log("Not same");
            return false;
        }

        Debug.Log("here");
        return true;
    }

    // login
    public void login()
    {
        doneSending = false;
        StartCoroutine(loginCheckAndSend());
    }

    // couroutine for login
    IEnumerator loginCheckAndSend()
    {
        yield return null;
        // if the requirement is valid
        if (checkLogin())
        {
            Debug.Log("halo");
            WWWForm form = new WWWForm();
            form.AddField("username", usernameLoginField.text);
            Debug.Log(usernameLoginField.text);

            string hashedPassword = hashing(passwordLoginField.text);
            form.AddField("password", hashedPassword);
            Debug.Log(hashedPassword);

            WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/login.php", form);
            yield return www;

            if (www.text == null || "".Equals(www.text))
            {
                Debug.Log("error : " + "\n server is offline");
            }
            else if (www.text[0] == '0')
            {
                string[] data = www.text.Split('\t');
                Debug.Log(data[1]);
                GameManager.instance.userId = int.Parse(data[1]);
                GameManager.instance.hasLogin = true;
            }
            else
            {
                Debug.Log("FOUND ERROR NO " + www.text);
            }
        }
    }

    // register
    public void register()
    {
        doneSending = false;
        StartCoroutine(registerCheckAndSend());
    }

    // couroutine for register
    IEnumerator registerCheckAndSend()
    {
        yield return null;
        // if requirement is valid
        if (checkRegister())
        {
            WWWForm form = new WWWForm();
            form.AddField("username", usernameRegisterField.text);
            Debug.Log(usernameRegisterField.text);

            string hashedPassword = hashing(passwordRegisterField.text);
            form.AddField("password", hashedPassword);
            Debug.Log(hashedPassword);

            string hashedUsername = hashing(usernameRegisterField.text);
            form.AddField("userKey", hashedUsername);
            Debug.Log(hashedUsername);

            WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/register.php", form);
            yield return www;

            if (www.text == null || "".Equals(www.text))
            {
                Debug.Log("error : " + "\n server is offline");
            }
            else if (www.text[0] == '0')
            {
                string[] data = www.text.Split('\t');
                Debug.Log(data[1]);
                GameManager.instance.userId = int.Parse(data[1]);
                GameManager.instance.hasLogin = true;
            }
            else
            {
                Debug.Log("FOUND ERROR NO " + www.text);
            }
        }
    }

    //check words
    bool isAlphaNumer(string s)
    {
        return !System.Text.RegularExpressions.Regex.IsMatch(s, "[^A-Za-z0-9 ]");
    }

    // reset all login and register field
    public void resetFields()
    {
        // login field
        usernameLoginField.text = "";
        passwordLoginField.text = "";

        // register field
        usernameRegisterField.text = "";
        passwordRegisterField.text = "";
        confirmPasswordRegisterField.text = "";
    }

    // hashing
    string hashing(string s)
    {
        string finalResult = "";

        SHA1 hash = SHA1.Create();
        byte[] data1 = hash.ComputeHash(Encoding.UTF8.GetBytes(s));

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < data1.Length; i++)
            sb.Append(data1[i].ToString("x2"));

        finalResult = sb.ToString();

        return finalResult;
    }
}
