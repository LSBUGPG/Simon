using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSequence : MonoBehaviour
{
	public AudioClip tone;
	public AudioClip buzz;
	public GameObject[] lights;
	AudioSource source;
	int buttonPressed;

	void Start ()
	{
		source = GetComponent<AudioSource> ();

		var sequence = CreateSequence (1);
		StartCoroutine (PlaySequence (sequence));
	}

	public void DetectButton(int button)
	{
		buttonPressed = button;
	}

	IEnumerator PlaySequence(List<int> sequence)
	{
		foreach (var item in sequence) {
			source.clip = tone;
			source.pitch = 0.8f + 0.1f * (float)item;
			source.Play ();
			lights [item].SetActive (true);
			yield return new WaitForSeconds (0.5f);
			lights [item].SetActive (false);
			yield return new WaitForSeconds (0.5f);
		}

		foreach (var item in sequence) {

			buttonPressed = 4;
			float time = 0.0f;
			while (time < 1.0f) {

				if (item == buttonPressed) {
					// correct answer
					source.clip = tone;
					source.pitch = 0.8f + 0.1f * (float)item;
					source.Play ();
					lights[item].SetActive(true);
					yield return new WaitForSeconds (1.0f);
					lights[item].SetActive(false);
					break;
				}
				else if (item != buttonPressed && buttonPressed != 4) {
					// wrong answer...
					foreach (var light in lights) {
						light.SetActive (true);
					}
					source.clip = buzz;
					source.Play ();
					yield return new WaitForSeconds (1.0f);
					foreach (var light in lights) {
						light.SetActive (false);
					}
					break;
				}
				time = time + Time.deltaTime;
				yield return null;
			}

			if (buttonPressed == 4) {
				// too late...
				foreach (var light in lights) {
					light.SetActive (true);
				}
				source.clip = buzz;
				source.Play ();
				yield return new WaitForSeconds (1.0f);
				foreach (var light in lights) {
					light.SetActive (false);
				}
				break;
			}

			yield return new WaitForSeconds (1.0f);
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
