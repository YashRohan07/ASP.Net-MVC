using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    // A generic interface for CRUD operations for any entity (CLASS) in the data access layer.
    public interface IRepository<CLASS, ID>
    {
        // Adds a new entity of type CLASS.
        // CLASS represents the data type or entity (e.g., Employee).
        void Add(CLASS s);


        // Retrieve an entity by its unique identifier (ID).
        // ID represents the type of the identifier (e.g., int, string).
        CLASS Get(ID id);

        List<CLASS> Get();  // Retrieve all entities of type CLASS.
        void Delete(ID id);  // Delete an entity by its unique identifier (ID).
        void Update(CLASS s);  // Update an existing entity of type CLASS.
    }
}


/*
 
The `IRepository` interface defines a contract for CRUD operations on any entity type using generics.
It abstracts data access, allowing the same interface to be used across different entities (e.g., Employee, Product).
This promotes reusability and flexibility, enabling easy switching between different data storage methods without changing business logic.

 */
