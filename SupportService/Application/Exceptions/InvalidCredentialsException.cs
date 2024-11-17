﻿namespace Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid login or password")
    {
        
    }

}
