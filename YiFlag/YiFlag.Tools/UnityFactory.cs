using Unity.Interception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Microsoft.Practices.Unity.Configuration;

namespace YiFlag.Tools
{
    public class UnityFactory
    {

        private static IUnityContainer _Container = null;
        private static readonly Object sigleton_lock = new object();
        private static ExeConfigurationFileMap fileMap = null;
        private static Configuration configuration = null;
        private static UnityConfigurationSection section = null;
        static UnityFactory()
        {
            fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");//找配置文件的路径
            configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

        }

        public static IUnityContainer CreateInstance()
        {
            if (_Container == null)
            {
                lock (sigleton_lock)
                {
                    if (_Container == null)
                    {
                        _Container = new UnityContainer();
                        section.Configure(_Container, "bllContainer");
                    }
                }
            }
            return _Container;
        }
    }
}
