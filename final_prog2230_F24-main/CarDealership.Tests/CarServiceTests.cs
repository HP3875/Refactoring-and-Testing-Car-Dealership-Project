using Xunit;
using Microsoft.EntityFrameworkCore;
using CarDealership.Data;
using CarDealership.Models;
using CarDealership.Services;
using System.Linq;

public class CarServiceTests
{
    
    private CarDealershipContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<CarDealershipContext>()
            .UseInMemoryDatabase("CarDealershipTestDb")
            .Options;

        return new CarDealershipContext(options);
    }

    [Fact]
    public void AddCar_SavesCarToDatabase()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        var car = new Car { Make = "Toyota", Model = "Camry", Year = 2022, Price = 25000 };

        
        service.AddCar(car);

        
        var savedCar = context.Cars.FirstOrDefault();
        Assert.NotNull(savedCar);
        Assert.Equal("Toyota", savedCar.Make);
    }

    [Fact]
    public void GetCarById_ReturnsCorrectCar()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        var car = new Car { CarId = 1, Make = "Honda", Model = "Civic", Year = 2020, Price = 20000 };
        context.Cars.Add(car);
        context.SaveChanges();

    
        var retrievedCar = service.GetCarById(1);

        
        Assert.NotNull(retrievedCar);
        Assert.Equal("Honda", retrievedCar.Make);
    }

    [Fact]
    public void GetCarById_ReturnsNullForNonExistentId()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        
        var retrievedCar = service.GetCarById(999);

        Assert.Null(retrievedCar);
    }

    [Fact]
    public void UpdateCar_ChangesCarDetails()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        var car = new Car { CarId = 1, Make = "Ford", Model = "Focus", Year = 2018, Price = 18000 };
        context.Cars.Add(car);
        context.SaveChanges();

        var updatedCar = new Car { CarId = 1, Make = "Ford", Model = "Fusion", Year = 2019, Price = 20000 };

        
        service.UpdateCar(updatedCar);

        
        var retrievedCar = context.Cars.FirstOrDefault(c => c.CarId == 1);
        Assert.NotNull(retrievedCar);
        Assert.Equal("Fusion", retrievedCar.Model);
        Assert.Equal(2019, retrievedCar.Year);
    }

    [Fact]
    public void UpdateCar_LeavesUnchangedPropertiesIntact()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        var car = new Car { CarId = 1, Make = "Ford", Model = "Escape", Year = 2015, Price = 15000 };
        context.Cars.Add(car);
        context.SaveChanges();

        var partialUpdateCar = new Car { CarId = 1, Model = "Explorer" };

        
        service.UpdateCar(partialUpdateCar);

        
        var retrievedCar = context.Cars.FirstOrDefault(c => c.CarId == 1);
        Assert.NotNull(retrievedCar);
        Assert.Equal("Ford", retrievedCar.Make); 
        Assert.Equal(2015, retrievedCar.Year);  
    }

    [Fact]
    public void DeleteCar_RemovesCarFromDatabase()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        var car = new Car { CarId = 1, Make = "Chevrolet", Model = "Malibu", Year = 2017, Price = 17000 };
        context.Cars.Add(car);
        context.SaveChanges();

        
        service.DeleteCar(1);

        
        var retrievedCar = context.Cars.FirstOrDefault(c => c.CarId == 1);
        Assert.Null(retrievedCar);
    }

    [Fact]
    public void DeleteCar_NoErrorWhenDeletingNonExistentCar()
    {
        
        var context = GetInMemoryDbContext();
        var service = new CarService(context);

        
        var exception = Record.Exception(() => service.DeleteCar(999));
        Assert.Null(exception); 
    }
}
