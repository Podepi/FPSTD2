using UnityEngine;
using System.Collections;

public class OneShotParticles : MonoBehaviour
{

	private void Start()
	{
		Destroy(gameObject, GetComponent<ParticleSystem>().main.duration); 
	}

}
