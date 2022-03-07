using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPipeline : MonoBehaviour
{
    public GameObject ObjectPlayer;
    public GameObject ObjectPlayerCamera;
    public GameObject ObjectBot;

    private List<IUpdate> _componentsUpdates = new List<IUpdate>();

	private void Start()
	{
		_componentsUpdates.Add(ObjectPlayer.GetComponent<Move>());
		_componentsUpdates.Add(ObjectPlayer.GetComponent<Player>());
		_componentsUpdates.Add(ObjectPlayer.GetComponent<Attack>());
		_componentsUpdates.Add(ObjectPlayerCamera.GetComponent<CameraBinding>());
		_componentsUpdates.Add(ObjectBot.GetComponent<Player>());
		_componentsUpdates.Add(ObjectBot.GetComponent<AttackBot>());
	}

	void Update()
    {
        foreach (IUpdate script in _componentsUpdates)
		{
			script.LaunchUpdate();
		}
    }
}
