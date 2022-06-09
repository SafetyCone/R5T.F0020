using System;


namespace R5T.F0020
{
    public static class Instances
    {
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectFileXmlOperator ProjectFileXmlOperator { get; } = F0020.ProjectFileXmlOperator.Instance;
        public static IProjectFileXPathOperator ProjectFileXPathOperator { get; } = F0020.ProjectFileXPathOperator.Instance;
    }
}