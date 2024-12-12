using UnityEngine;
using System;
using System.Collections;

public class Explosion : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private Camera _explosionCamera;
	[SerializeField] private float _explosionArea;
	[SerializeField] private float _explosionForce = 1000;
	[SerializeField] private GameObject _effect;
	[SerializeField] private GameObject _playerHips = null;
	private Rigidbody _playerRb = null;
    #endregion

    #region Unity Callbacks

    private void Start()
    {
       
    }

    private void Update()
    {
	
		
		if(_playerHips != null && Vector3.Distance(_explosionCamera.transform.position, _playerHips.transform.position) > 5)
		{
			_explosionCamera.transform.LookAt(_playerHips.transform.position);
			Vector3 directionToPlayer = (_playerHips.transform.position - _explosionCamera.transform.position).normalized;
			_explosionCamera.transform.position += directionToPlayer * Time.deltaTime * 2;

			//Vector3 directionToMainCamera = (_mainCamera.transform.position - _explosionCamera.transform.position).normalized;

			//// Mover la cámara principal hacia la cámara inactiva
			//_explosionCamera.transform.position += directionToMainCamera * Time.deltaTime * 2;


			//Resetear Player

			if (_playerRb.velocity.magnitude < 1 )
			{
				StartCoroutine(RagDollMoment());
				Debug.Log("Hola");
                


			}
		}
    }
    private void OnTriggerEnter(Collider other)
	{
		//Camera
		_mainCamera.enabled = false;
		_explosionCamera.enabled = true;

		//Effects
		_effect.SetActive(true);
		Animator playerAnim = other.GetComponentInParent<Animator>();
		_playerHips = playerAnim.transform.GetChild(0).gameObject;
		_playerRb = playerAnim.GetComponentInChildren<Rigidbody>();
		if (playerAnim != null)
		{
			playerAnim.enabled = false;

		}

        ExplosionForce();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, _explosionArea);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void ExplosionForce()
	{
		Collider[] objects = Physics.OverlapSphere(transform.position, _explosionArea);
		for (int i = 0; i < objects.Length; i++)
		{
			Rigidbody objectRB = objects[i].GetComponent<Rigidbody>();
			if (objectRB != null)
			{
				objectRB.AddExplosionForce(_explosionForce, transform.position, _explosionArea);
			}
		}
	}

	IEnumerator RagDollMoment()
	{
		yield return new WaitForSeconds(10);
        RagDollFinish();
    }

	private void RagDollFinish()
	{
        _mainCamera.enabled = true;
        _explosionCamera.enabled = false;
        Vector3 currentPos = _playerHips.transform.position;
        _playerHips.transform.parent.GetComponent<CharacterController>().enabled = false; //Character controller no permite un teletransporte
        _playerHips.transform.localPosition = Vector3.zero;
        _playerHips.transform.parent.position = currentPos;
        //Levantarse

        _playerHips.transform.parent.GetComponent<CharacterController>().enabled = true;
        _playerHips.transform.parent.GetComponent<Animator>().SetTrigger("StandUp");
        _playerHips.transform.parent.GetComponent<Animator>().enabled = true;
        Destroy(gameObject);
    }


	#endregion
}
