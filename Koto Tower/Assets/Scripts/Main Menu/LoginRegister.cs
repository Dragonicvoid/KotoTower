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
    [SerializeField] Toggle rememberMeLogin = null;

    // register field
    [SerializeField] InputField usernameRegisterField = null;
    [SerializeField] InputField passwordRegisterField = null;
    [SerializeField] InputField confirmPasswordRegisterField = null;

    // login and register box
    [SerializeField] GameObject loginBox = null;
    [SerializeField] GameObject registerBox = null;

    //filter
    [SerializeField] GameObject filter = null;
    [SerializeField] GameObject filterLoginRegister = null;

    // Text login dan register akun
    [SerializeField] Text loginText = null;
    [SerializeField] Text loginErrorText = null;
    [SerializeField] Text registerErrorText = null;
    [SerializeField] Text loginRegisterLoadingText = null;

    // cancel button when loading
    [SerializeField] GameObject loadingBox = null;
    [SerializeField] Button cancelButton = null;

    // button leaderboards
    [SerializeField] Button leaderboards = null;
    // button new Game
    [SerializeField] Button newGameButton = null;
    // button Continue
    [SerializeField] Button continueButton = null;
    // button Rangkuman
    [SerializeField] Button rangkumanButton = null;

    // is sending the input field done?
    bool doneSending;
    // error text;
    string errorText;

    private void Start()
    {
        doneSending = false;

        if (PlayerPrefs.HasKey("save") && !"".Equals(GameManager.instance.saveFile.username) && !GameManager.instance.hasLogin)
            loginFromRememberMe();
    }

    // check validity for login
    bool checkLogin()
    {
        // if any field is empty
        if ("".Equals(usernameLoginField.text) || "".Equals(passwordLoginField.text))
        {
            errorText = "Seluruh field harus diisi";
            return false;
        }

        // if any field is not alpha numeric
        if (!isAlphaNumer(usernameLoginField.text) || !isAlphaNumer(passwordLoginField.text))
        {
            errorText = "Username dan password akun harus berupa huruf atau/dan angka";
            return false;
        }

        // if username is shorter than 3
        if (usernameLoginField.text.Length < 3 || usernameLoginField.text.Length > 20)
        {
            errorText = "Panjang username harus diantara 3 sampai 20 huruf";
            return false;
        }
            
        // if password length is shorter than 3
        if (passwordLoginField.text.Length < 3 || passwordLoginField.text.Length > 20)
        {
            errorText = "Panjang password harus diantara 3 sampai 20 huruf";
            return false;
        }

        return true;
    }

    // check validity for register
    bool checkRegister()
    {
        errorText = "";
        // if any field is empty
        if ("".Equals(usernameRegisterField.text) || "".Equals(passwordRegisterField.text) || "".Equals(confirmPasswordRegisterField.text))
        {
            errorText = "Seluruh field harus diisi";
            return false;
        }
            
        // if any field is not alpha numeric
        if (!isAlphaNumer(usernameRegisterField.text) || !isAlphaNumer(passwordRegisterField.text) || !isAlphaNumer(confirmPasswordRegisterField.text))
        {
            errorText = "Username dan password akun harus berupa huruf atau/dan angka";
            return false;
        }

        // if username is shorter than 3
        if (usernameRegisterField.text.Length < 3 || usernameRegisterField.text.Length > 20)
        {
            errorText = "Panjang username harus diantara 3 sampai 20 huruf";
            return false;
        }

        // if password length is shorter than 3
        if (passwordRegisterField.text.Length < 3 || passwordRegisterField.text.Length > 20)
        {
            errorText = "Panjang password harus diantara 3 sampai 20 huruf";
            return false;
        }

        // if confimation is not the same
        if (!confirmPasswordRegisterField.text.Equals(passwordRegisterField.text))
        {
            errorText = "Password dan konfirmasi password tidak sama";
            return false;
        }

        return true;
    }

    // login from remember me situation
    public void loginFromRememberMe()
    {
        doneSending = false;
        StartCoroutine(loginFromRememberMeCheckAndSend());
    }

    // couroutine for login from remember me
    IEnumerator loginFromRememberMeCheckAndSend()
    {
        yield return null;
        loginRegisterLoadingText.text = "LOGIN AKUN";
        loadingBox.SetActive(true);
        filterLoginRegister.SetActive(true);
        errorText = "";
        WWWForm form = new WWWForm();
        form.AddField("username", GameManager.instance.saveFile.username);
        form.AddField("password", GameManager.instance.saveFile.password);

        form.AddField("mySQLPassword", GameManager.instance.getMySQLPassword());
        
        WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/login.php", form);
        yield return www;

        if (www.text == null || "".Equals(www.text))
            errorText = "Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi";
        else if (www.text[0] == '0')
        {
            cancelButton.interactable = false;
            string[] data = www.text.Split('\t');
            GameManager.instance.userId = int.Parse(data[1]);
            GameManager.instance.hasLogin = true;
            changeText(GameManager.instance.hasLogin);
            leaderboards.interactable = GameManager.instance.saveFile.levelDone != 0;
            newGameButton.interactable = true;
            continueButton.interactable = GameManager.instance.saveFile.levelDone != 0;
            rangkumanButton.interactable = GameManager.instance.saveFile.levelDone != 0;
            cancelButton.interactable = true;
        }
        else
            errorText = "Error ditemukan : " + www.text.Substring(3, www.text.Length - 3);

        Debug.Log(errorText);
        loadingBox.SetActive(false);
        filterLoginRegister.SetActive(false);
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
            loginRegisterLoadingText.text = "LOGIN AKUN";
            loadingBox.SetActive(true);
            filterLoginRegister.SetActive(true);
            errorText = "";
            WWWForm form = new WWWForm();
            form.AddField("username", usernameLoginField.text);
            form.AddField("password", passwordLoginField.text);

            form.AddField("mySQLPassword", GameManager.instance.getMySQLPassword());

            WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/login.php", form);
            yield return www;

            if (www.text == null || "".Equals(www.text))
                errorText = "Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi";
            else if (www.text[0] == '0')
            {
                cancelButton.interactable = false;
                string[] data = www.text.Split('\t');
                GameManager.instance.userId = int.Parse(data[1]);
                GameManager.instance.hasLogin = true;

                if (rememberMeLogin.isOn)
                {
                    if (!PlayerPrefs.HasKey("save"))
                        GameManager.instance.saveFile = new SaveState(false);

                    GameManager.instance.saveFile.username = usernameLoginField.text;
                    GameManager.instance.saveFile.setPassword(passwordLoginField.text);
                    SaveManager.instance.saveAndUpdate();
                }
                else
                {
                    GameManager.instance.saveFile.username = "";
                    GameManager.instance.saveFile.setPassword("");
                }

                changeText(GameManager.instance.hasLogin);
                leaderboards.interactable = GameManager.instance.saveFile.levelDone != 0;
                newGameButton.interactable = true;
                continueButton.interactable = GameManager.instance.saveFile.levelDone != 0;
                rangkumanButton.interactable = GameManager.instance.saveFile.levelDone != 0;
                resetFields();
                loginBox.SetActive(false);
                registerBox.SetActive(false);
                filter.SetActive(false);
                cancelButton.interactable = true;
            }
            else
                errorText = "Error ditemukan : " + www.text.Substring(3, www.text.Length - 3);

            loginErrorText.text = errorText;
            loadingBox.SetActive(false);
            filterLoginRegister.SetActive(false);
        }
        else
            loginErrorText.text = errorText;
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
            loginRegisterLoadingText.text = "MEMBUAT AKUN BARU";
            loadingBox.SetActive(true);
            filterLoginRegister.SetActive(true);
            errorText = "";
            WWWForm form = new WWWForm();
            form.AddField("username", usernameRegisterField.text);
            form.AddField("password", passwordRegisterField.text);

            form.AddField("mySQLPassword", GameManager.instance.getMySQLPassword());

            WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/register.php", form);
            yield return www;

            if (www.text == null || "".Equals(www.text))
                errorText =  "Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi";
            else if (www.text[0] == '0')
            {
                cancelButton.interactable = false;
                string[] data = www.text.Split('\t');
                GameManager.instance.userId = int.Parse(data[1]);
                GameManager.instance.hasLogin = true;
                changeText(GameManager.instance.hasLogin);
                leaderboards.interactable = GameManager.instance.saveFile.levelDone != 0;
                newGameButton.interactable = true;
                continueButton.interactable = GameManager.instance.saveFile.levelDone != 0;
                rangkumanButton.interactable = GameManager.instance.saveFile.levelDone != 0;
                resetFields();
                registerBox.SetActive(false);
                loginBox.SetActive(false);
                filter.SetActive(false);
                cancelButton.interactable = true;
            }
            else
                errorText = "Error ditemukan : " + www.text.Substring(3, www.text.Length - 3);

            registerErrorText.text = errorText;
            loadingBox.SetActive(false);
            filterLoginRegister.SetActive(false);
        }
        else
            registerErrorText.text = errorText;

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

        // error text
        loginErrorText.text = "";
        registerErrorText.text = "";
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

    // change text based of login condition
    public void changeText(bool hasLogin)
    {
        if (hasLogin)
            loginText.text = "Logout Akun";
        else
            loginText.text = "Login atau Daftar Akun";
    }

    // cancel all couroutine
    public void cancelLoginOrRegister()
    {
        StopAllCoroutines();
        loadingBox.SetActive(false);
        filterLoginRegister.SetActive(false);
    }
}
