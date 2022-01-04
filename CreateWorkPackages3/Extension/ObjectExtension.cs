using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateWorkPackages3.Extension
{
	public static class ObjectExtension
	{
		public static object GetPropValue(this object src, string propName)
		{
			return src.GetType().GetProperty(propName).GetValue(src, null);
		}
	}
}
