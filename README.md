# a2-s3734720-s3596621
## Use of Business Objects
We make use of business objects for the purpose of keeping controllers lean in many places:
    - We pulled complex validation logic from controllers into view models, and imposed simpler validation using built-in or custom data annotation attributes, e.g. `AusState`. 
    - We arranged database interaction / EF Core / code that which directly makes use of `DbContext` into "Managers", and inject them into controllers using Asp.net Core Mvc's Dependency Injection feature.
    - We added extra properties to Models to deal with repeating small field transformations such as datetime conversion, string representation of integer IDs.
    - We added user-friendly string representation of `enum` types using extension methods.


## Reference
    - Week 8, "NorthWindWithPagingExample" by Matthew Bolger
    - "MvcMovie" from MSDN "Get Started with ASP.NET Core Mvc"
