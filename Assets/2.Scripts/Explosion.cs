using UnityEngine;
using System;

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
	private Rigidbody _playerRb;
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
			_explosionCamera.transform.Translate(_explosionCamera.transform.forward * Time.deltaTime * 2, Space.Self);
			//Resetear Player
			
			if(_playerRb.velocity.magnitude < 3 )
			{
				Debug.Log("Hola");
				_playerHips.transform.parent.GetComponent<CharacterController>().enabled = false; //Character controller no permite un teletransporte
                _mainCamera.enabled = true;
                _explosionCamera.enabled = false;
				Vector3 currentPos = _playerHips.transform.position;
				_playerHips.transform.localPosition = Vector3.zero;
				_playerHips.transform.parent.position = currentPos;
                _playerHips.transform.parent.GetComponent<CharacterController>().enabled = true;
				//Levantarse
				

                _playerHips.transform.parent.GetComponent<Animator>().SetTrigger("StandUp");
				_playerHips.transform.parent.GetComponent<Animator>().enabled = true;
                Destroy(gameObject);


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

	//private void Sta

	private void AlignPosToHips() //Para empezar a caminar desde donde cayó
	{
		Vector3 originalHipsPos = _playerHips.transform.position;
		transform.position = _playerHips.transform.position;

		if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
		{
			transform.position = new Vector3(transform.position.x, hitInfo.point.y, hitInfo.point.z);
		}
		_playerHips.transform.position = originalHipsPos;
	}


	#endregion
}
