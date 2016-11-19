using PluginCore;
using Reactive.Framework.Error;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Generic;
using Reactive.Framework.Events;

namespace Reactive.Plugin
{
    /// <summary>
    /// Handles the loading and management of plugins. Using Managed Extensibility Framework (MEF)
    /// This is the base script for the MALE system.
    /// </summary>
    public class PluginManager
    {
        internal string pluginPath = App.StartupPath + "\\plugins";

        /// <summary>
        /// Public variable to allow access to list of available plugins.
        /// </summary>
        [ImportMany(typeof(IPlugin))]
        public IEnumerable<IPlugin> pluginsList;

        /// <summary>
        /// A list of loaded plugins
        /// </summary>
        public List<IPlugin> loadedPlugins = new List<IPlugin>();

        /// <summary>
        /// Initializes the plugin manager. Loads the list of plugins.
        /// </summary>
        public PluginManager()
        {
            this.LoadAvailablePlugins();

            ApplicationEvents.addToExitEvent(this.StopHost);
        }

        /// <summary>
        /// Loads the available plugins in the plugin directory
        /// </summary>
        public void LoadAvailablePlugins()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(pluginPath));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        /// <summary>
        /// Allows the user/system to selectively load plugins into the system
        /// </summary>
        /// <param name="plugin"></param>
        public void LoadPlugin(IPlugin plugin)
        {
            try
            {
                if (!pluginsList.Contains(plugin))
                    throw new KeyNotFoundException();

                loadedPlugins.Add(plugin);
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log("Unable to load plugin: " + plugin.pluginName);
            }
        }

        /// <summary>
        /// Allows the user/system to selectively unload plugins into the system
        /// </summary>
        /// <param name="plugin"></param>
        public void UnloadPlugin(IPlugin plugin)
        {
            try
            {
                if (!loadedPlugins.Contains(plugin))
                    throw new KeyNotFoundException();

                loadedPlugins.Remove(plugin);
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log("Unable to unload plugin: " + plugin.pluginName);
            }
        }

        /// <summary>
        /// Adds the Start, Stop, and Loop functions from the modules to the job handler
        /// </summary>
        /// todo: Find a way to run this at start of Application
        public void StartHost()
        {
            foreach (var module in loadedPlugins)
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
        public void StopHost()
        {
            Framework.Jobs.JobHandler.Stop();
        }

    }
}
