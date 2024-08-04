using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour, IInitable, IUpdatable
{
    private Camera _camera;
    private Player _player;

    public void PreInit(IContext context)
    {
        _camera = GetComponent<Camera>();
    }

    public void Init(IContext context)
    {
        _player = context.GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
    }

    public void UpdateGame(float deltaTime)
    {
        Vector2 position = _player.Movement.Position;
        position = new Vector3(Mathf.Clamp(position.x, -9.75f, 9.75f), Mathf.Clamp(position.y, -7.25f, 7.25f), -10);
        _camera.transform.position = position;
    }

    public void FixedUpdateGame(float deltaTime)
    {
        
    }
}
