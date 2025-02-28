namespace EFCoreCodeFirstDemo.Entities
{
    public static class DbInitializer
    {
        public static void Initialize(EFCoreDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if the database has been seeded
            if (context.Countries.Any() || context.States.Any() || context.Cities.Any())
            {
                Console.WriteLine("Database already seeded.");
                return;
            }

            using var transaction = context.Database.BeginTransaction();
            try
            {
                // Seed Countries
                var countries = new List<Country>
                {
                    new Country { CountryName = "India", CountryCode = "IND" },
                    new Country { CountryName = "Australia", CountryCode = "AUS" }
                };
                context.Countries.AddRange(countries);
                context.SaveChanges(); // Save to generate CountryIds

                // Seed States
                var states = new List<State>
                {
                    new State { StateName = "Odisha", CountryId = countries.Single(c => c.CountryName == "India").CountryId },
                    new State { StateName = "Delhi", CountryId = countries.Single(c => c.CountryName == "India").CountryId },
                    new State { StateName = "New South Wales", CountryId = countries.Single(c => c.CountryName == "Australia").CountryId }
                };
                context.States.AddRange(states);
                context.SaveChanges(); // Save to generate StateIds

                // Seed Cities
                var cities = new List<City>
                {
                    new City { CityName = "Bhubaneswar", StateId = states.Single(s => s.StateName == "Odisha").StateId },
                    new City { CityName = "Cuttack", StateId = states.Single(s => s.StateName == "Odisha").StateId },
                    new City { CityName = "New Delhi", StateId = states.Single(s => s.StateName == "Delhi").StateId },
                    new City { CityName = "Sydney", StateId = states.Single(s => s.StateName == "New South Wales").StateId }
                };
                context.Cities.AddRange(cities);
                context.SaveChanges();

                // Commit transaction
                transaction.Commit();

                Console.WriteLine("Database has been seeded successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction if any error occurs
                transaction.Rollback();
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
    }
}
