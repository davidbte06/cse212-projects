﻿public static class TakingTurns
{
    public static void Test()
    {
        // Test Cases

        // Test 1
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
        // run until the queue is empty
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        Console.WriteLine("Test 1");
        var players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 5);
        players.AddPerson("Sue", 3);
        // Console.WriteLine(players);    // This can be un-commented out for debug help
        while (players.Length > 0)
            players.GetNextPerson();
        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 2
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
        // After running 5 times, add George with 3 turns.  Run until the queue is empty.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
        Console.WriteLine("Test 2");
        players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 5);
        players.AddPerson("Sue", 3);
        for (int i = 0; i < 5; i++) {
            players.GetNextPerson();
            // Console.WriteLine(players);
        }

        players.AddPerson("George", 3);
        // Console.WriteLine(players);
        while (players.Length > 0)
            players.GetNextPerson();

        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 3
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
        // Run 10 times.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        Console.WriteLine("Test 3");
        players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 0);
        players.AddPerson("Sue", 3);
        // Console.WriteLine(players);
        for (int i = 0; i < 10; i++) {
            players.GetNextPerson();
            // Console.WriteLine(players);
        }
        // Defect(s) Found: None

        Console.WriteLine("---------");

         // Test 4
        // Scenario: Create a queue with the following people and turns: Tim (Forever), Sue (3)
        // Run 10 times.
        // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
        Console.WriteLine("Test 4");
        players = new TakingTurnsQueue();
        players.AddPerson("Tim", -3);
        players.AddPerson("Sue", 3);
        // Console.WriteLine(players);
        for (int i = 0; i < 10; i++) {
            players.GetNextPerson();
            // Console.WriteLine(players);
        }
        // Defect(s) Found: Tim appears more times than expected

        Console.WriteLine("---------");

        // Test 5
        // Scenario: Try to get the next person from an empty queue
        // Expected Result: Error message should be displayed
        Console.WriteLine("Test 5");
        players = new TakingTurnsQueue();
        players.GetNextPerson();
        // Defect(s) Found: The error message might need to be more detailed
    }

    public class TakingTurnsQueue
    {
        private Queue<Person> queue;

        public TakingTurnsQueue()
        {
            queue = new Queue<Person>();
        }

        public void AddPerson(string name, int turns)
        {
            queue.Enqueue(new Person(name, turns));
        }

        public void GetNextPerson()
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("Error: Queue is empty.");
                return;
            }

            Person currentPerson = queue.Dequeue();
            Console.WriteLine(currentPerson.Name);

            if (currentPerson.HasTurnsLeft())
            {
                currentPerson.DecreaseTurn();
                if (currentPerson.HasTurnsLeft())
                    queue.Enqueue(currentPerson);
            }
            else
            {
                // If turns are infinite (turns == 0), re-enqueue without decrementing
                queue.Enqueue(currentPerson);
            }
        }

        public int Length
        {
            get { return queue.Count; }
        }

        private class Person
        {
            public string Name { get; private set; }
            private int turns;

            public Person(string name, int turns)
            {
                Name = name;
                this.turns = turns;
            }

            public bool HasTurnsLeft()
            {
                return turns != 0;
            }

            public void DecreaseTurn()
            {
                if (turns > 0)
                    turns--;
            }
        }
    }
}
