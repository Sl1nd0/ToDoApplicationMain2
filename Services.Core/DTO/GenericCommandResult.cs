namespace Services.Core.DTO
{
    public class GenericCommandResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
        public object Result { get; set; }
    }
}
