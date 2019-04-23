using System.Collections.Generic;

namespace FMR.Image.Business.Commands
{
    public class CommandResult
    {
        public bool Success
        {
            get { return Notifications == null || Notifications.Count == 0; }
        }

        public List<string> Notifications { get; } = new List<string>();
    }
}
