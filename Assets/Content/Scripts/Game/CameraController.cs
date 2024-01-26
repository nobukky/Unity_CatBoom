using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RSE_Input _inputArrow;
    [SerializeField] private RSO_RotationPlayer _rsoPlayerRotation;
    [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
    [SerializeField] private GameSettings _gameSettings;

    private float _smoothRotate;
    
    private void OnEnable()
    {
        _inputArrow.action += RotateCamera;
        _rsoPositionPlayer.OnChanged += FollowPlayer;
    }

    private void OnDisable()
    {
        _inputArrow.action -= RotateCamera;
        _rsoPositionPlayer.OnChanged -= FollowPlayer;
    }

    private void RotateCamera(int inputRotationID) 
    {
        // correspond to -1 for left, 0 for nothing and 1 for right
        _rsoPlayerRotation.value = Mathf.SmoothDamp(_rsoPlayerRotation.value, inputRotationID * _gameSettings.maxRotation, ref _smoothRotate, _gameSettings.rotationSmoothDuration);
        transform.rotation = Quaternion.Euler(0, 0, _rsoPlayerRotation.value);
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(
            transform.position.x, 
            Mathf.Lerp(transform.position.y, _rsoPositionPlayer.value.y + _gameSettings.cameraOffsetY, _gameSettings.cameraLerpDuration) , 
            transform.position.z);
    }
}