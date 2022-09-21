// <copyright file="Address.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.Domain.Entities.Abstractions;

namespace Uow.Domain.Entities;
public class Address : ValueObject
{
    public Address()
    {
        // parameterless constructor    
    }

    public Address(string country, string county, string city, string postalCode, string firstLine, string secondLine)
    {
        Country = country;
        County = county;
        City = city;
        PostalCode = postalCode;
        FirstLine = firstLine;
        SecondLine = secondLine;
    }

    public string Country { get; }

    public string County { get; }

    public string City { get; }

    public string PostalCode { get; }

    public string FirstLine { get; }

    public string SecondLine { get; }
}
