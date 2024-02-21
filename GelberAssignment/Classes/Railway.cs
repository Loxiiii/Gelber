using System;
using System.Collections.Generic;

namespace GelberAssignment.Classes
{
    public class Railway
    {
        private int numberOfStations;
        private int timeToTravel;
        private int frequencyDepart;
        private int capacityOfTrains;

        public int NumberOfStations
        {
            get { return numberOfStations; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Number of stations must be greater than 0.");
                }
                numberOfStations = value;
            }
        }

        public int TimeToTravel
        {
            get { return timeToTravel; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Time to travel must be greater than 0.");
                }
                timeToTravel = value;
            }
        }

        public int FrequencyDepart
        {
            get { return frequencyDepart; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Frequency of departure must be greater than 0.");
                }
                frequencyDepart = value;
            }
        }

        public int CapacityOfTrains
        {
            get { return capacityOfTrains; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity of trains must be greater than 0.");
                }
                capacityOfTrains = value;
            }
        }

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

        public void RemoveTrains()
        {
            try
            {
                this.Trains.RemoveAll(t => (t.Direction == "Right" && t.CurrentPosition == this.NumberOfStations)
                || (t.Direction == "Left" && t.CurrentPosition == 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while removing trains: " + ex.Message);
            }
        }
    }
}
