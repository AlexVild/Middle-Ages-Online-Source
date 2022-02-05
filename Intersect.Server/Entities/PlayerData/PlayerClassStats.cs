using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities.PlayerData
{
    public class PlayerClassStats
    {
        public int Rank;

        public int TotalTasksComplete;

        public int TasksRemaining;

        public bool AssignmentAvailable;

        public bool OnSpecialAssignment;

        public bool OnTask;

        public bool InGuild;

        public PlayerClassStats()
        {
            Rank = 0;
            TotalTasksComplete = 0;
            TasksRemaining = 2; // TODO Alex this should be configurable
            AssignmentAvailable = false;
            OnSpecialAssignment = false;
            OnTask = false;
            InGuild = false;
        }
    }
}
