using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CaregoryController
{
    private HashSet<ICategory> categories;

    public CaregoryController()
    {
        this.categories = new HashSet<ICategory>();
    }

    public CaregoryController(IEnumerable<string> names)
        :this()
    {
        foreach (var name in names)
        {
            this.AddCategory(name);
        }
    }

    public void AddCategory(string name)
    {
        if (this.categories.Any(c => c.Name == name))
        {
            return;
        }
        this.categories.Add(new Category(name));
    }

    public void AddCategory(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            this.AddCategory(name);
        }
    }

    public void RemoveCategory(string name)
    {
        var categToRemove = this.categories.FirstOrDefault(c => c.Name == name);
        if (categToRemove != null)
        {
            this.categories.Remove(categToRemove);
        }
    }
   
    public void AddUser(ICategory category, IUser user) => category.AddUser(user);
}