using System;
using System.Collections.Generic;
using System.Text;

public class User:IUser
{

    private HashSet<ICategory> categories;
    private string name;

    public string Name => this.name;
    public IEnumerable<ICategory> Categories => this.categories as IReadOnlyCollection<ICategory>;

    public User(string name)
    {
        this.name = name;
        this.categories = new HashSet<ICategory>();
    }


    public void AddCategory(ICategory category)
    {
        this.categories.Add(category);
    }

    public void RemoveCategory(ICategory category)
    {
        this.categories.RemoveWhere(n => n.Name == category.Name);
    }
}