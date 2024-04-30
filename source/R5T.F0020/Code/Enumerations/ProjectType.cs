using System;

using R5T.T0142;


namespace R5T.F0020
{
    [DataTypeMarker]
    public enum ProjectType
    {
        /// <summary>
        /// Default is library.
        /// </summary>
        Library = 0,

        Console,
        Test,
        WebApplication,
    }
}
