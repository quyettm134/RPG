using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            Player.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetCameraFollow();
        }
    }
}
