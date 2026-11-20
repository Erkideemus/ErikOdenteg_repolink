using Codice.Client.BaseCommands;
using PlasticPipe.PlasticProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace FrontierDispatch.Core
{
    // Assignment Phase 4.

    /// <summary>
    /// Finds minimum-total-cost routes through a Frontier Dispatch world
    /// using Dijkstra's shortest-path algorithm.
    ///
    /// This class is intentionally incomplete in the assignment starter.
    /// It should eventually use the student's prepared world representation
    /// and MinPriorityQueue implementation.
    /// </summary>
    public sealed class DijkstraRouteFinder
    {
        private readonly WorldData _world;

        /// <summary>
        /// Constructor. Creates a route finder operating on the supplied world.
        /// </summary>
        public DijkstraRouteFinder(WorldData world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
        }
        /// <summary>
        /// Finds a minimum-total-travel-cost route from the start location
        /// to the goal location.
        ///
        /// A successful RouteResult should contain the route as location IDs,
        /// including both the start and goal locations.
        /// </summary>
        public RouteResult FindRoute(int startLocationId, int goalLocationId)
        {
            // Done kind of
            IReadOnlyList<Location> locations = _world.Locations;
            IReadOnlyList<Connection> connections = _world.Connections;
            bool switch1 = false;
            bool switch2 = false;
            Location startLocale;
            Location endLocale;
            foreach (var location in locations)
            {
                if (location.Id == startLocationId) { switch1 = true; startLocale = location; }
                if (location.Id == goalLocationId) { switch2 = true; endLocale = location; }
                if (switch1 == true && switch2 == true) { break; }
            }
            if (switch1 == false || switch2 == false) { throw new ArgumentNullException("Locations not in world"); }

            var bestCost = new Dictionary<int, int>();
            var cameFrom = new Dictionary<int, int>();
            var frontier = new MinPriorityQueue<int>();

            bestCost[startLocationId] = 0;
            frontier.Enqueue(startLocationId, 0);

            int expandedCount = 0;

            while (frontier.Count > 0)
            {
                int prio = frontier.PeekPrio();
                int cur = frontier.Dequeue();
                
                if (!bestCost.TryGetValue(cur, out int currentBest) || prio != currentBest)
                {
                    continue;
                }
                //Console.WriteLine($"Currently Visiting {cur}");
                expandedCount++;

                if (cur == goalLocationId)
                {
                    Stack<int> path = new Stack<int>();
                    int backlog = cur;

                    while (backlog != startLocationId)
                    {
                        path.Push(backlog);
                        backlog = cameFrom[backlog];
                    }
                    path.Push(backlog);
                    List<int> act = path.ToList();
                    //Console.WriteLine("Path:");
                    foreach (int i in act) { Console.WriteLine(i); }
                    return new RouteResult(true, act, bestCost[cur], expandedCount);
                }
                foreach (var con in connections)
                {
                    if (con.FromLocationId == cur)
                    {
                        //Console.WriteLine($"  Connection found: {con.FromLocationId} -> {con.ToLocationId}, Cost: {con.TravelCost}");
                        int candidate = checked(currentBest + con.TravelCost);
                        if (!bestCost.TryGetValue(con.ToLocationId, out int known) || candidate < known)
                        {
                            bestCost[con.ToLocationId] = candidate;
                            cameFrom[con.ToLocationId] = cur;
                            frontier.Enqueue(con.ToLocationId, candidate);
                        }
                    }
                }
            }
            return new RouteResult(false, new List<int>(), 0, 0);
        }
    }
}
