using System;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public InstanceController(Guid instanceId)
        {
            InstanceId = instanceId;

            InitializeInstanceVariables();
        }

        Guid InstanceId { get; set; }

        public int PlayerCount { get; set; }
    }
}
