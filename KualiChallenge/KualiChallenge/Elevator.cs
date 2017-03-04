using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KualiChallenge
{
    public class Elevator
    {

        /// <summary>
        /// Constructor
        /// Initialize with the given number of floors
        /// Initialize at floor 1 with the door closed
        /// Initialize with 0 trips
        /// Initialize with 0 floors passed
        /// </summary>
        /// <param name="floors"></param>
        public Elevator(int floors)
        {
            
            Floors = floors;
            CurrentFloor = 1;
            IsDoorOpen = false;
            Trips = 0;
            FloorsPassed = 0;
        }

        #region Properties
        int Floors { get; set; }
        int CurrentFloor { get; set; }
        bool IsDoorOpen { get; set; }
        int Trips { get; set; }
        int FloorsPassed { get; set; }

        #endregion

    }
}
