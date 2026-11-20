using System;
using System.Collections.Generic;

namespace FrontierDispatch.Core
{
    // Done!
    // Assignment Phase 3. Implement as a binary min-heap.

    /// <summary>
    /// A minimum-priority queue implemented as a binary min-heap.
    ///
    /// Items with lower integer priority values should be returned before
    /// items with higher priority values.
    ///
    /// This class is intentionally incomplete in the assignment starter.
    /// </summary>
    public sealed class MinPriorityQueue<T>
    {
        // The list stores the physical representation of the binary heap.
        // Your implementation is responsible for preserving the min-heap invariant.
        private readonly List<Entry> _heap = new List<Entry>();

        /// <summary>
        /// Number of items currently stored in the queue.
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// Adds an item with the supplied priority.
        /// Lower priority values are removed before higher priority values.
        /// </summary>
        public void Enqueue(T item, int priority)
        {
            // Done!
            _heap.Add(new Entry(item, priority));
            int index = Count - 1;
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_heap[index].Priority >= _heap[parentIndex].Priority)
                {
                    break;
                }
                
                Entry entry = _heap[index];
                _heap[index] = _heap[parentIndex];
                _heap[parentIndex] = entry;
                index = parentIndex;
            }
        }

        /// <summary>
        /// Returns the minimum-priority item without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the queue is empty.
        /// </exception>
        public T Peek()
        {
            // Done!
            if (Count == 0) { throw new InvalidOperationException(); }
            return _heap[0].Item;
        }

        public int PeekPrio()
        {
            if (Count == 0) { throw new InvalidOperationException(); }
            return _heap[0].Priority;
        }

        /// <summary>
        /// Removes and returns the minimum-priority item.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the queue is empty.
        /// </exception>
        public T Dequeue()
        {
            // Working on this now
            if (Count == 0) { throw new InvalidOperationException("The frontier is empty"); }
            
            T result = _heap[0].Item;
            int last = Count - 1;

            if (last == 0)
            {
                _heap.RemoveAt(0);
                return result;
            }

            _heap[0] = _heap[last];
            _heap.RemoveAt(last);
            SiftDown(0);
            Console.WriteLine($"Dequeue {result}");
            return result;
        }
        private void SiftDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                if (left >= _heap.Count)
                {
                    return;
                }

                int right = left + 1;
                int smallerChild = left;

                if (right < _heap.Count && _heap[right].Priority < _heap[left].Priority)
                {
                    smallerChild = right;
                }

                if (_heap[index].Priority <= _heap[smallerChild].Priority)
                {
                    return;
                }

                Swap(index, smallerChild);
                index = smallerChild;
            }
        }

        private void Swap(int a, int b)
        {
            Entry temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }

        /// <summary>
        /// Stores one item together with the priority used by the heap.
        /// </summary>
        private readonly struct Entry
        {
            public Entry(T item, int priority)
            {
                Item = item;
                Priority = priority;
            }

            public T Item { get; }
            public int Priority { get; }
        }
    }
}
