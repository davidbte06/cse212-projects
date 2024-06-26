public static class Priority {
    public static void Test() {
        // Test Case 1
        // Scenario: Enqueue items with different priorities and dequeue them.
        // Expected Result: Items dequeued in order of highest priority.
        Console.WriteLine("Test Case 1");
        var priorityQueue = new PriorityQueue<string>();
        priorityQueue.Enqueue("Task A", 3);
        priorityQueue.Enqueue("Task B", 1);
        priorityQueue.Enqueue("Task C", 2);

        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task A" (priority 3)
        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task C" (priority 2)
        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task B" (priority 1)

        Console.WriteLine("---------");

        // Test Case 2
        // Scenario: Enqueue items with the same highest priority and dequeue them.
        // Expected Result: Items dequeued in the order they were enqueued.
        Console.WriteLine("Test Case 2");
        priorityQueue.Enqueue("Task X", 2);
        priorityQueue.Enqueue("Task Y", 2);
        priorityQueue.Enqueue("Task Z", 2);

        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task X" (priority 2, first enqueued)
        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task Y" (priority 2, next enqueued)
        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Task Z" (priority 2, last enqueued)

        Console.WriteLine("---------");

        // Test Case 3
        // Scenario: Dequeue from an empty queue.
        // Expected Result: Error message should be displayed.
        Console.WriteLine("Test Case 3");
        try {
            Console.WriteLine(priorityQueue.Dequeue());  // Expected: Exception: Queue is empty.
        } catch (InvalidOperationException ex) {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("---------");

        // Test Case 4
        // Scenario: Enqueue and dequeue a single item.
        // Expected Result: The item should be dequeued correctly.
        Console.WriteLine("Test Case 4");
        priorityQueue.Enqueue("Single Task", 5);

        Console.WriteLine(priorityQueue.Dequeue());  // Expected: "Single Task" (priority 5)

        Console.WriteLine("---------");
    }

    public class PriorityQueue<T>
    {
        private List<(T item, int priority)> queue;

        public PriorityQueue()
        {
            queue = new List<(T, int)>();
        }

        public void Enqueue(T item, int priority)
        {
            queue.Add((item, priority));
        }

        public T Dequeue()
        {
            if (queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty."); // Better exception handling
            }

            int highestPriorityIndex = 0;
            for (int i = 1; i < queue.Count; i++)
            {
                if (queue[i].priority > queue[highestPriorityIndex].priority)
                {
                    highestPriorityIndex = i;
                }
            }

            T highestPriorityItem = queue[highestPriorityIndex].item;
            queue.RemoveAt(highestPriorityIndex);
            return highestPriorityItem;
        }

        public int Count
        {
            get { return queue.Count; }
        }
    }
}
