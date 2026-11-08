namespace GymManagementSystem.BusinessLogic
{


    public class Result
    {
        public bool IsSuccess { get; init; }

        public Dictionary<string, string[]> Errors { get; init; }
            = new();

        public static Result Success()
        {
            return new Result
            {
                IsSuccess = true
            };
        }

        public static Result Failure(
            Dictionary<string, string[]> errors)
        {
            return new Result
            {
                IsSuccess = false,
                Errors = errors
            };
        }

        public static Result Failure(
            string property,
            string error)
        {
            return Failure(new Dictionary<string, string[]>
            {
                [property] = new[] { error }
            });
        }
    }



}
