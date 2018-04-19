using System;
using System.Collections.Generic;
using System.Text;

public class Person : IPerson
{
    public string Username { get; private set; }
    public long Id { get; private set; }

    public Person(long id, string username)
    {
        this.Id = id;
        this.Username = username;
    }
}