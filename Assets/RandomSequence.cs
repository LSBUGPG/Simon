using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSequence : MonoBehaviour
{
	void Start ()
	{
		var sequence = CreateSequence (10);
		foreach (var item in sequence) {
			Debug.Log (item);
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
