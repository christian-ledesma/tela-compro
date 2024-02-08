﻿namespace TelaCompro.Application.Responses
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string? Error { get; set; }
        public static Result<T> Success(T? value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public static Result Success() => new() { IsSuccess = true };
        public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
    }
}
