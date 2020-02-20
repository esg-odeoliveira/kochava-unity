using System.Collections.Generic;
using UnityEngine;

public class KochavaDemo : MonoBehaviour {
	private void Awake()
	{
		Debug.Log("Register for Token");
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
		Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
	}

	
	
	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) 
	{
		Debug.Log("Kochava Received Registration Token: " + token.Token);
		// Add the new/updated push token to Kochava.
		Kochava.Tracker.AddPushToken (token.Token);
	}

	public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) 
	{
		UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
		UnityEngine.Debug.Log("Data : " + e.Message.RawData);
		string json = JsonUtility.ToJson(e.Message.Data);
		UnityEngine.Debug.Log("Json : " + json);
		
		IDictionary<string, string> data = e.Message.Data;
 
		// Retrieve our engagement campaign information.
		string kochava;
		data.TryGetValue ("kochava", out kochava);

		if (kochava != null && data.ContainsKey("silent")) {
			// This is a Kochava silent push. These are used for tracking uninstalls and should not be acted on.
		} else if (kochava != null) {
			//This is a regular Kochava push.
 
			//Retrieve the rest of the items for our push.
			string title;
			string message;
			data.TryGetValue ("title", out title);
			data.TryGetValue ("message", out message);
			Debug.Log("kochava title is:" + title);
			Debug.Log("kochava message is:" + message);
		}
		else
		{
			Debug.Log("Didn't find Kochava attribute in the notification message");
		}
	}
	
	
	
	
	
	
	
	
	
	public void SendEvent() {
		
	}
}
