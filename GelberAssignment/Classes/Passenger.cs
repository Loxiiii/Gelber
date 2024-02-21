using System;
namespace GelberAssignment.Classes
{
	public class Passenger
	{
        public Guid Id { get; set; }
		public string TypeOfPassenger { get; set; }
        public int TimeArrives { get; set; }
        public int Destination { get; set; }
        public int Origin { get; set; }
        public int CurrentPosition { get; set; }
        public bool Boarded { get; set; }
        public bool Traveling { get; set; }

        public Passenger(string typeOfPassenger, int timeArrives, int destination, int origin)
		{
            Id = Guid.NewGuid();
			TypeOfPassenger = typeOfPassenger;
            TimeArrives = timeArrives;
            Destination = destination;
            Origin = origin;
            CurrentPosition = origin;
            Boarded = false;
            Traveling = false;
        }

    }
}

