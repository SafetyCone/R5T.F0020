using System;
using System.Xml.Linq;

using R5T.F0020;


namespace System
{
    public static class ProjectExtensions
    {
        public static Project Modify(this Project project,
            Action<XElement> projectElementModifier)
        {
            Instances.ProjectOperator.Modify(project, projectElementModifier);

            return project;
        }
    }
}
