using System;


namespace BachelorProject.Exceptions
{
    class ElectrodeException : Exception
    {
        public ElectrodeException(string message) : base(message) {
        }
    }

    class RouteException : Exception
    {
        public RouteException(string message) : base(message) {
        }
    }
}
