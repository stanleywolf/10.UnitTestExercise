using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Category:ICategory
{
    private string name;
    private IList<IUser> users;
    private IList<ICategory> children;

    public string Name
    {
        get { return this.name; }
        private set {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Name cannot be null or empry");
            }
            this.name = value;
        }
    }
    public IList<IUser> Users { get; private set; }
    public IList<ICategory> Children { get; private set; }

    public Category(string name)
    {
        this.Name = name;
        this.Users = new List<IUser>();
        this.Children = new List<ICategory>();
    }


    public void AddChild(ICategory child)
    {
        this.Children.Add(child);
    }

    public void RemoveChild(string name)
    {
        var categoryToRemove = this.Children.FirstOrDefault(c => c.Name == name);
        this.Children.Remove(categoryToRemove);
    }

    public void AddUser(IUser user)
    {
        this.Users.Add(user);
        user.AddCategory(this);
    }
}