namespace MyEshop.Data.Repositories
{
    public interface IUserRepository
    {
        #region Login
        bool IsExistUserByEmail(string email);
        Users GetUserForLogin(string email, string password);

        #endregion
        #region User
        void UpdateUser(Users user);

        #endregion
        #region Register
        void AddUser(RegisterViewModel register);

        #endregion
        #region User Panel
        Users GetUserByUserId(int userId);
        ProfileInformationViewModel GetUserInformation(int userId);
        EditProfileViewModel GetDataForEditUserProfile(int userId);
        void EditProfile(int userId, EditProfileViewModel editProfileViewModel);

        #endregion
    }
}
