using System;

using R5T.T0142;


namespace R5T.F0020.Construction
{
    [DraftDataTypeMarker]
    public class ProjectFilesTuple
    {
        public string ProjectFilePath { get; set; }
        public string AssemblyFilePath { get; set; }
        public string DocumentationFilePath { get; set; }
    }
}
