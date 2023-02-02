using System;
using System.Collections.Generic;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public Dictionary<Guid, VariableValue> InstanceVariables { get; set; }

        public void InitializeInstanceVariables()
        {
            InstanceVariables = new Dictionary<Guid, VariableValue>();

            // If we do, go out and fetch the default values of all our instance variables
            foreach (InstanceVariableBase instVar in InstanceVariableBase.Lookup.Values)
            {
                VariableValue copyValue = new VariableValue();
                copyValue.SetValue(instVar.DefaultValue.Value);
                copyValue.Type = instVar.Type;
                InstanceVariables[instVar.Id] = copyValue;
            }
        }

        public void AddInstanceVariable(Guid instanceVarId)
        {
            // Create the instance var if it does not exist
            if (InstanceVariables.ContainsKey(instanceVarId))
            {
                return;
            }

            VariableValue copyValue = new VariableValue();
            var instVar = (InstanceVariableBase)InstanceVariableBase.Lookup[instanceVarId];
            copyValue.SetValue(instVar.DefaultValue.Value);
            copyValue.Type = instVar.Type;
            InstanceVariables[instanceVarId] = copyValue;
        }
    }
}
