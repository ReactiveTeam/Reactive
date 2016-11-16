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
        /// This is a test function
        /// </summary>
        /// todo: Delete this when Start, Stop, and Tick have been fully implemented.
        public void StartHost()
        {
            foreach (var module in pluginsList)
            {
                module.Start();
            }
        }
    }
}
