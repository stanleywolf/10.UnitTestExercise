using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExDatabase
{
    private HashSet<IPerson> people;
    public int Count => this.people.Count;
    public ExDatabase()
    {
        this.people = new HashSet<IPerson>();
    }

    public ExDatabase(IEnumerable<IPerson> people) 
        :this()
    {
        if (people != null)
        {
            foreach (var person in people)
            {
                this.Add(person);
            }
        }
    }

    public void Add(IPerson person)
    {
        if (this.people.Any(p => p.Id == person.Id || p.Username == person.Username))
        {
            throw new InvalidOperationException();
        }
        this.people.Add(person);
    }

    public void Remove(IPerson person)
    {
        this.people.RemoveWhere(p => p.Id == person.Id && p.Username == person.Username);
    }

    public IPerson Find(long id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        var foundPersonById = this.people.FirstOrDefault(p => p.Id == id);
        if (foundPersonById == null)
        {
            throw new InvalidOperationException();
        }
        return foundPersonById;
    }

    public IPerson Find(string username)
    {
        if (username == null)
        {
            throw new ArgumentNullException();
        }
        var foundPeopleByUsername = this.people.FirstOrDefault(p => p.Username == username);
        if (foundPeopleByUsername == null)
        {
            throw new InvalidOperationException();
        }
        return foundPeopleByUsername;
    }

}
