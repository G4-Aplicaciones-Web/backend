using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Shared.Infrastructure.Persistence.Configuration.Extensions;

/// <summary>
/// Model Builder extensions for configuring entity names to use snake case
/// </summary>
/// <remarks>
/// This class provides methods to convert entity table names, property column names, index names,
/// primary keys names, and foreign key constraint names to snake case
/// </remarks>
public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if(!string.IsNullOrEmpty(tableName))
                entity.SetTableName(tableName.ToPlural().ToSnakeCase());
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }
            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if(!string.IsNullOrEmpty(keyName))
                    key.SetName(keyName.ToSnakeCase());
            }
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if(!string.IsNullOrEmpty(foreignKeyName))
                    foreignKey.SetConstraintName(foreignKeyName.ToSnakeCase());
            }
            foreach (var index in entity.GetIndexes())
            {
                var indexDatabaseName = index.GetDatabaseName();
                if(!string.IsNullOrEmpty(indexDatabaseName))
                    index.SetDatabaseName(indexDatabaseName.ToSnakeCase());
            }
        }
    }
}