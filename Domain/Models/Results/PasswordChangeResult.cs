namespace Domain.Models.Results
{
    public class PasswordChangeResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}