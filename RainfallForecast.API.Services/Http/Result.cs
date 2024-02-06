﻿namespace RainfallForecast.API.Services.Http
{
    public class Result
    {
        public bool IsSuccess => Errors != null && !Errors.Any();
        public bool HasWarnings => Warnings != null && Warnings.Any();
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }

        public Result()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }

        public void AddWarning(string warning)
        {
            Warnings.Add(warning);
        }

        public void AddWarnings(IEnumerable<string> warnings)
        {
            Warnings.AddRange(warnings);
        }

        public void Merge(Result result)
        {
            Errors.AddRange(result.Errors);
            Warnings.AddRange(result.Warnings);
        }

        public string JoinErrors(string delimiter = ",")
        {
            return string.Join(delimiter, Errors);
        }

        public string JoinWarnings(string delimiter = ",")
        {
            return string.Join(delimiter, Warnings);
        }
    }

    public class Result<T> : Result
    {
        public Result()
        {
        }

        public Result(T value)
        {
            Value = value;
        }

        public Result(T value, List<string> errors)
        {
            Value = value;
            Errors = errors;
        }

        public T Value { get; set; }
    }
}
