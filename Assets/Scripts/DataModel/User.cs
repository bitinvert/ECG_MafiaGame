using UnityEngine;
using System.Collections;

public class User {

	public string username { get { return _username; } set {_username = value; } }
	private string _username;

	public Fraction fraction { get { return _fraction; } }
	private Fraction _fraction;

	public User (string username, string fraction) {
		this.username = username;
		if (fraction.Equals ("Mafia")) {
			this._fraction = Fraction.MAFIA;
		} else if (fraction.Equals ("Police")) {
			this._fraction = Fraction.POLICE;
		}
	}
}
