public class PriorityQueue
{
    private readonly List<PriorityItem> _queue = [];

    /// <summary>
    /// Add a new value to the queue with an associated priority.  The
    /// node is always added to the back of the queue regardless of 
    /// the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        _queue.Add(new PriorityItem(value, priority));
    }

    public string Dequeue()
    {
        // Find the index of the item with the highest priority to remove
        var highPriorityIndex = 0;
        var allPrioritiesEqual = true;
        for (int index = 1; index < _queue.Count; index++)
        {
            if (_queue[index].Priority != _queue[0].Priority)
                allPrioritiesEqual = false;

            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
                highPriorityIndex = index;
        }

        if (allPrioritiesEqual)
            highPriorityIndex = 0;

        // Remove and return the item with the highest priority
        var item = _queue[highPriorityIndex];
        _queue.RemoveAt(highPriorityIndex);
        return item.Value;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}