using System;

namespace Yburn.Interfaces
{
    public delegate void JobStartEventHandler(
        object sender,
        JobStartEventArgs e
        );

    public class JobStartEventArgs : EventArgs
    {
        public JobStartEventArgs()
            : base()
        {
        }
    }

    public delegate void JobFinishedEventHandler(
        object sender,
        JobFinishedEventArgs e
        );

    public class JobFinishedEventArgs : EventArgs
    {
        public JobFinishedEventArgs()
            : base()
        {
        }
    }

    public delegate void JobFailureEventHandler(
        object sender,
        JobFailureEventArgs args
        );

    public class JobFailureEventArgs : EventArgs
    {
        public JobFailureEventArgs()
            : base()
        {
        }

        public Exception Exception;
    }
}