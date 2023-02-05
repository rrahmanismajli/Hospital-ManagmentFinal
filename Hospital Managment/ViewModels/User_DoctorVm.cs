using Hospital_Managment.Models;

namespace Hospital_Managment.ViewModels
{
    public class User_DoctorVm
    {

        public UserFeedback?  _userFeed{ get; set; }
        public List<UserFeedback> UsersFeedback{ get; set; }
        public List<Doctor> DoctorsVM { get; set; }
    }
}
