using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IoC;

public static class ConfigureMongoDb
{
    public static void Configure(this IServiceCollection serviceCollection, string connectionString,string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        serviceCollection.AddSingleton(database);
        
    }
}