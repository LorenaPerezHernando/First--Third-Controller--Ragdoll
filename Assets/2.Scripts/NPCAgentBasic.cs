using UnityEngine;
using System;
using UnityEngine.AI;

public class NPCAgentBasic : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Transform _target;
	private Animator _anim;
	private NavMeshAgent _agent;
	private float previousAceleration;
	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_anim = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}
	// Start is called before the first frame update
	void Start()
    {
		previousAceleration = _agent.acceleration;

	}

    // Update is called once per frame
    void Update()
    {
		_agent.destination = _target.position;
		if (_agent.velocity.magnitude == 0)
		{
			//Attack
			if (Vector3.Distance(transform.position, _target.position) < 5)//Attack Distance
			{
				_anim.SetBool("Walk", false);
				_anim.SetBool("Attack", true);
				transform.LookAt(_target);
			}
		}
		else
		{
			//Chase
			_anim.SetBool("Walk", true);
			_anim.SetBool("Attack", false);
		}

		if (Vector3.Distance(transform.position, _target.position) > 10)//Chase distance
		{
			//Iddle
			_anim.SetBool("Walk", false);
			_anim.SetBool("Attack", false);
			_agent.acceleration = 0;
			_agent.velocity = Vector3.zero;
		}
		else
		{
			if (Vector3.Distance(transform.position, _target.position) > 5)//Attack Distance
			{
				_agent.acceleration = previousAceleration;
				_anim.SetBool("Walk", true);
			}

		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
