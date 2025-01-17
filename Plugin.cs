using BepInEx;
using UnityEngine;
using Falcon.World;
using Falcon.Game2.UI;
using System.Collections.Generic;

namespace dynamic_time;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID,
             MyPluginInfo.PLUGIN_NAME,
             MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Arena.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static Dictionary<TimeOfDay, float> timeOfDayPresets;

    internal static TimeOfDay currentTimeOfDay;

    private void Awake()
    {
        currentTimeOfDay
            = TimeOfDay.Dawn;

        timeOfDayPresets
            = new Dictionary<TimeOfDay, float>
        {
            {
                TimeOfDay.Dawn,
                6f
            },
            {
                TimeOfDay.Morning,
                9f
            },
            {
                TimeOfDay.Noon,
                12f
            },
            {
                TimeOfDay.Afternoon,
                15f
            },
            {
                TimeOfDay.Evening,
                17.75f
            },
            {
                TimeOfDay.Night,
                22f
            }
        };
    }

    void Update()
    {
        if (SingletonMonobehaviour<QuickMissionBuilder>.Instance != null)
        {
            if (Environment.Instance.TODTimespan.Hours != timeOfDayPresets[currentTimeOfDay]
                &&
                Environment.Instance.Timescale == 0f)
            {
                Environment.Instance.SetTimeOfDayPreset(currentTimeOfDay);
            }

            if (Input.GetKeyUp(KeyCode.Alpha0))
            {
                currentTimeOfDay
                    = (int)currentTimeOfDay++ < 5 ? currentTimeOfDay : currentTimeOfDay = 0;
            }

            if (Input.GetKeyUp(KeyCode.Alpha9))
            {
                if (Environment.Instance.Timescale == 0f)
                {
                    Environment.Instance.Timescale = 48f;
                }
                else
                {
                    Environment.Instance.Timescale = 0f;
                }
            }
        }
    }
}
