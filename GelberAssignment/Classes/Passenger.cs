using System;

namespace GelberAssignment.Classes
{
    public class Passenger
    {
        public Guid Id { get; private set; }
        private string? typeOfPassenger;
        private int timeArrives;
        private int destination;
        private int origin;
        private int currentPosition;

        public string TypeOfPassenger
        {
            get
            {
                if (typeOfPassenger == null)
                {
                    throw new InvalidOperationException("Type of passenger is not set.");
                    // Or return a default value
                    // return "";
                }
                return typeOfPassenger;
            }
            set
            {
                if (value != "A" && value != "B")
                {
                    throw new ArgumentException("Type of passenger must be 'A' or 'B'.");
                }
                typeOfPassenger = value;
            }
        }

        public int TimeArrives
        {
            get { return timeArrives; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Time arrives must be greater than or equal to 0.");
                }
                timeArrives = value;
            }
        }

        public int Destination
        {
            get { return destination; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Destination must be greater than or equal to 0.");
                }
                destination = value;
            }
        }

        public int Origin
        {
            get { return origin; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Origin must be greater than or equal to 0.");
                }
                origin = value;
            }
        }

        public int CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Current position must be greater than or equal to 0.");
                }
                currentPosition = value;
            }
        }

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
        }
    }
}
