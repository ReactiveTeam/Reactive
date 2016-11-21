using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Plugin
{
    /// <summary>
    /// Handles the custom plugin format. Basically it's a zip file with a custom extension.
    /// </summary>
    /// todo: Find a better logic for PluginSerializer to build custom modules data files. For now, this is not usable.
    public class PluginSerializer
    {
        internal string pluginPath = App.StartupPath + "\\plugins\\data\\";
        public List<string> loadedDataFiles = new List<string>();

        private void LoadDataFile(string plugin)
        {
            try
            {
                if (File.Exists(pluginPath + plugin + ".data"))
                {
                    loadedDataFiles.Add(plugin);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }catch (FileNotFoundException e1)
            {
                
            }catch (Exception e2)
            {

            }
        }
    }
}
