namespace PlanShift.Services.Data.Common
{
    internal static class SuccessfulMessages
    {
        // UserService
        internal const string CreatedUserSuccessfully = "{0} was created successfully!";
        internal const string UserWasUpdated = "User {0} was updated successfully!";
        internal const string UserWasDeletedSuccessfully = "User was deleted successfully!";

        // BusinessService
        internal const string BusinessCreatedSuccessfully = "Business {0} was created successfully!";
        internal const string BusinessUpdatedSuccessfully = "Business {0} was updated successfully!";

        // GroupService
        internal const string GroupCreatedSuccessfully = "{0} group for {1} was created successfully!";
        internal const string GroupWasUpdatedSuccessfully = "Group {0} was updated successfully!";
        internal const string GroupWasDeletedSuccessfully = "Group {0} was deleted successfully!";
        internal const string EmployeeWasAddedSuccessfully = "Employee {0} was added successfully in group:{1} at position{2}";

        // ShiftService
        internal const string ShiftCreatedSuccessfully = "New Shift for group: {0} was created!";
        internal const string ShiftDeletedSuccessfully = "Shift was deleted successfully!";
        internal const string ShiftWasSuccessfullyGivenToEmployee = "Shift with id:{0} was successfully given to employee:{1}";

        // ShiftChange
        internal const string ShiftChangeCreatedSuccessfully = "New shift change created successfully!";
        internal const string ShiftChangeAccepted = "Shift change was accepted by {0}";
        internal const string ShiftChangeDeclined = "Shift change was declined by {0}";
    }
}
