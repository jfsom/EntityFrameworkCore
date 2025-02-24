using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the Connection String
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply global configurations

            // Set the default schema for the database to "Admin"
            modelBuilder.HasDefaultSchema("Admin");

            // Set default precision and scale for all decimal properties
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                // Select all properties from all entity types
                .SelectMany(t => t.GetProperties())
                // Filter properties to those of type decimal or nullable decimal
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // Set the precision to 18 (total number of digits)
                property.SetPrecision(18);

                // Set the scale to 3 (digits after the decimal point)
                property.SetScale(3);
            }

            // Set default max length for string properties
            // Loop through all entity types in the model
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get all properties of type string
                var stringProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string));

                // Set default max length for each string property if no max length is already defined
                foreach (var property in stringProperties)
                {
                    if (property.GetMaxLength() == null) // Check if the max length is not already set
                    {
                        property.SetMaxLength(200); // Set the default max length to 200 characters
                    }
                }
            }

            // Store enums as strings
            // Loop through all entity types in the model
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get all properties of type Enum (enumerations)
                var enumProperties = entityType.GetProperties()
                    .Where(p => p.ClrType.IsEnum);

                foreach (var property in enumProperties)
                {
                    // Get the CLR type of the enum
                    var enumType = property.ClrType;

                    // Dynamically Create a generic EnumToStringConverter for the specific enum type
                    var converterType = typeof(EnumToStringConverter<>).MakeGenericType(enumType);
                    var converter = Activator.CreateInstance(converterType) as ValueConverter;

                    // Apply the converter to the property if the instance was created successfully
                    if (converter != null)
                    {
                        property.SetValueConverter(converter);
                    }
                }
            }

            // Configure all string properties to be non-Unicode (varchar)
            // Loop through all entity types in the model
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get all properties of type string
                var stringProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string));

                // Set each string property to be non-Unicode (varchar) instead of Unicode (nvarchar)
                foreach (var property in stringProperties)
                {
                    property.SetIsUnicode(false); // Set property to varchar
                }
            }

            // Automatically set CreatedAt and UpdatedAt column values
            // Loop through all entity types in the model
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if the entity implements the ITimestampedEntity interface
                if (typeof(ITimestampedEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Configure the CreatedAt property to have a default value of the current UTC date/time
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()")  // SQL function to get the current UTC date/time
                        .ValueGeneratedOnAdd();              // Value is set when the entity is first added to the database

                    // Configure the UpdatedAt property to also default to the current UTC date/time
                    // This value will be updated on both creation and subsequent updates
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("UpdatedAt")
                        .HasDefaultValueSql("GETUTCDATE()")  // SQL function to get the current UTC date/time
                        .ValueGeneratedOnAddOrUpdate();      // Value is set on both add and update operations
                }
            }

            // Set delete behavior to Restrict
            // Loop through all entity types in the model
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) // Select all foreign keys across all entity types
            {
                // Set the delete behavior for each foreign key relationship to "Restrict"
                // This prevents cascading deletes, meaning that deleting a parent entity
                // will not automatically delete related child entities.
                // This means that the principal entity cannot be deleted if dependent entities exist.
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        // Define DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}