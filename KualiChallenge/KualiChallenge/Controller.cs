using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KualiChallenge
{
    public class Controller
    {
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

            // Find an elevator to make the trip
            int index = 1;

            // Assign the elevator to the trip
            Elevators[index].MakeTrip(startFloor, endFloor);

            return index;
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
