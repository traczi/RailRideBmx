﻿namespace RailRideBMX.Middleware;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}