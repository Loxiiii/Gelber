using System;
using System.Diagnostics;

namespace GelberAssignment.Classes
{
	public class Train
	{
		public int OriginStation { get; set; }
		public int TimeOfCreation { get; set; }
		public int CurrentPosition { get; set; }
		public List<Passenger> Passengers { get; set; }
        public List<Passenger> PassengersToUnboard { get; set; }
		public string Direction { get; set; }
		public int RwNumberOfStations { get; set; }
		public int DisappearAtStation { get; set; }
		public bool Traveling { get; set; }

		public Train(int originStation, int timeOfCreation, int railwayStations)
		{
			OriginStation = originStation;
			TimeOfCreation = timeOfCreation;
			CurrentPosition = originStation;
			Passengers = new List<Passenger>();
            PassengersToUnboard = new List<Passenger>();
			Direction = originStation == 1 ? "Right" : "Left";
			RwNumberOfStations = railwayStations;
			DisappearAtStation = originStation == 1 ? railwayStations : 1;
			Traveling = false;
		}

		
		public void moveTrainAndPassengers(int t, ref Railway rw)
		{
            if ((t - this.TimeOfCreation) % rw.TimeToTravel == 0)
            {
                // Change currentPosition based on direction of movement
                if (this.Direction == "Right")
                {
                    this.CurrentPosition++;
                    this.Traveling = false;
                    foreach (Passenger passenger in this.Passengers)
                    {
                        passenger.CurrentPosition++;
                        passenger.Traveling = false;
                    }
                }
                else
                {
                    this.CurrentPosition--;
                    this.Traveling = false;
                    foreach (Passenger passenger in this.Passengers)
                    {
                        passenger.CurrentPosition--;
                        passenger.Traveling = false;
                    }
                }
            }
            else
            {
                this.Traveling = true;
                foreach (Passenger passenger in this.Passengers)
                {
                    passenger.Traveling = true;
                }
            }
        }

        public void unboardPassengers(ref Railway rw, ref int numPassengers)
        {
            // Get list of passengers that are at the destination station
            this.PassengersToUnboard = this.Passengers
                .Where(p => p.Destination == this.CurrentPosition).ToList();

            // Get guids of those passengers
             List<Guid> guidsToUnboard = this.PassengersToUnboard.Select(p => p.Id).ToList();

            // Remove the passengers from the this
            this.Passengers.RemoveAll(p => guidsToUnboard.Contains(p.Id));

            // Remove the passengers from the railway
            rw.PassengersInRailway.RemoveAll(p => guidsToUnboard.Contains(p.Id));

            // Decrease numPassengers by the number of unboarded passengers
            numPassengers -= guidsToUnboard.Count();
        }

        public List<Passenger> checkBoardablePassengers(Railway rw)
        {
            return (List<Passenger>)rw.PassengersInRailway
                    .Where(p => p.Boarded == false && p.CurrentPosition == this.CurrentPosition &&
                    (this.Direction == "Right" && p.Destination > p.CurrentPosition ||
                    this.Direction == "Left" && p.Destination < p.CurrentPosition))
                    .OrderByDescending(p => Math.Abs(p.CurrentPosition - p.Destination))
                    .ThenBy(p => p.TypeOfPassenger).ToList();
        }

        public void boardPassenger(Passenger passenger, Railway rw)
        {

            if (passenger.TypeOfPassenger == "A")
            {
                passenger.Boarded = true;
                this.Passengers.Add(passenger);
            }
            else
            {
                if (this.Passengers.Count() <= rw.CapacityOfTrains / 2)
                {
                    passenger.Boarded = true;
                    this.Passengers.Add(passenger);
                }
            }
        }
	}
}

