using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LibraryEntitySystem;


namespace NotificationService
{


    public interface INotificationService
    {
        public void SendNotificationOnSuccess(LibraryEntity Object, string ObjectType, string Action, string SupportContact);
        public void SendNotificationOnFailure(LibraryEntity Object, string ObjectType, string Action, string SupportContact);
    }



    public class EmailNotificationService : INotificationService
    {


        // success email message for a library action. - //*(To deal with the message only)*
        public string SuccessEmailMessage(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            string message = $"New {ObjectType} Named '{Object.Name}' has been successfully {Action} to the Library.\n\n If you have any queries or feedback, please contact our support team at {SupportContact}.\n";
            return message;
        }


        // Failure email message for a library action.
        public string FailureEmailMessage(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            string message = $"We encountered an issue {Action} {ObjectType} Named '{Object.Name}'. Please review the input data. For more help, please contact {SupportContact}.\n";
            return message;
        }


        // Sending a notification to user - //*(To deal with the way of sending)*
        public void SendNotificationOnSuccess(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            Console.WriteLine($"{SuccessEmailMessage(Object, ObjectType, Action, SupportContact)}");
        }


        // Sending a notification to user
        public void SendNotificationOnFailure(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            Console.WriteLine($"{FailureEmailMessage(Object, ObjectType, Action, SupportContact)}");
        }


    }



    public class SMSNotificationService : INotificationService
    {
        
        // success SMS message for a library action.
        private string SuccessSMSMessage(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            string message = $"The {ObjectType} '{Object.Name}' {Action} successfully. Thank you!\n";
            return message;
        }


        // Failure SMS message for a library action.
        private string FailureSMSMessage(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            string message = $"Error {Action} {ObjectType} '{Object.Name}'. please contact {SupportContact}.\n";
            return message;
        }


        public void SendNotificationOnSuccess(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            Console.WriteLine(SuccessSMSMessage(Object, ObjectType, Action, SupportContact));
        }


        public void SendNotificationOnFailure(LibraryEntity Object, string ObjectType, string Action, string SupportContact)
        {
            Console.WriteLine(FailureSMSMessage(Object, ObjectType, Action, SupportContact));
        }


    }
}