using System;
using UnityEngine;

[Serializable]
public class AuthenticationRequest
{
    public string accountType;

    public AuthenticationRequest WithAccountType(string type)
    {
        accountType = type;
        return this;
    }

    public string GetAccountType()
    {
        return accountType;
    }
}

[Serializable]
public class CreateAccountRequest : AuthenticationRequest
{
    public CreateAccountRequestData data;

    public CreateAccountRequest()
    {
        data = new CreateAccountRequestData();
    }

    public new CreateAccountRequest WithAccountType(string accountTypeValue)
    {
        base.WithAccountType(accountTypeValue);
        return this;
    }

    public CreateAccountRequest WithEmail(string emailValue)
    {
        data.SetEmail(emailValue);
        return this;
    }

    public CreateAccountRequest WithPassword(string passwordValue)
    {
        data.SetPassword(passwordValue);
        return this;
    }

    public CreateAccountRequest WithSalt(string saltValue)
    {
        data.SetSalt(saltValue);
        return this;
    }

    public string GetEmail()
    {
        return data.GetEmail();
    }

    public string GetPassword()
    {
        return data.GetPassword();
    }

    public string GetSalt()
    {
        return data.GetSalt();
    }
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static CreateAccountRequest FromJson(string json)
    {
        return JsonUtility.FromJson<CreateAccountRequest>(json);
    }
}

[Serializable]
public class CreateAccountRequestData
{
    public string email;
    public string password;
    public string salt;

    public void SetEmail(string emailValue)
    {
        email = emailValue;
    }

    public void SetPassword(string passwordValue)
    {
        password = passwordValue;
    }

    public void SetSalt(string saltValue)
    {
        salt = saltValue;
    }

    public string GetEmail()
    {
        return email;
    }

    public string GetPassword()
    {
        return password;
    }

    public string GetSalt()
    {
        return salt;
    }
}

[Serializable]
public class ArxoftLoginRequest : AuthenticationRequest
{
    public ArxoftLoginRequestData data;

    public ArxoftLoginRequest()
    {
        data = new ArxoftLoginRequestData();
    }

    public ArxoftLoginRequest WithEmail(string email)
    {
        data.SetEmail(email);
        return this;
    }

    public ArxoftLoginRequest WithPassword(string password)
    {
        data.SetPassword(password);
        return this;
    }
}

[Serializable]
public class ArxoftLoginRequestData
{
    public string email;
    public string password;

    public void SetEmail(string emailValue)
    {
        email = emailValue;
    }

    public void SetPassword(string passwordValue)
    {
        password = passwordValue;
    }

    public string GetEmail()
    {
        return email;
    }

    public string GetPassword()
    {
        return password;
    }
}

[Serializable]
public class ArxoftLoginSaltRequest
{
    public ArxoftLoginSaltRequestData data;

    public ArxoftLoginSaltRequest()
    {
        data = new ArxoftLoginSaltRequestData();
    }

    public ArxoftLoginSaltRequest WithUserEmail(string email)
    {
        data.SetEmail(email);
        return this;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class ArxoftLoginSaltRequestData
{
    public string email;

    public void SetEmail(string emailValue)
    {
        email = emailValue;
    }

    public string GetEmail()
    {
        return email;
    }
}