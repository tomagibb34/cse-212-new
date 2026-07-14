using System;
/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    /// <exception cref="ArgumentException">Thrown if the name is null or empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the turns is less than 0</exception>
    /// <exception cref="InvalidOperationException">Thrown if the queue is full</exception>
    /// 
    /// 
    public void AddPerson(string name, int turns)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
 
        var person = new Person(name, turns);
        int beforeLength = _people.Length;

        _people.Enqueue(person);

        if (_people.Length != beforeLength + 1)
        {
            throw new InvalidOperationException("Enqueue did not increment the queue length.");
        }
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left.  Note that a turns value of 0 or less means the 
    /// person has an infinite number of turns.  An error exception is thrown 
    /// if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        else
        {
            int beforeLength = _people.Length;
            Person person = _people.Dequeue();

            if (_people.Length != beforeLength - 1)
            {
                throw new InvalidOperationException("Dequeue did not decrement the queue length.");
            }

            if (person.Turns <= 0)
            {
                // Person has infinite turns, so we don't modify their turns and re-add them to the queue

                Console.WriteLine($"Person {person.Name} has {person.Turns}.");

                int beforeEnqueue = _people.Length;
                
                _people.Enqueue(person);

                if (_people.Length != beforeEnqueue + 1)
                {
                    throw new InvalidOperationException("Enqueue did not increment the queue length.");
                }
            }
            else
            {
                person.Turns -= 1;
                if (person.Turns > 0)
                {
                    int beforeEnqueue = _people.Length;

                    _people.Enqueue(person);
                    
                    if (_people.Length != beforeEnqueue + 1)
                    {
                        throw new InvalidOperationException("Enqueue did not increment the queue length.");
                    }
                }
            }
            return person;
        }
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}