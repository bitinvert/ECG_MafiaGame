using UnityEngine;
using System.Collections;

public class Mission {

	public string missionId { get { return _missionId; } set { _missionId = value; } }
	private string _missionId;

	public string mafiaDescription { get { return _mafiaDescription; } set { _mafiaDescription = value; } }
	private string _mafiaDescription;

	public string policeDescription { get { return _mafiaDescription; } set { _mafiaDescription = value; } }
	private string _policeDescription;

	public Mission (string missionId, string mafiaDescription, string policeDescription) {
		this.missionId = missionId;
		this.mafiaDescription = mafiaDescription;
		this.policeDescription = policeDescription;
	}

}
