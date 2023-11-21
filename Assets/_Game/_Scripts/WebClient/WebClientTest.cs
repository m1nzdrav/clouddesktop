using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebClientTest : MonoBehaviour
{
    private void Awake()
    {
        //StartCoroutine(WebClient.GetWeatherForecast());
        StartCoroutine(WebClient.CreateNewUser(new DbUser("unityUser", "testPassword") { Email = "testMail", Number = "88008008080"}, null));
        //StartCoroutine(WebClient.AuthoriseUser(jwt => StartCoroutine(WebClient.GetUser(null, jwt)), password: "testPassword", name:"unityUser"));
        /*StartCoroutine(WebClient.AuthoriseUser(
            jwt => StartCoroutine(WebClient.UpdateUser(null, new DbUser("updatedUser", "updatedPassword"), jwt)), 
            password: "testPassword", 
            name: "unityUser"));*/
        //StartCoroutine(WebClient.AuthoriseUser(jwt => StartCoroutine(WebClient.DeleteUser(null, jwt)), password: "updatedPassword", name: "updatedUser"));
    }
}
