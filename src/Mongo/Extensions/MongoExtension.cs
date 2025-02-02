﻿using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mongo.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Mongo.Extensions;
public static class MongoExtension
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterMongoMappings();
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        services.AddSingleton<IMongoService, MongoService>();
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRepository<BookReview>, BookRepository>();
        services.AddTransient<IRepository<User>, UserRepository>();
        return services;
    }

    private static void RegisterMongoMappings()
    {
        BsonClassMap.RegisterClassMap<BookReview>(map =>
        {
            map.AutoMap();
            map.MapProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<User>(map =>
        {
            map.AutoMap();
            map.MapProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
        });

    }
}