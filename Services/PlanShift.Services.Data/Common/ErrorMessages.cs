namespace PlanShift.Services.Common
{
    internal static class ErrorMessages
    {
        // Common
        internal const string NoInformationWasUpdated = "There wasn't found any new information to update";

        // UserService
        internal const string NameCanNotBeNull = "Name can't be null";
        internal const string NoSuchUserFound = "User with this {0} doesn't exists";

        // BusinessService
        internal const string OwnerCantBeNull = "Owner can't be null";

        // GroupService
        internal const string BusinessCanNotBeNull = "Business can't be null!";
        internal const string GroupWithThisNameAlreadyExists = "Group with name:{0} already exists!";
        internal const string GroupCantBeNull = "Group cannot be null!";
        internal const string NoSuchGroupFound = "No such group found!";
        internal const string UserCantBeNull = "User cannot be null!";

        // ShiftService
        internal const string ShiftCannotBeNull = "Shift can't be null!";
        internal const string NoSuchEmployeeParticipatesInTheGroup = "Employee: {0} was not found in group: {1}";
        internal const string IsNotManagement = "Employee: {0} does not have permission to finish the transaction.";

        // ChangeShiftService
        internal const string OnlyOriginalEmployeeCanChangeShift = "Only original employee can change shift.";
    }
}
