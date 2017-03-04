using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KualiChallenge
{
    public class Controller
    {

        private Object thisLock = new Object();

        const int MAX_TRIPS = 100;
        const int MIN_FLOOR = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevators"></param>
        public Controller(int floors, int elevators)
        {
            Elevators = new Elevator[elevators];

            for(int index = 0; index < elevators; index++)
            {
                //Initialize the elevator simulation with the desired number of elevators, and the desired number of floors.
                Elevators[index] = new Elevator(index, floors, this);
            }
        }

        #region properties
        Elevator[] Elevators { get; set; }
        int Floors { get; set; }

        #endregion
        /// <summary>
        /// Request an elevator at the given floor
        /// 
        /// </summary>
        /// <param name="floor"></param>
        public async Task<int> RequestTrip(int startFloor, int endFloor)
        {
            if (startFloor < MIN_FLOOR || startFloor > Floors || endFloor < MIN_FLOOR || endFloor > Floors)
            {
                return -1;
            }
            int index;

            // Process one request at a time
            lock (thisLock)
            {

                // Find an elevator to make the trip
                Elevator assignedElevator = AssignElevator(startFloor, endFloor);

                // I am not handling the case when no elevator is available
                if (assignedElevator == null)
                {
                    return -1;
                }
                index = assignedElevator.Index;

                // Assign the elevator to the trip
                assignedElevator.MakeTrip(startFloor, endFloor);
            }

            return index; 
        }


        /// <summary>
        /// Choose an elevator to make the trip
        /// No new elevator will be assigned during this method, but elevators may become available
        /// </summary>
        /// <param name="startFloor"></param>
        /// <param name="endFloor"></param>
        /// <returns></returns>
        public Elevator AssignElevator(int startFloor, int endFloor)
        {
            // Find all of the available elevators
            IEnumerable<Elevator> availableElevators = Elevators.Where(e => e.IsAvailable);

            if (availableElevators.Count() == 0)
            {
                // No elevators are available try again later
                return null;
            }

            Elevator assignedElevator = availableElevators.First();
            
            int floorDistance = Math.Abs(startFloor - assignedElevator.CurrentFloor);

            // When an elevator request is made, the unoccupied elevator closest to it will answer the call
            foreach (Elevator current in availableElevators)
            {
                int currentDistance = Math.Abs(startFloor - current.CurrentFloor);
                if (currentDistance < floorDistance)
                {
                    floorDistance = currentDistance;
                    assignedElevator = current;
                }

                if(currentDistance == 0)
                {
                    return assignedElevator;
                }
            }

            return assignedElevator;

        }





        /// <summary>
        /// Called by an elevator when it reaches a floor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="floor"></param>
        public void ReportNewFloor(int index, int floor)
        {

        }


        /// <summary>
        /// Called by an elevator when it opens its doors
        /// </summary>
        /// <param name="index"></param>
        public void ReportOpenDoor(int index)
        {

        }


        /// <summary>
        /// Called by an elevator when it closes its door
        /// </summary>
        /// <param name="index"></param>
        public void ReportCloseDoor(int index)
        {

        }
    }
}
