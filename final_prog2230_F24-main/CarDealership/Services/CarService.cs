using System;
using System.Collections.Generic;
using System.Linq;
using CarDealership.Data;
using CarDealership.Models;

namespace CarDealership.Services
{
    public class CarService
    {
        private readonly CarDealershipContext _context;

        public CarService(CarDealershipContext context)
        {
            _context = context;
        }

        // Add a new Car
        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        // Get a Car by ID
        public Car GetCarById(int carId)
        {
            return _context.Cars.FirstOrDefault(c => c.CarId == carId);
        }

        // Update an existing Car
        public void UpdateCar(Car car)
        {
            var existingCar = _context.Cars.FirstOrDefault(c => c.CarId == car.CarId);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.Price = car.Price;
                // Add any additional fields here
                _context.SaveChanges();
            }
        }

        // Delete a Car by ID
        public void DeleteCar(int carId)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        // Get all Cars
        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }
    }
}
