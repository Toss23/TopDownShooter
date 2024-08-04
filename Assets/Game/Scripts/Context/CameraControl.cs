using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour, IInitable, IUpdatable
{
    private const int MAP_WEDTH = 40;
    private const int MAP_HEIGHT = 30;

    private Camera _camera;
    private Player _player;

    private Vector2 _screenSize;
    private Vector2 _cameraSize;
    private Vector2 _borders;

    public void PreInit(IContext context)
    {
        _camera = GetComponent<Camera>();
        _screenSize = new Vector2(Screen.width, Screen.height);
        _cameraSize = new Vector2(_screenSize.x / _screenSize.y * _camera.orthographicSize, _camera.orthographicSize);
        _borders = new Vector2(MAP_WEDTH / 2 - _cameraSize.x, MAP_HEIGHT / 2 - _cameraSize.y);
    }

    public void Init(IContext context)
    {
        _player = context.GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
    }

    public void UpdateGame(float deltaTime)
    {
        Vector2 position = _player.Movement.Position;
        position = new Vector3(Mathf.Clamp(position.x, -_borders.x, _borders.x), Mathf.Clamp(position.y, -_borders.y, _borders.y), -10);
        _camera.transform.position = position;
    }

    public void FixedUpdateGame(float deltaTime)
    {
        
    }
}
