using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        CameraController.Instance.SetCameraFollow();

        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            Player.Instance.transform.position = this.transform.position;
        }
    }
}
