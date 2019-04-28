using UnityEngine;

public class InputManager : MonoBehaviour
{
	public delegate void OnJumpInputPressed(bool jumpPressed);

	public delegate void OnShootPressed(bool shootPressed);

	public static InputManager Instance
	{
		get;
		private set;
	}

	public Vector2 RawAxis
	{
		get;
		private set;
	}

	public Vector2 Axis
	{
		get; private set;
	}

	public Vector2 MousePosition
	{
		get; private set;
	}

	public void RegisterOnJumpInput(OnJumpInputPressed method, bool register)
	{
		if (register)
		{
			m_jumpInputListeners += method;
		}
		else
		{
			m_jumpInputListeners -= method;
		}
	}

	public void RegisterOnShootInput(OnShootPressed method, bool register)
	{
		if (register)
		{
			m_onShootPressed += method;
		}
		else
		{
			m_onShootPressed -= method;
		}
	}

	#region Private

	private void Update()
	{
		RawAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (RawAxis != Vector2.zero)
			Debug.Log("Raw Axis " + RawAxis);
		if (Axis != Vector2.zero)
			Debug.Log("Axis " + Axis);
		Axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		CheckJump();
		CheckShoot();
	}

	private void CheckJump()
	{
		if (Input.GetButtonDown("Jump"))
		{
			m_jumpInputListeners?.Invoke(true);
		}
		if (Input.GetButtonUp("Jump"))
		{
			m_jumpInputListeners?.Invoke(false);
		}
	}

	private void CheckShoot()
	{
		if (Input.GetButtonDown("Shoot"))
		{
			m_onShootPressed?.Invoke(true);
		}
		if (Input.GetButtonUp("Shoot"))
		{
			m_onShootPressed?.Invoke(false);
		}
	}

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("Duplicate InputManager => Destroyed.");
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private OnJumpInputPressed m_jumpInputListeners = null;
	private OnShootPressed m_onShootPressed = null;

	#endregion Private
}