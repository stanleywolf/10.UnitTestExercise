using System;
using System.Collections.Generic;

public class ListIterator
{
    private List<string> collections;
    public int Index { get; private set; }
    public int EndIndex { get; private set; }

    public ListIterator(IEnumerable<string> collection)
    {
        this.NullValidation(collection);
        this.collections = new List<string>(collection);
        this.EndIndex = this.collections.Count - 1;
        this.Index = 0;
    }

    private void NullValidation(IEnumerable<string> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException();
        }
    }

    public bool Move()
    {
        if (this.Index == this.EndIndex)
        {
            return false;
        }
        else
        {
            this.Index++;
            return true;
        }
    }

    public bool HasNext()
    {
        if (this.Index == this.EndIndex)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string Print()
    {
        if (this.collections.Count == 0)
        {
            return"Invalid Operation!";
        }
        else
        {
            return this.collections[this.Index];
        }
        
    }
}