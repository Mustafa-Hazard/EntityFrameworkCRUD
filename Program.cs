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

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

try
{
    // ✅ Ensure database exists
    await db.Database.EnsureCreatedAsync();
    Console.WriteLine("✅ Database ready!\n");

    // ------------------------------------------------------
    // CREATE (Insert)
    // ------------------------------------------------------
    if (!db.Customers.Any())
    {
        Console.WriteLine("🟢 Adding new customers...");

        var customers = new[]
        {
            new Customer { FirstName = "Mustafa", LastName = "Iqbal", Email = "bscs2312292@szabist.pk", Password = "mustafa642", Address = "Clifton, Karachi", PhoneNumber = "033302479956" },
            new Customer { FirstName = "Talha", LastName = "Mustafa", Email = "talha.mustafa@gmail.com", Password = "talha123", Address = "Bahadurabad, Karachi", PhoneNumber = "03152369841" },
            new Customer { FirstName = "Eden", LastName = "Hazard", Email = "eden.hazard@hotmail.com", Password = "hazard10", Address = "Brussels, Belgium", PhoneNumber = "03212457896" },
            new Customer { FirstName = "Maria", LastName = "B", Email = "Maria@hotmail.com", Password = "Maria124", Address = "Oslo, Norway", PhoneNumber = "03214570396" },
            new Customer { FirstName = "Habiba", LastName = "Khan", Email = "habiba.khan@yahoo.com", Password = "habiba456", Address = "North Nazimabad, Karachi", PhoneNumber = "03312749865" }
        };

        db.Customers.AddRange(customers);
        await db.SaveChangesAsync();
        Console.WriteLine("✅ Customers added successfully!\n");
    }

    // ------------------------------------------------------
    // READ (Select)
    // ------------------------------------------------------
    Console.WriteLine("📋 All Customers:");
    var allCustomers = from c in db.Customers
                       select new { c.CustomerId, c.FirstName, c.LastName, c.Email };

    foreach (var c in allCustomers)
        Console.WriteLine($"   {c.CustomerId}: {c.FirstName} {c.LastName} ({c.Email})");

    // ------------------------------------------------------
    // UPDATE
    // ------------------------------------------------------
    Console.WriteLine("\n✏️ Updating Mustafa's Address...");
    var updateQuery = from c in db.Customers
                      where c.FirstName == "Mustafa"
                      select c;

    foreach (var c in updateQuery)
    {
        c.Address = "DHA Phase II, Karachi";
    }
    await db.SaveChangesAsync();
    Console.WriteLine("✅ Mustafa's address updated successfully!\n");

    // ------------------------------------------------------
    // DELETE
    // ------------------------------------------------------
    Console.WriteLine("🗑️ Deleting customer 'Maria'...");
    var deleteQuery = from c in db.Customers
                      where c.FirstName == "Maria"
                      select c;

    foreach (var c in deleteQuery)
    {
        db.Customers.Remove(c);
    }
    await db.SaveChangesAsync();
    Console.WriteLine("✅ Customer 'Maria' deleted successfully!\n");

    // ------------------------------------------------------
    // ADDITIONAL LINQ EXAMPLES (like your sir)
    // ------------------------------------------------------
    Console.WriteLine("🔍 Customers with Karachi addresses:");
    var karachiCustomers = from c in db.Customers
                           where c.Address.Contains("Karachi")
                           select c;
    foreach (var c in karachiCustomers)
        Console.WriteLine($"   {c.FirstName} {c.LastName} → {c.Address}");

    Console.WriteLine("\n📞 Customers ordered by name:");
    var orderedCustomers = db.Customers
        .OrderBy(c => c.FirstName)
        .Select(c => new { c.FirstName, c.LastName });
    foreach (var c in orderedCustomers)
        Console.WriteLine($"   {c.FirstName} {c.LastName}");

    Console.WriteLine("\n📈 Total number of customers:");
    var totalCount = db.Customers.Count();
    Console.WriteLine($"   Total: {totalCount}");

    Console.WriteLine("\n🏙️ Group customers by city name:");
    var groupedByCity = db.Customers
        .GroupBy(c => c.Address.Split(',').Last().Trim())
        .Select(g => new { City = g.Key, Count = g.Count() });
    foreach (var g in groupedByCity)
        Console.WriteLine($"   {g.City}: {g.Count} customer(s)");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
}

Console.WriteLine("\n✅ TechNova Customer Management (LINQ Version) Completed!");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
