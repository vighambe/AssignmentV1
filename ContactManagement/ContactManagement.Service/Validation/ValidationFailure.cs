using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Service.Validation
{

    public class ValidationFailure
    {
        //
        // Summary:
        //     Creates a new validation failure.
        public ValidationFailure(string propertyName, string error)
        {

        }
        //
        // Summary:
        //     Creates a new ValidationFailure.
        public ValidationFailure(string propertyName, string error, object attemptedValue)
        {

        }

        //
        // Summary:
        //     The name of the property.
        public string PropertyName { get; }

        //
        // Summary:
        //     The error message
        public string ErrorMessage { get; }
        //
        // Summary:
        //     The property value that caused the failure.
        public object AttemptedValue { get; }
        
        

        //
        // Summary:
        //     Creates a textual representation of the failure.
        public override string ToString()
        {
            return (base.ToString());
        }
    }
}
