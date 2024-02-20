using System;
namespace GelberAssignment.Classes
{
	public class Railway
	{
		public int NumberOfStations { get; set; }
		public int TimeToTravel { get; set; }
		public int FrequencyDepart { get; set; }
		public int CapacityOfTrains { get; set; }
		public List<Train> Trains { get; set; }
		public List<Passenger> PassengersInRailway { get; set; }

		public Railway(int numberOfStations, int timeToTravel, int frequencyDepart, int capacityOfTrains)
		{
			NumberOfStations = numberOfStations;
			TimeToTravel = timeToTravel;
			FrequencyDepart = frequencyDepart;
			CapacityOfTrains = capacityOfTrains;
			Trains = new List<Train>();
			PassengersInRailway = new List<Passenger>();
		}

		public void removeTrains()
		{
			this.Trains.RemoveAll(t => (t.Direction == "Right" && t.CurrentPosition == this.NumberOfStations)
            || (t.Direction == "Left" && t.CurrentPosition == 1));
        }


	}
}

 