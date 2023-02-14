using System;


namespace R5T.F0020
{
    public class FrameworkNames : IFrameworkNames
    {
        #region Infrastructure

        public static IFrameworkNames Instance { get; } = new FrameworkNames();


        private FrameworkNames()
        {
        }

        #endregion
    }
}
