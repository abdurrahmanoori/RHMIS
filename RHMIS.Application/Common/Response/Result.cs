namespace RHMIS.Application.Common.Response;

public class Result
{
    public long Id { get; set; }

    public bool Success { get; set; } = true;

    public string? Message { get; set; }

    public List<string>? Errors { get; set; }
    //public List<ValidationError>? Errorssss { get; set; }

}

public class Result<T>
{
    public long Id { get; set; }

    public T? Response { get; set; }

    public bool Success { get; set; } = true;

    public string? Message { get; set; }

    public List<ValidationError>? Errors { get; set; }

    public static Result<T> SuccessResult(T? result, string? message = null)
    {
        return new Result<T>
        {
            Success = true,
            Response = result,
        };
    }
    public static Result<T> NotFoundResult(int? id = null)
    {
        return new Result<T>
        {
            Success = false,
            Message = $"Entity with the {id} not found",
        };
    }
    public static Result<T> EmptyResult(string? entity = null)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        Code = "EmptyList",
                        Description = $"The list of entity {entity} is empty.",
                        Property = "List"
                    }
                }
        };
    }

    public static Result<T> FailureResult(string code, string description)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new List<ValidationError>
                {
                    new ValidationError { Code = code, Description = description }
                },
        };
    }
    public static Result<T> FailureResult(string description)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new List<ValidationError>
                {
                    new() {Description = description }
                },
        };
    }

    public static Result<T> WithError(ValidationError error)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new List<ValidationError> { error },
        };
    }

    public static Result<T> WithErrors(List<ValidationError> errors)
    {
        return new Result<T>
        {
            Success = false,
            Errors = errors,
        };
    }

}
public class ValidationError
{
    public override string ToString()
    {
        return $"Code: {this.Code}, Property: {this.Property}, Description: {this.Description}";
    }

    public string? Code { get; set; }

    public string? Property { get; set; }

    public string? Description { get; set; }
    //public List<string>? Descriptions { get; set; }
}
