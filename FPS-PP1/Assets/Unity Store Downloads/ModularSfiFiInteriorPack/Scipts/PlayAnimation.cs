using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
	public GameObject objectToInteractWith;
	public string displayText = "Press 'F' to start animation";
	public Animation animationToPlay;
	public string playerTag = "Player";
	private bool isPlaying;

	private void Update()
	{
		GameObject player = GameObject.FindWithTag(playerTag);
		if (player != null)
		{
			if (Vector3.Distance(player.transform.position, objectToInteractWith.transform.position) < 2f)
			{
				Debug.Log(displayText);

				if (Input.GetKeyDown(KeyCode.F))
				{
					if(isPlaying)
					{
						animationToPlay.Stop();
						animationToPlay[animationToPlay.clip.name].time = animationToPlay[animationToPlay.clip.name].length;
						animationToPlay.Play();
						isPlaying = false;
					}
					else
					{
						animationToPlay.Play();
						isPlaying = true;
					}
				}
			}
		}
	}
}