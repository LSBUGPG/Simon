using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSequence : MonoBehaviour
{
	public AudioClip tone;
	public AudioClip buzz;
	public GameObject[] lights;
	AudioSource source;

	void Start ()
	{
		source = GetComponent<AudioSource> ();

		var sequence = CreateSequence (10);
		StartCoroutine (PlaySequence (sequence));
	}

	IEnumerator PlaySequence(List<int> sequence)
	{
		foreach (var item in sequence) {
			source.clip = tone;
			source.pitch = 0.8f + 0.1f * (float)item;
			Debug.Log (item);
			source.Play ();
			lights [item].SetActive (true);
			yield return new WaitForSeconds (0.5f);
			lights [item].SetActive (false);
			yield return new WaitForSeconds (0.5f);
		}
	}

	List<int> CreateSequence(int n)
	{
		List <int> sequence = new List<int> ();
		for (int i = 0; i < n; i++) {
			sequence.Add (Random.Range (0, 4));
		}

		return sequence;
	}

	void Update ()
	{
		
	}
}
