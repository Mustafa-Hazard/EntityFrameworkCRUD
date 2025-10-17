using Connection.Model;
using Connection.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer("Data Source=LENOVO-IDEAPAD5\\LENOVOIDEAPAD5;Initial Catalog=TechNova;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"));
    })
    .Build();


// ------------------------------------------------------
// CREATE OPERATION
// ------------------------------------------------------
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    var customers = new[]
    {
        new Customer
        {
            FirstName = "Mustafa",
            LastName = "Iqbal",
            Email = "bscs2312292@szabist.pk",
            Password = "mustafa642",
            Address = "Clifton, Karachi",
            PhoneNumber = "033302479956"
        },
        new Customer
        {
            FirstName = "Talha",
            LastName = "Mustafa",
            Email = "talha.mustafa@gmail.com",
            Password = "talha123",
            Address = "Gulshan-e-Iqbal, Karachi",
            PhoneNumber = "03152369841"
        },
        new Customer
        {
            FirstName = "Eden",
            LastName = "Hazard",
            Email = "eden.hazard@hotmail.com",
            Password = "hazard10",
            Address = "Brussels, Belgium",
            PhoneNumber = "03212457896"
        },
        new Customer
        {
            FirstName = "Maria",
            LastName = "B",
            Email = "Maria@hotmail.com",
            Password = "Maria124",
            Address = "Oslo, Norway",
            PhoneNumber = "03214570396"
        },
        new Customer
        {
            FirstName = "Habiba",
            LastName = "Khan",
            Email = "habiba.khan@yahoo.com",
            Password = "habiba456",
            Address = "North Nazimabad, Karachi",
            PhoneNumber = "03312749865"
        }
    };

    db.Customers.AddRange(customers);
    db.SaveChanges();
    Console.WriteLine("Customers added successfully!");
}


// ------------------------------------------------------
// READ OPERATION
// ------------------------------------------------------
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var customers = db.Customers.ToList();

    Console.WriteLine("\nAll Customers:");
    foreach (var c in customers)
    {
        Console.WriteLine($"{c.CustomerId} - {c.FirstName} {c.LastName} - {c.Email}");
    }
}


// ------------------------------------------------------
// UPDATE OPERATION
// ------------------------------------------------------
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var customer = db.Customers.FirstOrDefault(c => c.FirstName == "Mustafa");

    if (customer != null)
    {
        customer.Address = "DHA Phase II, Karachi";
        db.SaveChanges();
        Console.WriteLine("\nCustomer updated successfully!");
    }
    else
    {
        Console.WriteLine("\nCustomer not found!");
    }
}


// ------------------------------------------------------
// DELETE OPERATION
// ------------------------------------------------------
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var customer = db.Customers.FirstOrDefault(c => c.FirstName == "Maria");

    if (customer != null)
    {
        db.Customers.Remove(customer);
        db.SaveChanges();
        Console.WriteLine("\nCustomer deleted successfully!");
    }
    else
    {
        Console.WriteLine("\nCustomer not found!");
    }
}
