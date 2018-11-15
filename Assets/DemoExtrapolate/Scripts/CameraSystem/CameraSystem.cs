using UnityEngine;

namespace Assets.DemoExtrapolate.Scripts.CameraSystem
{
    public class CameraSystem : MonoBehaviour
    {
        public Camera MainCamera;

        public Vector3 LocalPlayerCameraOffset;

        protected void FixedUpdate()
        {
            var cam = MainCamera.gameObject;
            var follow = cam.GetComponent<CameraFollowBehaviour>();
            if(follow == null)
            {
                cam.AddComponent<CameraFollowBehaviour>().Init(
                    MainCamera, 
                    LocalPlayerCameraOffset
                    );
            }
        }
    }
}