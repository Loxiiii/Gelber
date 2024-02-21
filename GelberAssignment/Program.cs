using System;
using Helpers;
using System.Linq;
using GelberAssignment.Classes;

namespace GelberAssignment;

public class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("What example do you want to read?");
        string example = Console.ReadLine();
        Console.WriteLine("You chose: {0}", example);

        // Initialize the reader
        Reader reader = new Reader();

        // Read the file
        string content = reader.getContent(example);

        // Split the file in lines
        string[][] elements = ParseFile(content);

        // Create Railway
        Railway rw = CreateRailway(elements);

        // Create Passenger List
        List<Passenger> passengers = CreatePassengerList(elements);            

        // Print the number of existing passengers and check input information
        bool passengerInfoIsCorrect = VerifyRead(rw, passengers);

        // If error in passenger input, return 0
        if (passengerInfoIsCorrect == false)
        {
            Console.WriteLine("Wrong passenger input, please fix the " +
                "passengers information in the .txt file to " +
                 "include correct data");
            return 0;
        }

        // Initialize t
        int t = 0;

        // Initialize number of passengers still not in destination
        int numPassengers = passengers.Count();

        // Initialize what happens at t = 0
        // Create initial trains
        createTrain(1, 0, ref rw);
        createTrain(rw.NumberOfStations, 0, ref rw);

        // Initialize passengersToUnboardList
        List<Passenger> passengersToUnboard = new List<Passenger>();
        List<Guid> guidsToUnboard = new List<Guid>();
        List<Passenger> boardablePassengers = new List<Passenger>();

        while (numPassengers > 0)
        {
            // Move trains
            if (t != 0)
            {
                foreach(Train train in rw.Trains)
                { 
                    train.MoveTrainAndPassengers(t, ref rw);
                }
            }

            // Unboard passengers at destination
            foreach(Train train in rw.Trains)
            {
                train.UnboardPassengers(ref rw, ref numPassengers);
            }

            // Remove trains
            rw.RemoveTrains();

            // Spawn trains
            if (t != 0 && t % rw.FrequencyDepart == 0)
            {
                createTrain(1, 0, ref rw);
                createTrain(rw.NumberOfStations, 0, ref rw);
            }
            // Spawn passengers
            CreatePassengersAtTimeT(passengers, t, ref rw);// create passengers at t

            // Board passengers in trains if unboarded
            foreach (Train train in rw.Trains
                .Where(t => t.Traveling == false))
            {
                // Check boardable passengers and order them by descending distance to destination
                boardablePassengers = train.CheckBoardablePassengers(rw);

                foreach(Passenger passenger in boardablePassengers)
                {
                    
                    train.BoardPassenger(passenger, rw);
                    if (train.Passengers.Count() == rw.CapacityOfTrains)
                    {
                        break;
                    }
                }

            }



            t++;
        }
        t--;
        Console.WriteLine("The total time is: " + t);
        return t;
    }

    private static void createTrain(int stationNumber,int timeOfCreation, ref Railway railway)
    {
        Train newTrain = new Train(stationNumber, timeOfCreation, railway.NumberOfStations);
        railway.Trains.Add(newTrain);
        
    }

    private static void CreatePassengersAtTimeT(List<Passenger> passengers, int t,
        ref Railway railway)
    {
        List<Passenger> newPassengers = passengers
                    .Where(passenger => passenger.TimeArrives == t).ToList();

        foreach (Passenger newPassenger in newPassengers)
        {
            //newPassenger.Exists = true;
            newPassenger.CurrentPosition = newPassenger.Origin;
            railway.PassengersInRailway.Add(newPassenger);
        }
    }

    private static bool VerifyRead(Railway rw, List<Passenger> passengers)
    {
        Console.WriteLine();
        Console.WriteLine("The railway has {0} stops, it takes {1} minutes to travel from one" +
            " station to another, trains depart every {2} minutes and the train capacity is {3} ",
            rw.NumberOfStations, rw.TimeToTravel, rw.FrequencyDepart, rw.CapacityOfTrains);
        Console.WriteLine();
        Console.WriteLine("There are {0} passengers", passengers.Count());
        Console.WriteLine();
        for (int i = 0; i < passengers.Count(); i++)
        {
            Console.WriteLine("Passenger {0} is of type {1}, arrives at time {2}, with destination" +
                " station {3}, and origin station {4}", i+1, passengers[i].TypeOfPassenger,
                passengers[i].TimeArrives, passengers[i].Destination, passengers[i].Origin);

            if ((passengers[i].TypeOfPassenger != "A" && passengers[i].TypeOfPassenger != "B") ||
                passengers[i].TimeArrives < 0 || passengers[i].Destination < 1 ||
                passengers[i].Destination > rw.NumberOfStations ||
                passengers[i].Origin < 1 ||
                passengers[i].Origin > rw.NumberOfStations)
            {
                return false;
            }
        }
        return true;
    }

    private static List<Passenger> CreatePassengerList(string[][] elements)
    {
        List<Passenger> passengers = new List<Passenger>();
        for (int j = 1; j < elements.Count(); j++)
        {
            // Create passenger
            Passenger passenger = new Passenger(elements[j][0],
                int.Parse(elements[j][1]),
                int.Parse(elements[j][2]),
                int.Parse(elements[j][3]));

            // Add passenger to passengers list
            passengers.Add(passenger);

        }
        return passengers;
    }

    private static Railway CreateRailway(string[][] elements)
    {
        return new Railway(int.Parse(elements[0][0]),
                    int.Parse(elements[0][1]),
                    int.Parse(elements[0][2]),
                    int.Parse(elements[0][3])
                    );
    }

    public static string[][] ParseFile(string content)
    {
        string[] lines = content.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        // Split each line in elements
        string[][] elements = lines.Select(line => line.Split(' ')).ToArray();

        return elements;
    }
}