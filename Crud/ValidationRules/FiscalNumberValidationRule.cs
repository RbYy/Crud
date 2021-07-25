using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Crud.ValidationRules
{
	public class FiscalNumberValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			bool canConvert = int.TryParse(value as string, out int result) && result < 100000000;
			return new ValidationResult(canConvert, "Ni veljavna davčna številka");
		}
	}
}
