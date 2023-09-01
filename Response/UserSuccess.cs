namespace Event_Management.Responses{
    public class UserSuccess{

        public int Code {get; set; } 
        public string Message {get; set; } 

        public UserSuccess(int c, string m)
        {
            this.Code = c;
            this.Message = m;
        }
    }
}