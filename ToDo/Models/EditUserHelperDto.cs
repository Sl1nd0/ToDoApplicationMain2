namespace ToDo.Models
{
    public class EditUserHelperDto
    {
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public string ToDo { get; internal set; }
        public string ToDoTitle { get; internal set; }
        public int ToDoID { get; internal set; }
        public int UserID { get; internal set; }
    }
}
