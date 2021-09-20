using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphSearch
{
    class Program
    {
        class Location
        {
            public string Name;
            public string Description;
            public List<Neighbor> Neighbors = new List<Neighbor>();
            public List<Path> ShortestPaths = new List<Path>();

            public override string ToString() => Name;
        }

        class Neighbor
        {
            public Location Location;
            public int Distance;
        }

        class Path
        {
            public Location Location;
            public int Distance;
            public List<string> StopNames = new List<string>();
        }

        static void Main(string[] args)
        {
            var winterfell = new Location { Name = "Winterfell", Description = "the capital of the Kingdom of the North" };
            var pyke = new Location { Name = "Pyke", Description = "the stronghold and seat of House Greyjoy" };
            var riverrun = new Location { Name = "Riverrun", Description = "a large castle located in the central-western part of the Riverlands" };
            var theTrident = new Location { Name = "The Trident", Description = "one of the largest and most well-known rivers on the continent of Westeros" };
            var kingsLanding = new Location { Name = "King's Landing", Description = "the capital, and largest city, of the Seven Kingdoms" };
            var highgarden = new Location { Name = "Highgarden", Description = "the seat of House Tyrell and the regional capital of the Reach" };

            var locations = new List<Location> { winterfell, pyke, riverrun, theTrident, kingsLanding, highgarden };

            void ConnectLocations(Location a, Location b, int distance)
            {
                a.Neighbors.Add(new Neighbor { Location = b, Distance = distance });
                b.Neighbors.Add(new Neighbor { Location = a, Distance = distance });
            }

            ConnectLocations(winterfell, pyke, 18);
            ConnectLocations(winterfell, theTrident, 10);
            ConnectLocations(kingsLanding, theTrident, 5);
            ConnectLocations(kingsLanding, riverrun, 25);
            ConnectLocations(kingsLanding, highgarden, 8);
            ConnectLocations(riverrun, pyke, 3);
            ConnectLocations(riverrun, theTrident, 2);
            ConnectLocations(riverrun, highgarden, 10);
            ConnectLocations(pyke, highgarden, 14);

            foreach (Location location in locations)
            {
                Dijkstra(locations, location);
            }

            Location currentLocation = winterfell;

            while (true)
            {
                Console.WriteLine($"Welcome to {currentLocation.Name}, {currentLocation.Description}.");
                Console.WriteLine();
                Console.WriteLine("Possible destinations are:");

                for (int i = 0; i < currentLocation.ShortestPaths.Count; i++)
                {
                    Path path = currentLocation.ShortestPaths[i];
                    Console.Write($"{i + 1}. {path.Location.Name} ({path.Distance}");

                    if (path.StopNames.Count > 0)
                    {
                        Console.Write($" via {String.Join(", ", path.StopNames)}");
                    }

                    Console.WriteLine(")");
                }

                Console.WriteLine();
                Console.WriteLine("Where do you want to travel?");
                string destinationInput = Console.ReadLine();
                int destinationNumber;

                if (!Int32.TryParse(destinationInput, out destinationNumber)) return;
                int destinationIndex = destinationNumber - 1;

                currentLocation = currentLocation.ShortestPaths[destinationIndex].Location;
                Console.WriteLine();
            }
        }

        static void Dijkstra(List<Location> graph, Location source)
        {
            var Q = new List<Location>();
            var dist = new Dictionary<Location, int>();
            var prev = new Dictionary<Location, Location>();

            foreach (Location v in graph)
            {
                dist[v] = Int32.MaxValue;
                prev[v] = null;
                Q.Add(v);
            }

            dist[source] = 0;

            while (Q.Count > 0)
            {
                /*
                int minDist = Int32.MaxValue;
                Location u = null;

                foreach (Location v in Q)
                {
                    if (dist[v] < minDist)
                    {
                        minDist = dist[v];
                        u = v;
                    }
                }*/

                Location u = Q.OrderBy((v) => dist[v]).First();

                Q.Remove(u);

                foreach (Neighbor d in u.Neighbors)
                {
                    Location v = d.Location;
                    if (!Q.Contains(v)) continue;

                    int alt = dist[u] + d.Distance;
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }

            foreach (Location otherLocation in graph)
            {
                if (otherLocation == source) continue;

                var path = new Path { Location = otherLocation, Distance = dist[otherLocation] };
                source.ShortestPaths.Add(path);

                Location stop = prev[otherLocation];
                while (stop != source)
                {
                    path.StopNames.Insert(0, stop.Name);
                    stop = prev[stop];
                }
            }

            source.ShortestPaths.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        }
    }
}
