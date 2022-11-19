using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace R5T.F0020.Extensions
{
    public static class ProjectXElementExtensions
    {
        public static IEnumerable<XElement> ItemGroups(this XElement projectElement)
        {
            var output = Instances.ProjectXmlOperator.GetItemGroups(projectElement);
            return output;
        }
    }
}
