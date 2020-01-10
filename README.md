# Net Core Distributed Cache

This repo serves as an example implementation of the IDistributedCache interface using MemoryCache and RedisCache. It also includes an example of a basic implementation of the CachedRepository using the decorator pattern.

## Branch Organization

#### master
API Final implementation including an implementation of the Cached Repository pattern built on top of Decorator pattern

#### distributed-cache-api
API using IDistributedCache with two different implementations, MemoryCache for development enrivonment and RedisCache for non-development environments

#### redis-api
API using Redis cache, no dependency injection

#### base-api
API without caching, starting point.

## Considerations

This API can be run either using docker or as a simple web api.

The docker version builds and runs the API as a container and also an instance of Redis. It asumes a network called backend exists in the local environment, you can create the aforementioned network via the following command:

    docker network create backend

The simple web api version requires access to a Redis instance, you can set the corresponding connection string in the appsettings.json file:

    "ConnectionStrings": {
        "Redis": "redis"
    }