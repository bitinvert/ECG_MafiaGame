using UnityEngine;
using System.Collections;

public class User {

	public string username {get{ return _username;} set {_username = value;}}
	private string _username;

	public User (string username) {
		this.username = username;
	}
}
