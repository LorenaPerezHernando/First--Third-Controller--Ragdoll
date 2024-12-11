using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private float _speed = 15;
	private Vector2 _movement;
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(_movement.x * Time.deltaTime * _speed, 0, _movement.y * Time.deltaTime * _speed, Space.World);
	}
	#endregion

	#region Public Methods
	public void OnMove(InputValue value)
	{
		_movement = (value.Get<Vector2>());
	}

	#endregion

	#region Private Methods
	#endregion
}
