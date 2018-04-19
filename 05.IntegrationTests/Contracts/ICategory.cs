using System;
using System.Collections.Generic;
using System.Text;

public interface ICategory
{
    string Name { get; }
    IList<IUser> Users { get; }
    IList<ICategory> Children { get; }

    void AddChild(ICategory child);
    void RemoveChild(string name);
    void AddUser(IUser user);
}