using System;
using NUnit.Framework;
using FrontierDispatch.Core;
using System.Diagnostics;

namespace FrontierDispatch.Tests
{
    /// <summary>
    /// Small contract examples for later assignment phases.
    ///
    /// These are not an exhaustive assignment test suite.
    /// Students are expected to enable the relevant tests as they reach each
    /// phase and add meaningful tests of their own, including edge cases.
    /// </summary>
    public sealed class AssignmentStarterTests
    {
        // These tests document later assignment contracts without making the
        // untouched baseline red. Remove Ignore when you reach the named phase.

        /*[Test]
        public void PriorityQueue_EnqueueAndDequeue_ReturnsLowestPriorityFirst()
        {
            var queue = new MinPriorityQueue<string>();
            queue.Enqueue("expensive", 9);
            queue.Enqueue("cheap", 2);
            queue.Enqueue("medium", 5);
            Assert.That(queue.Dequeue(), Is.EqualTo("cheap"));
        }

        [Test]
        public void Dijkstra_StartEqualsGoal_ReturnsZeroCostRoute()
        {
            var finder = new DijkstraRouteFinder(WorldDatasetFactory.CreateSmall());
            RouteResult result = finder.FindRoute(0, 0);
            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(0));
            Assert.That(result.LocationIds, Is.EqualTo(new[] { 0 }));
        }

        [Test]
        public void Reachability_ZeroBudget_IncludesStartAtZeroCost()
        {
            var query = new ReachabilityQuery(WorldDatasetFactory.CreateSmall());
            var result = query.FindReachable(0, 0);
            Assert.That(result.ContainsKey(0), Is.True);
            Assert.That(result[0], Is.EqualTo(0));
        }*/

        [Test]
        public void FindLocationID()
        {
            WorldData world = WorldDatasetFactory.CreateMedium();
            var queries = new WorldQueries(world);

            Location? location = queries.FindLocationById(245);
            if (location != null){
                Console.WriteLine(location.Id);
            }
            else { Console.WriteLine("Could not find location"); }
        }

        /*[Test]
        public void Dijkstra_TimeTaker()
        {
            var sw = Stopwatch.StartNew();
            var finder = new DijkstraRouteFinder(WorldDatasetFactory.CreateLarge());
            RouteResult result = finder.FindRoute(0, 39999);
            sw.Stop();
            Console.WriteLine(result.ToString());
            Console.WriteLine($"Estimated time of {sw.ElapsedMilliseconds} ms");
        }*/
    }
}
