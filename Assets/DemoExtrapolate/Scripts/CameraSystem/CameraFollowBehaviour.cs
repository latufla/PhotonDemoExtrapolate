using System.Linq;
using UnityEngine;

namespace Assets.DemoExtrapolate.Scripts.CameraSystem
{
    public class CameraFollowBehaviour : MonoBehaviour
    {

        private Camera _camera;
        private Vector3 _offset;

        public void Init(Camera camera, Vector3 offset)
        {
            _camera = camera;
            _offset = offset;
        }

        private void LateUpdate()
        {
            var players = GameObject.FindGameObjectsWithTag("Player").ToList();
            var localPlayer = players.FirstOrDefault(p =>
            {
                var b = p.GetComponent<PlayerNetworkBehaviour>();
                return b != null && b.entity.hasControl;
            });
            //localPlayer = players[0];
            
            if (localPlayer == null)
                return;

            _camera.gameObject.transform.position = localPlayer.transform.position + _offset;
        }
    }
}
