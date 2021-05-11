# About

**WIP**

Code samples for using [.Find method](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.find?view=efcore-3.1) for Entity Framework Core.

See also [repository for .NET Core](https://github.com/karenpayneoregon/moving-to-net5-version1)

<table width="800px">
    <tr>
        <th></th>
        <th>Single()</th>
        <th>SingleOrDefault()</th>
        <th>First()</th>
        <th>FirstOrDefault()</th>
        <th>Find()</th>
    </tr>
    <tr>
        <td valign="top"><strong>Description</strong></td>
        <td valign="top">Returns a single, specific element of a sequence</td>
        <td valign="top">Returns a single, specific element of a sequence, or a default value if that element is not found</td>
        <td valign="top">Returns the first element of a sequence, or a default value if no element is found</td>
        <td valign="top">Returns the first element of a sequence, or a default value if no element is found</td>
        <td valign="top">Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned</td>
    </tr>
    <tr>
        <td valign="top"><strong>Exception thrown when</strong></td>
        <td valign="top">There are 0 or more than 1 elements in the result</td>
        <td valign="top">Returns a single, specific element of a sequence, or a default value if that element is not found</td>
        <td valign="top">There are no elements in the result</td>
        <td valign="top">There are no elements in the result</td>
        <td valign="top">Not found</td>
    </tr>
     <tr>
            <td valign="top"><strong>When to use</strong></td>
            <td valign="top">If exactly 1 element is expected; not 0 or more than 1</td>
            <td valign="top">When 0 or 1 elements are expected</td>
            <td valign="top">When more than 1 element is expected and you want only the first</td>
            <td valign="top">When more than 1 element is expected and you want only the first. Also it is ok for the result to be empty</td>
            <td valign="top">It’s a DbSet method and executes immediately</td>
        </tr>
</table>



# Requires
* Microsoft Visual Studio 2017 or higher
* SQL Server Management Studio
* Intermediate level knowledge 
  * Working with Entity Framework Core
  * Working with C#


