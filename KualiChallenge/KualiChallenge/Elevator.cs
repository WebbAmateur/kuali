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
        const int PASSENGER_DELAY = 5000;
        const int MAX_FLOORS_PASSED = 100;
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
            IsOccupied = false;
            InService = true;
        }

        #region Properties
        public Controller Master { get; set; }
        public int Index { get; set; }
        int Floors { get; set; }
        public int CurrentFloor { get; private set; }
        bool IsDoorOpen { get; set; }
        int Trips { get; set; }
        int FloorsPassed { get; set; } // Assume that floors passed includes the current floor. From 1 to 4 is 3 floors passed
        bool IsOccupied { get; set; }
        bool InService { get; set;  }

        public bool IsAvailable { get { return !IsOccupied && InService; } }


        #endregion

        #region Public Interface

        /// <summary>
        /// Accept and assignment to pick up passengers at startFloor and take them to endFloor
        /// </summary>
        /// <param name="startFloor"></param>
        /// Postcondition
        /// CurrentFloor' = floor
        /// Trips' = Trips +1
        /// FloorsPassed' = FloorsPassed + Abs(CurrentFloor - floor)
        /// InService' = Trips' < 100 ? InService : false;
        public async Task<int> MakeTrip(int startFloor, int endFloor)
        {
            int floorsTraveled = Math.Abs(CurrentFloor - startFloor) + Math.Abs(endFloor - startFloor);

            if (startFloor < MIN_FLOOR || startFloor > Floors || endFloor < MIN_FLOOR || endFloor > Floors)
            {
                // Doesn't count as a trip
                return CurrentFloor;
            }


            //  If necessary travel to startFloor
            if (startFloor != CurrentFloor)
            {
                await CloseDoor();
                await TravelToFloor(startFloor);
                await OpenDoor();
            }

            // Invariant: CurrentFloor = startFloor AND IsDoorOpen


            // Let passengers get on
            await Task.Delay(PASSENGER_DELAY);

            await CloseDoor();

            await TravelToFloor(endFloor);


            // Open the Door
            await OpenDoor();

            // Wait for the passengers to exit
            await Task.Delay(PASSENGER_DELAY);

            // Record trip
            Trips++;
            FloorsPassed += floorsTraveled;
            if (FloorsPassed >= MAX_FLOORS_PASSED)
            {
                InService = false;
            }

            return startFloor;
        }

        /// <summary>
        /// Open the Door
        /// - Report the open door
        /// </summary>
        /// <returns></returns>
        private async Task<int> OpenDoor()
        {
            await Task.Delay(DOOR_OPEN_CLOSE_DELAY);
            IsDoorOpen = true;
            // Each elevator will report when it opens or closes its doors
            Master.ReportOpenDoor(Index);
            return CurrentFloor;
        }


        /// <summary>
        /// Close the door
        ///  - Report the close door
        /// </summary>
        /// <returns></returns>
        private async Task<int> CloseDoor()
        {
            await Task.Delay(DOOR_OPEN_CLOSE_DELAY);
            IsDoorOpen = false;
            // Each elevator will report when it opens or closes its doors
            Master.ReportCloseDoor(Index);
            return CurrentFloor;
        }


        /// <summary>
        /// Travel from CurrentFloor to floor
        /// - Report every floor change
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        private async Task<int> TravelToFloor(int floor)
        {
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

            return CurrentFloor;
           
        }

        #endregion

    }
}
