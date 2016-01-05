using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Interfaces
{
    public interface IValidationRules
    {
        // Property declaration:
        Boolean ValidationStatus { get; }
        List<String> ValidationMessage { get; }
        Hashtable ValidationErrors { get; }
        Object BusinessObject { set; }
    }
}
