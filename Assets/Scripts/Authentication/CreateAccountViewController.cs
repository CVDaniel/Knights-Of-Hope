using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountViewController : MonoBehaviour {

    public GameObject Email;
    public GameObject Password;
    public GameObject ConfirmPassword;
	
    public void OnCreateAccount()
    {
        string email = (Email.transform.Find("FieldInput").GetComponent<InputField>()).text;
        string password = (Password.transform.Find("FieldInput").GetComponent<InputField>()).text;
        string confirmPassword = (ConfirmPassword.transform.Find("FieldInput/Text").GetComponent<Text>()).text;

        string salt = SaltGenerator.instance().generateSalt();
        string hashedPassword = HashTool.EncodePassword(password, salt);

        CreateAccountRequest request = new CreateAccountRequest()
                                    .WithAccountType("Arxoft")
                                    .WithEmail(email)
                                    .WithPassword(hashedPassword)
                                    .WithSalt(salt);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        byte[] data = System.Text.Encoding.ASCII.GetBytes(request.ToJson().ToCharArray());

        WWW apiCall = new WWW(URLStorage.ARXOFT_USERS_URL, data, headers);
        LoadingWheel.StarAnimation();
        StartCoroutine(OnCreateAccountResult(apiCall));
    }

    IEnumerator OnCreateAccountResult(WWW response)
    {
        yield return response;

        LoadingWheel.StopAnimation();
        if (response.error != null)
        {
            Debug.Log("ERROR AT REQUEST! Error: " + response.error);
        }
        else
        {
            Debug.Log("REQUEST SUCCESSFUL ! DATA: " + response.text);
        }
    }
}
