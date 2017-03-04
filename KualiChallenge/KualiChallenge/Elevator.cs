using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KualiChallenge
{
    public class Elevator
    {
        #region constants
        const int FLOOR_TRAVEL_DELAY = 1000;
        const int DOOR_OPEN_CLOSE_DELAY = 1000;
        #endregion


        /// <summary>
        /// Constructor
        /// Initialize with the given number of floors
        /// Initialize at floor 1 with the door closed
        /// Initialize with 0 trips
        /// Initialize with 0 floors passed
        /// </summary>
        /// <param name="floors"></param>
        public Elevator(int index, int floors, Controller controler)
        {
            Index = index;
            Floors = floors;
            CurrentFloor = 1;
            IsDoorOpen = false;
            Trips = 0;
            FloorsPassed = 0;
            InService = true;
        }

        #region Properties
        int Index { get; set; }
        int Floors { get; set; }
        public int CurrentFloor { get; private set; }
        bool IsDoorOpen { get; set; }
        int Trips { get; set; }
        int FloorsPassed { get; set; } // Assume that floors passed includes the current floor. From 1 to 4 is 3 floors passed
        public bool InService { get; set;  }
        public bool Available { get; set; }

        #endregion

        #region Public Interface

        /// <summary>
        /// Accept an assignment to go to a floor
        /// </summary>
        /// <param name="floor"></param>
        /// Postcondition
        /// CurrentFloor' = floor
        /// Trips' = Trips +1
        /// FloorsPassed' = FloorsPassed + Abs(CurrentFloor - floor)
        /// InService' = Trips' < 100 ? InService : false;
        public async Task<int> GoToFloor(int floor)
        {
            // Close Door
            await Task.Delay(DOOR_OPEN_CLOSE_DELAY);

            // Travel to destination


            // Record trip

            // Open Door

            return floor;
        }

        #endregion

    }
}
