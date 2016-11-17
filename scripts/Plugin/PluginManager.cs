using PluginCore;
using Reactive.Framework.Error;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Reactive.Plugin
{
    /// <summary>
    /// Handles the loading and management of plugins. Using Managed Extensibility Framework (MEF)
    /// </summary>
    public class PluginManager
    {
        internal string pluginPath = App.StartupPath + "\\plugins";

        /// <summary>
        /// Public variable to allow access to list of plugins.
        /// </summary>
        [ImportMany(typeof(IPlugin))]
        public IEnumerable<IPlugin> pluginsList;

        /// <summary>
        /// Initializes the plugin manager. Loads the list of plugins.
        /// </summary>
        // todo: Find a way to load specific modules
        public PluginManager()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(pluginPath));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        /// <summary>
        /// Adds the Start, Stop, and Loop functions from the modules to the job handler
        /// </summary>
        /// todo: Find a way to run this at start of Application
        public void StartHost()
        {
            foreach (var module in pluginsList)
            {
                Framework.Jobs.JobHandler.AddToStart(module.Start);
                Framework.Jobs.JobHandler.AddToLoop(module.Tick);
                Framework.Jobs.JobHandler.AddToStop(module.Stop);
            }

            Framework.Jobs.JobHandler.Start();
            Framework.Jobs.JobHandler.Update();
        }

        /// <summary>
        /// Stops the JobHandler
        /// </summary>
        public static void StopHost()
        {
            Framework.Jobs.JobHandler.Stop();
        }

    }
}
