using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue multiple numeric values and verify highest priority is dequeued first
    // Expected Result: Dequeue returns values in order of highest to lowest priority (5, 3, 1)
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("1", 1);
        priorityQueue.Enqueue("3", 3);
        priorityQueue.Enqueue("5", 5);
        priorityQueue.Enqueue("2", 2);
        priorityQueue.Enqueue("4", 4);


        Assert.AreEqual<string>("5", priorityQueue.Dequeue());
        Assert.AreEqual<string>("4", priorityQueue.Dequeue());
        Assert.AreEqual<string>("3", priorityQueue.Dequeue());
        Assert.AreEqual<string>("2", priorityQueue.Dequeue());
        Assert.AreEqual<string>("1", priorityQueue.Dequeue());

        // Display the results of the test case
        System.Console.WriteLine("TestPriorityQueue_1 passed successfully.");  
    
    }

    [TestMethod]
    // Scenario: Enqueue multiple numeric values with same priority
    // Expected Result: Items with same priority dequeue in FIFO order (order of insertion)
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("10", 5);
        priorityQueue.Enqueue("20", 5);
        priorityQueue.Enqueue("30", 5);

        Assert.AreEqual<string>("10", priorityQueue.Dequeue());
        Assert.AreEqual<string>("20", priorityQueue.Dequeue());
        Assert.AreEqual<string>("30", priorityQueue.Dequeue());

        // Display the results of the test case
        System.Console.WriteLine("TestPriorityQueue_2 passed successfully.");
    }

    [TestMethod]
    // Scenario: Enqueue mixed numeric priorities
    // Expected Result: Highest priority values dequeue first, then lower priorities
    // Defect(s) Found:
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("100", 2);
        priorityQueue.Enqueue("50", 5);
        priorityQueue.Enqueue("75", 10);

        Assert.AreEqual<string>("75", priorityQueue.Dequeue());
        Assert.AreEqual<string>("50", priorityQueue.Dequeue());
        Assert.AreEqual<string>("100", priorityQueue.Dequeue());

        System.Console.WriteLine("TestPriorityQueue_3 passed successfully.");
    }

    // Add more test cases as needed below.
}