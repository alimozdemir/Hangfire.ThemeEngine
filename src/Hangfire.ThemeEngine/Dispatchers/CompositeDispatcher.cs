using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire.Dashboard;

namespace Hangfire.ThemeEngine.Dispatchers
{
    /// <summary>
    /// Its copied from Hangfire.Console project
    /// Source: https://github.com/pieceofsummer/Hangfire.Console/blob/master/src/Hangfire.Console/Support/CompositeDispatcher.cs
    /// </summary>
    internal class CompositeDispatcher : IDashboardDispatcher
    {
        private readonly List<IDashboardDispatcher> _dispatchers;

        public CompositeDispatcher(params IDashboardDispatcher[] dispatchers)
        {
            _dispatchers = new List<IDashboardDispatcher>(dispatchers);
        }

        public void AddDispatcher(IDashboardDispatcher dispatcher)
        {
            if (dispatcher == null)
                throw new ArgumentNullException(nameof(dispatcher));

            _dispatchers.Add(dispatcher);
        }

        public async Task Dispatch(DashboardContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (_dispatchers.Count == 0)
                throw new InvalidOperationException("CompositeDispatcher should contain at least one dispatcher");

            foreach (var dispatcher in _dispatchers)
            {
                await dispatcher.Dispatch(context);
            }
        }
    }
}