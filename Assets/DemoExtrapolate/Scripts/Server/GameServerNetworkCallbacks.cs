using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace Assets.DemoExtrapolate.Scripts.Server
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, "DemoExtrapolate")]
    public class GameServerNetworkCallbacks : GlobalEventListener   
    {
        private readonly List<BoltConnection> _connections = new List<BoltConnection>();

        public override void SceneLoadRemoteDone(BoltConnection connection)
        {
            _connections.Add(connection);
            
            var n = _connections.Count;
            if(n >= 2)
            {
                for(var i = 0; i < n; ++i)
                {
                    var entity = BoltNetwork.Instantiate(BoltPrefabs.DEPlayer, new Vector3(i * 2, 10.0f, 0.0f), Quaternion.identity);
                    entity.AssignControl(_connections[i]);
                }
            }
        }
    }
}
