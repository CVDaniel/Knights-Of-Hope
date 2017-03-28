using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using Facebook.MiniJSON;

public class FBManager : MonoBehaviour {

    //public GameObject FBLoginButton;
    //public GameObject FBLoggedInCanvas;
    //public GameObject FBUsernameText;
    //public GameObject FBProfilePic;

    void Awake()
    {
        FB.Init(OnFBInitComplete, OnHideUnity);

    }

    void OnFBInitComplete()
    {
        checkIfLoggedIn();
        //DisplayMenu(FB.IsLoggedIn);
    }

    void checkIfLoggedIn()
    {
        if(FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();

        permissions.Add("public_profile");
        LoadingWheel.StarAnimation();

        FB.LogInWithReadPermissions(permissions, OnLoginCompleted);
    }

    public void FBLogout()
    {
        FB.LogOut();
        //FBLoggedInCanvas.SetActive(false);
        //FBLoginButton.SetActive(true);
    }

    void OnLoginCompleted(IResult result)
    {
        LoadingWheel.StopAnimation();
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            checkIfLoggedIn();
            Debug.Log("LOGGED IN");
            //DisplayMenu(FB.IsLoggedIn);
            FB.API("/me?fields=name,email", HttpMethod.GET, OnGetFBInfoCompleted);
        }
    }

    void OnGetFBInfoCompleted(IResult result)
    {
        Debug.Log(result.ResultDictionary.ToJson().ToString());

        string request = "{"
                            + "\"accountType\" : \"Facebook\","
                            + "\"data\" : "
                            + "{"
                            + "\"fbid\":\"" + result.ResultDictionary["id"] + "\""
                            + "}" 
                        + "}";

        Debug.Log("request JSON: " + request);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        //byte[] b = System.Text.Encoding.UTF8.GetBytes();
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(request.ToCharArray());

        WWW apiCall = new WWW(URLStorage.ARXOFT_USERS_URL, pData, headers);
        StartCoroutine(WaitForResponse(apiCall));
    }

    private IEnumerator WaitForResponse(WWW response)
    {
        yield return response;

        if (response.error != null)
        {
            Debug.Log("ERROR AT REQUEST! Error: " + response.error);
        }
        else
        {
            Debug.Log("REQUEST SUCCESSFUL ! DATA: " + response.text);
        }
    }

    void DisplayMenu(bool isLoggedIn)
    {
        //FBLoginButton.SetActive(!isLoggedIn);
        //FBLoggedInCanvas.SetActive(isLoggedIn);

        if(isLoggedIn)
        {
            FB.API("/me?fields=first_name",HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture/?type=square&height=128&witdh=128", HttpMethod.GET, DisplayProfilePicture);
        }
    }

    void DisplayUsername(IResult result)
    {
        //Text UserName = FBUsernameText.GetComponent<Text>();
        
        if(result.Error == null)
        {
            //UserName.text = result.ResultDictionary["first_name"].ToString();
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePicture(IGraphResult result)
    {
        if(result.Texture != null)
        {
            //Image ProfilePic = FBProfilePic.GetComponent<Image>();
            //ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }
}
