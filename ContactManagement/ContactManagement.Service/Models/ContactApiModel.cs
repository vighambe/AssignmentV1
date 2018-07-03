using ContactManagement.Lib.Model;
using ContactManagement.Service.Validation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactManagement.Service.Models
{
    public class ContactApiModel : IValidatableObject
    {
        private static string[] _phonePatterns = new string[] { 
                                                            @"^[0-9]{10}$",
                                                            @"^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$",
                                                            @"^[0-9]{3}-[0-9]{4}-[0-9]{4}$",
                                                           };

        

        const string _memberRequireErrorMessage = "The {0} can not be empty.";
        const string _lengthErrorMessage = "The {0} field must have minimum {1} and maximum {2} character!";
        const string _invalidEmailErrorMessage = "Invalid email address.";
        const string _phoneNumberInvalidError = "Invalid phone number.";

        List<ValidationResult> result;

        public ContactApiModel()
        {
            result = new List<ValidationResult>();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "First name can not be empty.")]//,ErrorMessageResourceName = "FirstName", ErrorMessageResourceType =typeof(String))]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name can not be empty.")]//, ErrorMessageResourceName = "LastName", ErrorMessageResourceType = typeof(String))]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address can not be empty.")]//, ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(String))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public ContactStatus Status { get; set; }// (Possible values: Active/Inactive)

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (String.IsNullOrEmpty(FirstName) )
                result.Add(GetEmptyErrorValidationResult("FirstName"));
            else if(FirstName.Length < 3 || FirstName.Length > 20  )
                result.Add(GetLengthErrorValidationResult("FirstName",3,100));
            if (String.IsNullOrEmpty(LastName))
                result.Add(GetEmptyErrorValidationResult("LastName"));
            else if (LastName.Length < 3 || LastName.Length > 20)
                result.Add(GetLengthErrorValidationResult("LastName", 3, 100));
            if (String.IsNullOrEmpty(Email))
                result.Add(GetEmptyErrorValidationResult("Email"));
            else if (Email.Length < 3 || Email.Length > 120)
                result.Add(GetLengthErrorValidationResult("Email", 3, 120));
            try
            {
                MailAddress mailAddress = new MailAddress(Email);
            }
            catch
            {
                ValidationResult emailResult = new ValidationResult(_invalidEmailErrorMessage, new List<string>() { "Email" });
                result.Add(emailResult);
            }

            if(!String.IsNullOrWhiteSpace(PhoneNumber) && !ValidatePhoneNumber())
            {
                ValidationResult phoneResult = new ValidationResult(_phoneNumberInvalidError, new List<string>() { "PhoneNumber" });
                result.Add(phoneResult);
            }
            
            return (result);
        }

        private static string MakeCombinedPhonePattern()
        {
            return string.Join("|", _phonePatterns.Select(item => "(" + item + ")"));
        }

        /*
            9011534816
            +91 11 41653155
            011-2647-0969 */

        private bool ValidatePhoneNumber()
        {
            bool result = false;

            string phoneNumberPattern = MakeCombinedPhonePattern();

            result = Regex.IsMatch(PhoneNumber, phoneNumberPattern);

            return (result);
        }


        private ValidationResult GetEmptyErrorValidationResult(string memberName)
        {
            string error = String.Format(_memberRequireErrorMessage, memberName);
            ValidationResult result = new ValidationResult(error, new List<string>() { memberName });
            return (result);
        }

        private ValidationResult GetLengthErrorValidationResult(string memberName, int min , int max)
        {
            string error = String.Format(_lengthErrorMessage, memberName,min,max);
            ValidationResult result = new ValidationResult(error, new List<string>() { memberName });
            return (result);
        }

        

    }
}
