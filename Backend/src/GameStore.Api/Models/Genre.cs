using System;
using System.Security.Cryptography.X509Certificates;

namespace GameStore.Api.Models;

public class Genre
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
}
