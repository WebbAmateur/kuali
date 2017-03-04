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
        const int MIN_FLOOR = 1;
        #endregion


        /// <summary>
        /// Constructor
        /// Initialize with the given number of floors
        /// Initialize at floor 1 with the door closed
        /// Initialize with 0 trips
        /// Initialize with 0 floors passed
        /// </summary>
        /// <param name="floors"></param>
        public Elevator(int index, int floors, Controller controller)
        {
            Master = controller; 
            Index = index;
            Floors = floors;
            CurrentFloor = 1;
            IsDoorOpen = false;
            Trips = 0;
            FloorsPassed = 0;
            InService = true;
        }

        #region Properties
        public Controller Master { get; set; }
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
            if (floor < MIN_FLOOR || floor > Floors || floor == CurrentFloor)
            {
                // Doesn't count as a trip
                return floor;
            }

            // Close Door
            await Task.Delay(DOOR_OPEN_CLOSE_DELAY);
            IsDoorOpen = false;
            // Each elevator will report when it opens or closes its doors
            Master.ReportCloseDoor(Index);
            

            // Travel to destination

            int floorChange;
            int floorDelta;

            if (CurrentFloor < floor)
            {
                // We are going up
                floorChange = 1;
                floorDelta = floor - CurrentFloor;
            }
            else
            {
                // We are going down
                floorChange = -1;
                floorDelta = CurrentFloor - floor;
            }

            while (CurrentFloor != floor)
            {
                await Task.Delay(FLOOR_TRAVEL_DELAY);
                CurrentFloor = CurrentFloor + floorChange;

                //Each elevator will report as is moves from floor to floor
                Master.ReportNewFloor(Index, CurrentFloor);

            }




            // Record trip

            // Open Door
            await Task.Delay(DOOR_OPEN_CLOSE_DELAY);
            IsDoorOpen = true;
            Master.ReportOpenDoor(Index);

            return floor;
        }

        #endregion

    }
}
