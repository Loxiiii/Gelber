using System;
using System.Collections.Generic;
using System.Linq;

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


        public void MoveTrainAndPassengers(int t, ref Railway rw)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while moving train and passengers: " + ex.Message);
            }
        }

        public void UnboardPassengers(ref Railway rw, ref int numPassengers)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while unboarding passengers: " + ex.Message);
            }
        }

        public List<Passenger> CheckBoardablePassengers(Railway rw)
        {
            try
            {
                return (List<Passenger>)rw.PassengersInRailway
                        .Where(p => p.Boarded == false && p.CurrentPosition == this.CurrentPosition &&
                        (this.Direction == "Right" && p.Destination > p.CurrentPosition ||
                        this.Direction == "Left" && p.Destination < p.CurrentPosition))
                        .OrderByDescending(p => Math.Abs(p.CurrentPosition - p.Destination))
                        .ThenBy(p => p.TypeOfPassenger).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while checking boardable passengers: " + ex.Message);
                return new List<Passenger>(); 
            }
        }

        public void BoardPassenger(Passenger passenger, Railway rw)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while boarding passengers: " + ex.Message);
            }
        }
    }
}
