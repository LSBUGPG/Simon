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
	bool gameOver = false;

	void Start ()
	{
		source = GetComponent<AudioSource> ();

		StartCoroutine (PlayGame());
	}

	public void DetectButton(int button)
	{
		buttonPressed = button;
	}

	IEnumerator PlayGame()
	{
		var sequence = CreateSequence (1);
		while (true) 
		{
			yield return StartCoroutine (PlaySequence (sequence));
			if (gameOver) {
				break;
			} else {
				yield return new WaitForSeconds (1.0f);
				sequence = ExtendSequence(sequence);
			}
		}
	}

    IEnumerator Light(int number)
    {
        source.clip = tone;
        source.pitch = 0.8f + 0.1f * (float)number;
        source.Play ();
        lights [number].SetActive (true);
        yield return new WaitForSeconds (0.5f);
        lights [number].SetActive (false);
    }

    IEnumerator Buzz()
    {
        foreach (var light in lights) {
            light.SetActive (true);
        }
        source.pitch = 1.0f;
        source.clip = buzz;
        source.Play ();
        yield return new WaitForSeconds (0.5f);
        foreach (var light in lights) {
            light.SetActive (false);
        }
        gameOver = true;
    }

	IEnumerator PlaySequence(List<int> sequence)
	{
		foreach (var item in sequence) {
            yield return StartCoroutine(Light(item));
            yield return new WaitForSeconds (0.5f);
		}

		foreach (var item in sequence) {

			buttonPressed = 4;
			float time = 0.0f;
			while (time < 1.0f) {

                time = time + Time.deltaTime;
                yield return null;
				if (item == buttonPressed) {
					// correct answer
                    yield return StartCoroutine(Light(item));
					break;
				}
				else if (item != buttonPressed && buttonPressed != 4) {
					// wrong answer...
                    yield return StartCoroutine(Buzz());
					yield break;
				}
			}

			if (buttonPressed == 4) {
				// too late...
                yield return StartCoroutine(Buzz());
				break;
			}
		}
	}

	List<int> ExtendSequence(List<int> list)
	{
		list.Add(Random.Range (0, 4));
		return list;
	}

	List<int> CreateSequence(int n)
	{
		List <int> sequence = new List<int> ();
		for (int i = 0; i < n; i++) {
			sequence.Add (Random.Range (0, 4));
		}

		return sequence;
	}
}
