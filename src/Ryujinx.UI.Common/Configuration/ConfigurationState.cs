using ARMeilleure;
using Ryujinx.Common;
using Ryujinx.Common.Configuration;
using Ryujinx.Common.Configuration.Hid;
using Ryujinx.Common.Configuration.Hid.Controller;
using Ryujinx.Common.Configuration.Hid.Keyboard;
using Ryujinx.Common.Configuration.Multiplayer;
using Ryujinx.Common.Logging;
using Ryujinx.Graphics.Vulkan;
using Ryujinx.HLE;
using Ryujinx.UI.Common.Configuration.System;
using Ryujinx.UI.Common.Configuration.UI;
using Ryujinx.UI.Common.Helper;
using System;
using System.Collections.Generic;

namespace Ryujinx.UI.Common.Configuration
{
    public class ConfigurationState
    {
        /// <summary>
        /// UI configuration section
        /// </summary>
        public class UISection
        {
            public class Columns
            {
                public ReactiveObject<bool> FavColumn { get; private set; }
                public ReactiveObject<bool> IconColumn { get; private set; }
                public ReactiveObject<bool> AppColumn { get; private set; }
                public ReactiveObject<bool> DevColumn { get; private set; }
                public ReactiveObject<bool> VersionColumn { get; private set; }
                public ReactiveObject<bool> TimePlayedColumn { get; private set; }
                public ReactiveObject<bool> LastPlayedColumn { get; private set; }
                public ReactiveObject<bool> FileExtColumn { get; private set; }
                public ReactiveObject<bool> FileSizeColumn { get; private set; }
                public ReactiveObject<bool> PathColumn { get; private set; }

                public Columns()
                {
                    FavColumn = new ReactiveObject<bool>();
                    IconColumn = new ReactiveObject<bool>();
                    AppColumn = new ReactiveObject<bool>();
                    DevColumn = new ReactiveObject<bool>();
                    VersionColumn = new ReactiveObject<bool>();
                    TimePlayedColumn = new ReactiveObject<bool>();
                    LastPlayedColumn = new ReactiveObject<bool>();
                    FileExtColumn = new ReactiveObject<bool>();
                    FileSizeColumn = new ReactiveObject<bool>();
                    PathColumn = new ReactiveObject<bool>();
                }
            }

            public class ColumnSortSettings
            {
                public ReactiveObject<int> SortColumnId { get; private set; }
                public ReactiveObject<bool> SortAscending { get; private set; }

                public ColumnSortSettings()
                {
                    SortColumnId = new ReactiveObject<int>();
                    SortAscending = new ReactiveObject<bool>();
                }
            }

            /// <summary>
            /// Used to toggle which file types are shown in the UI
            /// </summary>
            public class ShownFileTypeSettings
            {
                public ReactiveObject<bool> NSP { get; private set; }
                public ReactiveObject<bool> PFS0 { get; private set; }
                public ReactiveObject<bool> XCI { get; private set; }
                public ReactiveObject<bool> NCA { get; private set; }
                public ReactiveObject<bool> NRO { get; private set; }
                public ReactiveObject<bool> NSO { get; private set; }

                public ShownFileTypeSettings()
                {
                    NSP = new ReactiveObject<bool>();
                    PFS0 = new ReactiveObject<bool>();
                    XCI = new ReactiveObject<bool>();
                    NCA = new ReactiveObject<bool>();
                    NRO = new ReactiveObject<bool>();
                    NSO = new ReactiveObject<bool>();
                }
            }

            // <summary>
            /// Determines main window start-up position, size and state
            ///<summary>
            public class WindowStartupSettings
            {
                public ReactiveObject<int> WindowSizeWidth { get; private set; }
                public ReactiveObject<int> WindowSizeHeight { get; private set; }
                public ReactiveObject<int> WindowPositionX { get; private set; }
                public ReactiveObject<int> WindowPositionY { get; private set; }
                public ReactiveObject<bool> WindowMaximized { get; private set; }

                public WindowStartupSettings()
                {
                    WindowSizeWidth = new ReactiveObject<int>();
                    WindowSizeHeight = new ReactiveObject<int>();
                    WindowPositionX = new ReactiveObject<int>();
                    WindowPositionY = new ReactiveObject<int>();
                    WindowMaximized = new ReactiveObject<bool>();
                }
            }

            /// <summary>
            /// Used to toggle columns in the GUI
            /// </summary>
            public Columns GuiColumns { get; private set; }

            /// <summary>
            /// Used to configure column sort settings in the GUI
            /// </summary>
            public ColumnSortSettings ColumnSort { get; private set; }

            /// <summary>
            /// A list of directories containing games to be used to load games into the games list
            /// </summary>
            public ReactiveObject<List<string>> GameDirs { get; private set; }

            /// <summary>
            /// A list of directories containing DLC/updates the user wants to autoload during library refreshes
            /// </summary>
            public ReactiveObject<List<string>> AutoloadDirs { get; private set; }

            /// <summary>
            /// A list of file types to be hidden in the games List
            /// </summary>
            public ShownFileTypeSettings ShownFileTypes { get; private set; }

            /// <summary>
            /// Determines main window start-up position, size and state
            /// </summary>
            public WindowStartupSettings WindowStartup { get; private set; }

            /// <summary>
            /// Language Code for the UI
            /// </summary>
            public ReactiveObject<string> LanguageCode { get; private set; }

            /// <summary>
            /// Selects the base style
            /// </summary>
            public ReactiveObject<string> BaseStyle { get; private set; }

            /// <summary>
            /// Start games in fullscreen mode
            /// </summary>
            public ReactiveObject<bool> StartFullscreen { get; private set; }

            /// <summary>
            /// Hide / Show Console Window
            /// </summary>
            public ReactiveObject<bool> ShowConsole { get; private set; }

            /// <summary>
            /// View Mode of the Game list
            /// </summary>
            public ReactiveObject<int> GameListViewMode { get; private set; }

            /// <summary>
            /// Show application name in Grid Mode
            /// </summary>
            public ReactiveObject<bool> ShowNames { get; private set; }

            /// <summary>
            /// Sets App Icon Size in Grid Mode
            /// </summary>
            public ReactiveObject<int> GridSize { get; private set; }

            /// <summary>
            /// Sorts Apps in Grid Mode
            /// </summary>
            public ReactiveObject<int> ApplicationSort { get; private set; }

            /// <summary>
            /// Sets if Grid is ordered in Ascending Order
            /// </summary>
            public ReactiveObject<bool> IsAscendingOrder { get; private set; }

            public UISection()
            {
                GuiColumns = new Columns();
                ColumnSort = new ColumnSortSettings();
                GameDirs = new ReactiveObject<List<string>>();
                AutoloadDirs = new ReactiveObject<List<string>>();
                ShownFileTypes = new ShownFileTypeSettings();
                WindowStartup = new WindowStartupSettings();
                BaseStyle = new ReactiveObject<string>();
                StartFullscreen = new ReactiveObject<bool>();
                GameListViewMode = new ReactiveObject<int>();
                ShowNames = new ReactiveObject<bool>();
                GridSize = new ReactiveObject<int>();
                ApplicationSort = new ReactiveObject<int>();
                IsAscendingOrder = new ReactiveObject<bool>();
                LanguageCode = new ReactiveObject<string>();
                ShowConsole = new ReactiveObject<bool>();
                ShowConsole.Event += static (_, e) => ConsoleHelper.SetConsoleWindowState(e.NewValue);
            }
        }

        /// <summary>
        /// Logger configuration section
        /// </summary>
        public class LoggerSection
        {
            /// <summary>
            /// Enables printing debug log messages
            /// </summary>
            public ReactiveObject<bool> EnableDebug { get; private set; }

            /// <summary>
            /// Enables printing stub log messages
            /// </summary>
            public ReactiveObject<bool> EnableStub { get; private set; }

            /// <summary>
            /// Enables printing info log messages
            /// </summary>
            public ReactiveObject<bool> EnableInfo { get; private set; }

            /// <summary>
            /// Enables printing warning log messages
            /// </summary>
            public ReactiveObject<bool> EnableWarn { get; private set; }

            /// <summary>
            /// Enables printing error log messages
            /// </summary>
            public ReactiveObject<bool> EnableError { get; private set; }

            /// <summary>
            /// Enables printing trace log messages
            /// </summary>
            public ReactiveObject<bool> EnableTrace { get; private set; }

            /// <summary>
            /// Enables printing guest log messages
            /// </summary>
            public ReactiveObject<bool> EnableGuest { get; private set; }

            /// <summary>
            /// Enables printing FS access log messages
            /// </summary>
            public ReactiveObject<bool> EnableFsAccessLog { get; private set; }

            /// <summary>
            /// Controls which log messages are written to the log targets
            /// </summary>
            public ReactiveObject<LogClass[]> FilteredClasses { get; private set; }

            /// <summary>
            /// Enables or disables logging to a file on disk
            /// </summary>
            public ReactiveObject<bool> EnableFileLog { get; private set; }

            /// <summary>
            /// Controls which OpenGL log messages are recorded in the log
            /// </summary>
            public ReactiveObject<GraphicsDebugLevel> GraphicsDebugLevel { get; private set; }

            public LoggerSection()
            {
                EnableDebug = new ReactiveObject<bool>();
                EnableDebug.LogChangesToValue(nameof(EnableDebug));
                EnableStub = new ReactiveObject<bool>();
                EnableInfo = new ReactiveObject<bool>();
                EnableWarn = new ReactiveObject<bool>();
                EnableError = new ReactiveObject<bool>();
                EnableTrace = new ReactiveObject<bool>();
                EnableGuest = new ReactiveObject<bool>();
                EnableFsAccessLog = new ReactiveObject<bool>();
                FilteredClasses = new ReactiveObject<LogClass[]>();
                EnableFileLog = new ReactiveObject<bool>();
                EnableFileLog.LogChangesToValue(nameof(EnableFileLog));
                GraphicsDebugLevel = new ReactiveObject<GraphicsDebugLevel>();
            }
        }

        /// <summary>
        /// System configuration section
        /// </summary>
        public class SystemSection
        {
            /// <summary>
            /// Change System Language
            /// </summary>
            public ReactiveObject<Language> Language { get; private set; }

            /// <summary>
            /// Change System Region
            /// </summary>
            public ReactiveObject<Region> Region { get; private set; }

            /// <summary>
            /// Change System TimeZone
            /// </summary>
            public ReactiveObject<string> TimeZone { get; private set; }

            /// <summary>
            /// System Time Offset in Seconds
            /// </summary>
            public ReactiveObject<long> SystemTimeOffset { get; private set; }

            /// <summary>
            /// Enables or disables Docked Mode
            /// </summary>
            public ReactiveObject<bool> EnableDockedMode { get; private set; }

            /// <summary>
            /// Enables or disables persistent profiled translation cache
            /// </summary>
            public ReactiveObject<bool> EnablePtc { get; private set; }

            /// <summary>
            /// Enables or disables low-power persistent profiled translation cache loading
            /// </summary>
            public ReactiveObject<bool> EnableLowPowerPtc { get; private set; }

            /// <summary>
            /// Enables or disables guest Internet access
            /// </summary>
            public ReactiveObject<bool> EnableInternetAccess { get; private set; }

            /// <summary>
            /// Enables integrity checks on Game content files
            /// </summary>
            public ReactiveObject<bool> EnableFsIntegrityChecks { get; private set; }

            /// <summary>
            /// Enables FS access log output to the console. Possible modes are 0-3
            /// </summary>
            public ReactiveObject<int> FsGlobalAccessLogMode { get; private set; }

            /// <summary>
            /// The selected audio backend
            /// </summary>
            public ReactiveObject<AudioBackend> AudioBackend { get; private set; }

            /// <summary>
            /// The audio backend volume
            /// </summary>
            public ReactiveObject<float> AudioVolume { get; private set; }

            /// <summary>
            /// The selected memory manager mode
            /// </summary>
            public ReactiveObject<MemoryManagerMode> MemoryManagerMode { get; private set; }

            /// <summary>
            /// Defines the amount of RAM available on the emulated system, and how it is distributed
            /// </summary>
            public ReactiveObject<MemoryConfiguration> DramSize { get; private set; }

            /// <summary>
            /// Enable or disable ignoring missing services
            /// </summary>
            public ReactiveObject<bool> IgnoreMissingServices { get; private set; }

            /// <summary>
            /// Uses Hypervisor over JIT if available
            /// </summary>
            public ReactiveObject<bool> UseHypervisor { get; private set; }

            public SystemSection()
            {
                Language = new ReactiveObject<Language>();
                Language.LogChangesToValue(nameof(Language));
                Region = new ReactiveObject<Region>();
                Region.LogChangesToValue(nameof(Region));
                TimeZone = new ReactiveObject<string>();
                TimeZone.LogChangesToValue(nameof(TimeZone));
                SystemTimeOffset = new ReactiveObject<long>();
                SystemTimeOffset.LogChangesToValue(nameof(SystemTimeOffset));
                EnableDockedMode = new ReactiveObject<bool>();
                EnableDockedMode.LogChangesToValue(nameof(EnableDockedMode));
                EnablePtc = new ReactiveObject<bool>();
                EnablePtc.LogChangesToValue(nameof(EnablePtc));
                EnableLowPowerPtc = new ReactiveObject<bool>();
                EnableLowPowerPtc.LogChangesToValue(nameof(EnableLowPowerPtc));
                EnableInternetAccess = new ReactiveObject<bool>();
                EnableInternetAccess.LogChangesToValue(nameof(EnableInternetAccess));
                EnableFsIntegrityChecks = new ReactiveObject<bool>();
                EnableFsIntegrityChecks.LogChangesToValue(nameof(EnableFsIntegrityChecks));
                FsGlobalAccessLogMode = new ReactiveObject<int>();
                FsGlobalAccessLogMode.LogChangesToValue(nameof(FsGlobalAccessLogMode));
                AudioBackend = new ReactiveObject<AudioBackend>();
                AudioBackend.LogChangesToValue(nameof(AudioBackend));
                MemoryManagerMode = new ReactiveObject<MemoryManagerMode>();
                MemoryManagerMode.LogChangesToValue(nameof(MemoryManagerMode));
                DramSize = new ReactiveObject<MemoryConfiguration>();
                DramSize.LogChangesToValue(nameof(DramSize));
                IgnoreMissingServices = new ReactiveObject<bool>();
                IgnoreMissingServices.LogChangesToValue(nameof(IgnoreMissingServices));
                AudioVolume = new ReactiveObject<float>();
                AudioVolume.LogChangesToValue(nameof(AudioVolume));
                UseHypervisor = new ReactiveObject<bool>();
                UseHypervisor.LogChangesToValue(nameof(UseHypervisor));
            }
        }

        /// <summary>
        /// Hid configuration section
        /// </summary>
        public class HidSection
        {
            /// <summary>
            /// Enable or disable keyboard support (Independent from controllers binding)
            /// </summary>
            public ReactiveObject<bool> EnableKeyboard { get; private set; }

            /// <summary>
            /// Enable or disable mouse support (Independent from controllers binding)
            /// </summary>
            public ReactiveObject<bool> EnableMouse { get; private set; }

            /// <summary>
            /// Hotkey Keyboard Bindings
            /// </summary>
            public ReactiveObject<KeyboardHotkeys> Hotkeys { get; private set; }

            /// <summary>
            /// Input device configuration.
            /// NOTE: This ReactiveObject won't issue an event when the List has elements added or removed.
            /// TODO: Implement a ReactiveList class.
            /// </summary>
            public ReactiveObject<List<InputConfig>> InputConfig { get; private set; }

            public HidSection()
            {
                EnableKeyboard = new ReactiveObject<bool>();
                EnableMouse = new ReactiveObject<bool>();
                Hotkeys = new ReactiveObject<KeyboardHotkeys>();
                InputConfig = new ReactiveObject<List<InputConfig>>();
            }
        }

        /// <summary>
        /// Graphics configuration section
        /// </summary>
        public class GraphicsSection
        {
            /// <summary>
            /// Whether or not backend threading is enabled. The "Auto" setting will determine whether threading should be enabled at runtime.
            /// </summary>
            public ReactiveObject<BackendThreading> BackendThreading { get; private set; }

            /// <summary>
            /// Max Anisotropy. Values range from 0 - 16. Set to -1 to let the game decide.
            /// </summary>
            public ReactiveObject<float> MaxAnisotropy { get; private set; }

            /// <summary>
            /// Aspect Ratio applied to the renderer window.
            /// </summary>
            public ReactiveObject<AspectRatio> AspectRatio { get; private set; }

            /// <summary>
            /// Resolution Scale. An integer scale applied to applicable render targets. Values 1-4, or -1 to use a custom floating point scale instead.
            /// </summary>
            public ReactiveObject<int> ResScale { get; private set; }

            /// <summary>
            /// Custom Resolution Scale. A custom floating point scale applied to applicable render targets. Only active when Resolution Scale is -1.
            /// </summary>
            public ReactiveObject<float> ResScaleCustom { get; private set; }

            /// <summary>
            /// Dumps shaders in this local directory
            /// </summary>
            public ReactiveObject<string> ShadersDumpPath { get; private set; }

            /// <summary>
            /// Enables or disables Vertical Sync
            /// </summary>
            public ReactiveObject<bool> EnableVsync { get; private set; }

            /// <summary>
            /// Enables or disables Shader cache
            /// </summary>
            public ReactiveObject<bool> EnableShaderCache { get; private set; }

            /// <summary>
            /// Enables or disables texture recompression
            /// </summary>
            public ReactiveObject<bool> EnableTextureRecompression { get; private set; }

            /// <summary>
            /// Enables or disables Macro high-level emulation
            /// </summary>
            public ReactiveObject<bool> EnableMacroHLE { get; private set; }

            /// <summary>
            /// Enables or disables color space passthrough, if available.
            /// </summary>
            public ReactiveObject<bool> EnableColorSpacePassthrough { get; private set; }

            /// <summary>
            /// Graphics backend
            /// </summary>
            public ReactiveObject<GraphicsBackend> GraphicsBackend { get; private set; }

            /// <summary>
            /// Applies anti-aliasing to the renderer.
            /// </summary>
            public ReactiveObject<AntiAliasing> AntiAliasing { get; private set; }

            /// <summary>
            /// Sets the framebuffer upscaling type.
            /// </summary>
            public ReactiveObject<ScalingFilter> ScalingFilter { get; private set; }

            /// <summary>
            /// Sets the framebuffer upscaling level.
            /// </summary>
            public ReactiveObject<int> ScalingFilterLevel { get; private set; }

            /// <summary>
            /// Preferred GPU
            /// </summary>
            public ReactiveObject<string> PreferredGpu { get; private set; }

            public GraphicsSection()
            {
                BackendThreading = new ReactiveObject<BackendThreading>();
                BackendThreading.LogChangesToValue(nameof(BackendThreading));
                ResScale = new ReactiveObject<int>();
                ResScale.LogChangesToValue(nameof(ResScale));
                ResScaleCustom = new ReactiveObject<float>();
                ResScaleCustom.LogChangesToValue(nameof(ResScaleCustom));
                MaxAnisotropy = new ReactiveObject<float>();
                MaxAnisotropy.LogChangesToValue(nameof(MaxAnisotropy));
                AspectRatio = new ReactiveObject<AspectRatio>();
                AspectRatio.LogChangesToValue(nameof(AspectRatio));
                ShadersDumpPath = new ReactiveObject<string>();
                EnableVsync = new ReactiveObject<bool>();
                EnableVsync.LogChangesToValue(nameof(EnableVsync));
                EnableShaderCache = new ReactiveObject<bool>();
                EnableShaderCache.LogChangesToValue(nameof(EnableShaderCache));
                EnableTextureRecompression = new ReactiveObject<bool>();
                EnableTextureRecompression.LogChangesToValue(nameof(EnableTextureRecompression));
                GraphicsBackend = new ReactiveObject<GraphicsBackend>();
                GraphicsBackend.LogChangesToValue(nameof(GraphicsBackend));
                PreferredGpu = new ReactiveObject<string>();
                PreferredGpu.LogChangesToValue(nameof(PreferredGpu));
                EnableMacroHLE = new ReactiveObject<bool>();
                EnableMacroHLE.LogChangesToValue(nameof(EnableMacroHLE));
                EnableColorSpacePassthrough = new ReactiveObject<bool>();
                EnableColorSpacePassthrough.LogChangesToValue(nameof(EnableColorSpacePassthrough));
                AntiAliasing = new ReactiveObject<AntiAliasing>();
                AntiAliasing.LogChangesToValue(nameof(AntiAliasing));
                ScalingFilter = new ReactiveObject<ScalingFilter>();
                ScalingFilter.LogChangesToValue(nameof(ScalingFilter));
                ScalingFilterLevel = new ReactiveObject<int>();
                ScalingFilterLevel.LogChangesToValue(nameof(ScalingFilterLevel));
            }
        }

        /// <summary>
        /// Multiplayer configuration section
        /// </summary>
        public class MultiplayerSection
        {
            /// <summary>
            /// GUID for the network interface used by LAN (or 0 for default)
            /// </summary>
            public ReactiveObject<string> LanInterfaceId { get; private set; }

            /// <summary>
            /// Multiplayer Mode
            /// </summary>
            public ReactiveObject<MultiplayerMode> Mode { get; private set; }

            public MultiplayerSection()
            {
                LanInterfaceId = new ReactiveObject<string>();
                Mode = new ReactiveObject<MultiplayerMode>();
                Mode.LogChangesToValue(nameof(Mode));
            }
        }

        /// <summary>
        /// The default configuration instance
        /// </summary>
        public static ConfigurationState Instance { get; private set; }

        /// <summary>
        /// The UI section
        /// </summary>
        public UISection UI { get; private set; }

        /// <summary>
        /// The Logger section
        /// </summary>
        public LoggerSection Logger { get; private set; }

        /// <summary>
        /// The System section
        /// </summary>
        public SystemSection System { get; private set; }

        /// <summary>
        /// The Graphics section
        /// </summary>
        public GraphicsSection Graphics { get; private set; }

        /// <summary>
        /// The Hid section
        /// </summary>
        public HidSection Hid { get; private set; }

        /// <summary>
        /// The Multiplayer section
        /// </summary>
        public MultiplayerSection Multiplayer { get; private set; }

        /// <summary>
        /// Enables or disables Discord Rich Presence
        /// </summary>
        public ReactiveObject<bool> EnableDiscordIntegration { get; private set; }

        /// <summary>
        /// Checks for updates when Ryujinx starts when enabled
        /// </summary>
        public ReactiveObject<bool> CheckUpdatesOnStart { get; private set; }

        /// <summary>
        /// Show "Confirm Exit" Dialog
        /// </summary>
        public ReactiveObject<bool> ShowConfirmExit { get; private set; }

        /// <summary>
        /// Ignore Applet
        /// </summary>
        public ReactiveObject<bool> IgnoreApplet { get; private set; }

        /// <summary>
        /// Enables or disables save window size, position and state on close.
        /// </summary>
        public ReactiveObject<bool> RememberWindowState { get; private set; }

        /// <summary>
        /// Enables or disables the redesigned title bar
        /// </summary>
        public ReactiveObject<bool> ShowTitleBar { get; private set; }

        /// <summary>
        /// Enables hardware-accelerated rendering for Avalonia
        /// </summary>
        public ReactiveObject<bool> EnableHardwareAcceleration { get; private set; }

        /// <summary>
        /// Hide Cursor on Idle
        /// </summary>
        public ReactiveObject<HideCursorMode> HideCursor { get; private set; }

        private ConfigurationState()
        {
            UI = new UISection();
            Logger = new LoggerSection();
            System = new SystemSection();
            Graphics = new GraphicsSection();
            Hid = new HidSection();
            Multiplayer = new MultiplayerSection();
            EnableDiscordIntegration = new ReactiveObject<bool>();
            CheckUpdatesOnStart = new ReactiveObject<bool>();
            ShowConfirmExit = new ReactiveObject<bool>();
            IgnoreApplet = new ReactiveObject<bool>();
            IgnoreApplet.LogChangesToValue(nameof(IgnoreApplet));
            RememberWindowState = new ReactiveObject<bool>();
            ShowTitleBar = new ReactiveObject<bool>();
            EnableHardwareAcceleration = new ReactiveObject<bool>();
            HideCursor = new ReactiveObject<HideCursorMode>();
        }

        public ConfigurationFileFormat ToFileFormat()
        {
            ConfigurationFileFormat configurationFile = new()
            {
                Version = ConfigurationFileFormat.CurrentVersion,
                BackendThreading = Graphics.BackendThreading,
                EnableFileLog = Logger.EnableFileLog,
                ResScale = Graphics.ResScale,
                ResScaleCustom = Graphics.ResScaleCustom,
                MaxAnisotropy = Graphics.MaxAnisotropy,
                AspectRatio = Graphics.AspectRatio,
                AntiAliasing = Graphics.AntiAliasing,
                ScalingFilter = Graphics.ScalingFilter,
                ScalingFilterLevel = Graphics.ScalingFilterLevel,
                GraphicsShadersDumpPath = Graphics.ShadersDumpPath,
                LoggingEnableDebug = Logger.EnableDebug,
                LoggingEnableStub = Logger.EnableStub,
                LoggingEnableInfo = Logger.EnableInfo,
                LoggingEnableWarn = Logger.EnableWarn,
                LoggingEnableError = Logger.EnableError,
                LoggingEnableTrace = Logger.EnableTrace,
                LoggingEnableGuest = Logger.EnableGuest,
                LoggingEnableFsAccessLog = Logger.EnableFsAccessLog,
                LoggingFilteredClasses = Logger.FilteredClasses,
                LoggingGraphicsDebugLevel = Logger.GraphicsDebugLevel,
                SystemLanguage = System.Language,
                SystemRegion = System.Region,
                SystemTimeZone = System.TimeZone,
                SystemTimeOffset = System.SystemTimeOffset,
                DockedMode = System.EnableDockedMode,
                EnableDiscordIntegration = EnableDiscordIntegration,
                CheckUpdatesOnStart = CheckUpdatesOnStart,
                ShowConfirmExit = ShowConfirmExit,
                IgnoreApplet = IgnoreApplet,
                RememberWindowState = RememberWindowState,
                ShowTitleBar = ShowTitleBar,
                EnableHardwareAcceleration = EnableHardwareAcceleration,
                HideCursor = HideCursor,
                EnableVsync = Graphics.EnableVsync,
                EnableShaderCache = Graphics.EnableShaderCache,
                EnableTextureRecompression = Graphics.EnableTextureRecompression,
                EnableMacroHLE = Graphics.EnableMacroHLE,
                EnableColorSpacePassthrough = Graphics.EnableColorSpacePassthrough,
                EnablePtc = System.EnablePtc,
                EnableLowPowerPtc = System.EnableLowPowerPtc,
                EnableInternetAccess = System.EnableInternetAccess,
                EnableFsIntegrityChecks = System.EnableFsIntegrityChecks,
                FsGlobalAccessLogMode = System.FsGlobalAccessLogMode,
                AudioBackend = System.AudioBackend,
                AudioVolume = System.AudioVolume,
                MemoryManagerMode = System.MemoryManagerMode,
                DramSize = System.DramSize,
                IgnoreMissingServices = System.IgnoreMissingServices,
                UseHypervisor = System.UseHypervisor,
                GuiColumns = new GuiColumns
                {
                    FavColumn = UI.GuiColumns.FavColumn,
                    IconColumn = UI.GuiColumns.IconColumn,
                    AppColumn = UI.GuiColumns.AppColumn,
                    DevColumn = UI.GuiColumns.DevColumn,
                    VersionColumn = UI.GuiColumns.VersionColumn,
                    TimePlayedColumn = UI.GuiColumns.TimePlayedColumn,
                    LastPlayedColumn = UI.GuiColumns.LastPlayedColumn,
                    FileExtColumn = UI.GuiColumns.FileExtColumn,
                    FileSizeColumn = UI.GuiColumns.FileSizeColumn,
                    PathColumn = UI.GuiColumns.PathColumn,
                },
                ColumnSort = new ColumnSort
                {
                    SortColumnId = UI.ColumnSort.SortColumnId,
                    SortAscending = UI.ColumnSort.SortAscending,
                },
                GameDirs = UI.GameDirs,
                AutoloadDirs = UI.AutoloadDirs,
                ShownFileTypes = new ShownFileTypes
                {
                    NSP = UI.ShownFileTypes.NSP,
                    PFS0 = UI.ShownFileTypes.PFS0,
                    XCI = UI.ShownFileTypes.XCI,
                    NCA = UI.ShownFileTypes.NCA,
                    NRO = UI.ShownFileTypes.NRO,
                    NSO = UI.ShownFileTypes.NSO,
                },
                WindowStartup = new WindowStartup
                {
                    WindowSizeWidth = UI.WindowStartup.WindowSizeWidth,
                    WindowSizeHeight = UI.WindowStartup.WindowSizeHeight,
                    WindowPositionX = UI.WindowStartup.WindowPositionX,
                    WindowPositionY = UI.WindowStartup.WindowPositionY,
                    WindowMaximized = UI.WindowStartup.WindowMaximized,
                },
                LanguageCode = UI.LanguageCode,
                BaseStyle = UI.BaseStyle,
                GameListViewMode = UI.GameListViewMode,
                ShowNames = UI.ShowNames,
                GridSize = UI.GridSize,
                ApplicationSort = UI.ApplicationSort,
                IsAscendingOrder = UI.IsAscendingOrder,
                StartFullscreen = UI.StartFullscreen,
                ShowConsole = UI.ShowConsole,
                EnableKeyboard = Hid.EnableKeyboard,
                EnableMouse = Hid.EnableMouse,
                Hotkeys = Hid.Hotkeys,
                KeyboardConfig = [],
                ControllerConfig = [],
                InputConfig = Hid.InputConfig,
                GraphicsBackend = Graphics.GraphicsBackend,
                PreferredGpu = Graphics.PreferredGpu,
                MultiplayerLanInterfaceId = Multiplayer.LanInterfaceId,
                MultiplayerMode = Multiplayer.Mode,
            };

            return configurationFile;
        }

        public void LoadDefault()
        {
            Logger.EnableFileLog.Value = true;
            Graphics.BackendThreading.Value = BackendThreading.Auto;
            Graphics.ResScale.Value = 1;
            Graphics.ResScaleCustom.Value = 1.0f;
            Graphics.MaxAnisotropy.Value = -1.0f;
            Graphics.AspectRatio.Value = AspectRatio.Fixed16x9;
            Graphics.GraphicsBackend.Value = DefaultGraphicsBackend();
            Graphics.PreferredGpu.Value = string.Empty;
            Graphics.ShadersDumpPath.Value = string.Empty;
            Logger.EnableDebug.Value = false;
            Logger.EnableStub.Value = true;
            Logger.EnableInfo.Value = true;
            Logger.EnableWarn.Value = true;
            Logger.EnableError.Value = true;
            Logger.EnableTrace.Value = false;
            Logger.EnableGuest.Value = true;
            Logger.EnableFsAccessLog.Value = false;
            Logger.FilteredClasses.Value = [];
            Logger.GraphicsDebugLevel.Value = GraphicsDebugLevel.None;
            System.Language.Value = Language.AmericanEnglish;
            System.Region.Value = Region.USA;
            System.TimeZone.Value = "UTC";
            System.SystemTimeOffset.Value = 0;
            System.EnableDockedMode.Value = true;
            EnableDiscordIntegration.Value = true;
            CheckUpdatesOnStart.Value = true;
            ShowConfirmExit.Value = true;
            IgnoreApplet.Value = false;
            RememberWindowState.Value = true;
            ShowTitleBar.Value = !OperatingSystem.IsWindows();
            EnableHardwareAcceleration.Value = true;
            HideCursor.Value = HideCursorMode.OnIdle;
            Graphics.EnableVsync.Value = true;
            Graphics.EnableShaderCache.Value = true;
            Graphics.EnableTextureRecompression.Value = false;
            Graphics.EnableMacroHLE.Value = true;
            Graphics.EnableColorSpacePassthrough.Value = false;
            Graphics.AntiAliasing.Value = AntiAliasing.None;
            Graphics.ScalingFilter.Value = ScalingFilter.Bilinear;
            Graphics.ScalingFilterLevel.Value = 80;
            System.EnablePtc.Value = true;
            System.EnableInternetAccess.Value = false;
            System.EnableFsIntegrityChecks.Value = true;
            System.FsGlobalAccessLogMode.Value = 0;
            System.AudioBackend.Value = AudioBackend.SDL2;
            System.AudioVolume.Value = 1;
            System.MemoryManagerMode.Value = MemoryManagerMode.HostMappedUnsafe;
            System.DramSize.Value = MemoryConfiguration.MemoryConfiguration4GiB;
            System.IgnoreMissingServices.Value = false;
            System.UseHypervisor.Value = true;
            Multiplayer.LanInterfaceId.Value = "0";
            Multiplayer.Mode.Value = MultiplayerMode.Disabled;
            UI.GuiColumns.FavColumn.Value = true;
            UI.GuiColumns.IconColumn.Value = true;
            UI.GuiColumns.AppColumn.Value = true;
            UI.GuiColumns.DevColumn.Value = true;
            UI.GuiColumns.VersionColumn.Value = true;
            UI.GuiColumns.TimePlayedColumn.Value = true;
            UI.GuiColumns.LastPlayedColumn.Value = true;
            UI.GuiColumns.FileExtColumn.Value = true;
            UI.GuiColumns.FileSizeColumn.Value = true;
            UI.GuiColumns.PathColumn.Value = true;
            UI.ColumnSort.SortColumnId.Value = 0;
            UI.ColumnSort.SortAscending.Value = false;
            UI.GameDirs.Value = [];
            UI.AutoloadDirs.Value = [];
            UI.ShownFileTypes.NSP.Value = true;
            UI.ShownFileTypes.PFS0.Value = true;
            UI.ShownFileTypes.XCI.Value = true;
            UI.ShownFileTypes.NCA.Value = true;
            UI.ShownFileTypes.NRO.Value = true;
            UI.ShownFileTypes.NSO.Value = true;
            UI.LanguageCode.Value = "en_US";
            UI.BaseStyle.Value = "Dark";
            UI.GameListViewMode.Value = 0;
            UI.ShowNames.Value = true;
            UI.GridSize.Value = 2;
            UI.ApplicationSort.Value = 0;
            UI.IsAscendingOrder.Value = true;
            UI.StartFullscreen.Value = false;
            UI.ShowConsole.Value = true;
            UI.WindowStartup.WindowSizeWidth.Value = 1280;
            UI.WindowStartup.WindowSizeHeight.Value = 760;
            UI.WindowStartup.WindowPositionX.Value = 0;
            UI.WindowStartup.WindowPositionY.Value = 0;
            UI.WindowStartup.WindowMaximized.Value = false;
            Hid.EnableKeyboard.Value = false;
            Hid.EnableMouse.Value = false;
            Hid.Hotkeys.Value = new KeyboardHotkeys
            {
                ToggleVsync = Key.F1,
                ToggleMute = Key.F2,
                Screenshot = Key.F8,
                ShowUI = Key.F4,
                Pause = Key.F5,
                ResScaleUp = Key.Unbound,
                ResScaleDown = Key.Unbound,
                VolumeUp = Key.Unbound,
                VolumeDown = Key.Unbound,
            };
            Hid.InputConfig.Value =
            [
                new StandardKeyboardInputConfig
                {
                    Version = InputConfig.CurrentVersion,
                    Backend = InputBackendType.WindowKeyboard,
                    Id = "0",
                    PlayerIndex = PlayerIndex.Player1,
                    ControllerType = ControllerType.ProController,
                    LeftJoycon = new LeftJoyconCommonConfig<Key>
                    {
                        DpadUp = Key.Up,
                        DpadDown = Key.Down,
                        DpadLeft = Key.Left,
                        DpadRight = Key.Right,
                        ButtonMinus = Key.Minus,
                        ButtonL = Key.E,
                        ButtonZl = Key.Q,
                        ButtonSl = Key.Unbound,
                        ButtonSr = Key.Unbound,
                    },
                    LeftJoyconStick = new JoyconConfigKeyboardStick<Key>
                    {
                        StickUp = Key.W,
                        StickDown = Key.S,
                        StickLeft = Key.A,
                        StickRight = Key.D,
                        StickButton = Key.F,
                    },
                    RightJoycon = new RightJoyconCommonConfig<Key>
                    {
                        ButtonA = Key.Z,
                        ButtonB = Key.X,
                        ButtonX = Key.C,
                        ButtonY = Key.V,
                        ButtonPlus = Key.Plus,
                        ButtonR = Key.U,
                        ButtonZr = Key.O,
                        ButtonSl = Key.Unbound,
                        ButtonSr = Key.Unbound,
                    },
                    RightJoyconStick = new JoyconConfigKeyboardStick<Key>
                    {
                        StickUp = Key.I,
                        StickDown = Key.K,
                        StickLeft = Key.J,
                        StickRight = Key.L,
                        StickButton = Key.H,
                    },
                }

            ];
        }

        public void Load(ConfigurationFileFormat configurationFileFormat, string configurationFilePath)
        {
            bool configurationFileUpdated = false;

            if (configurationFileFormat.Version is < 0 or > ConfigurationFileFormat.CurrentVersion)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Unsupported configuration version {configurationFileFormat.Version}, loading default.");

                LoadDefault();
            }

            if (configurationFileFormat.Version < 2)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 2.");

                configurationFileFormat.SystemRegion = Region.USA;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 3)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 3.");

                configurationFileFormat.SystemTimeZone = "UTC";

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 4)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 4.");

                configurationFileFormat.MaxAnisotropy = -1;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 5)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 5.");

                configurationFileFormat.SystemTimeOffset = 0;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 8)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 8.");

                configurationFileFormat.EnablePtc = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 9)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 9.");

                configurationFileFormat.ColumnSort = new ColumnSort
                {
                    SortColumnId = 0,
                    SortAscending = false,
                };

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = Key.F1,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 10)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 10.");

                configurationFileFormat.AudioBackend = AudioBackend.OpenAl;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 11)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 11.");

                configurationFileFormat.ResScale = 1;
                configurationFileFormat.ResScaleCustom = 1.0f;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 12)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 12.");

                configurationFileFormat.LoggingGraphicsDebugLevel = GraphicsDebugLevel.None;

                configurationFileUpdated = true;
            }

            // configurationFileFormat.Version == 13 -> LDN1

            if (configurationFileFormat.Version < 14)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 14.");

                configurationFileFormat.CheckUpdatesOnStart = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 16)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 16.");

                configurationFileFormat.EnableShaderCache = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 17)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 17.");

                configurationFileFormat.StartFullscreen = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 18)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 18.");

                configurationFileFormat.AspectRatio = AspectRatio.Fixed16x9;

                configurationFileUpdated = true;
            }

            // configurationFileFormat.Version == 19 -> LDN2

            if (configurationFileFormat.Version < 20)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 20.");

                configurationFileFormat.ShowConfirmExit = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 21)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 21.");

                // Initialize network config.

                configurationFileFormat.MultiplayerMode = MultiplayerMode.Disabled;
                configurationFileFormat.MultiplayerLanInterfaceId = "0";

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 22)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 22.");

                configurationFileFormat.HideCursor = HideCursorMode.Never;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 24)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 24.");

                configurationFileFormat.InputConfig = new List<InputConfig>
                {
                    new StandardKeyboardInputConfig
                    {
                        Version = InputConfig.CurrentVersion,
                        Backend = InputBackendType.WindowKeyboard,
                        Id = "0",
                        PlayerIndex = PlayerIndex.Player1,
                        ControllerType = ControllerType.ProController,
                        LeftJoycon = new LeftJoyconCommonConfig<Key>
                        {
                            DpadUp = Key.Up,
                            DpadDown = Key.Down,
                            DpadLeft = Key.Left,
                            DpadRight = Key.Right,
                            ButtonMinus = Key.Minus,
                            ButtonL = Key.E,
                            ButtonZl = Key.Q,
                            ButtonSl = Key.Unbound,
                            ButtonSr = Key.Unbound,
                        },
                        LeftJoyconStick = new JoyconConfigKeyboardStick<Key>
                        {
                            StickUp = Key.W,
                            StickDown = Key.S,
                            StickLeft = Key.A,
                            StickRight = Key.D,
                            StickButton = Key.F,
                        },
                        RightJoycon = new RightJoyconCommonConfig<Key>
                        {
                            ButtonA = Key.Z,
                            ButtonB = Key.X,
                            ButtonX = Key.C,
                            ButtonY = Key.V,
                            ButtonPlus = Key.Plus,
                            ButtonR = Key.U,
                            ButtonZr = Key.O,
                            ButtonSl = Key.Unbound,
                            ButtonSr = Key.Unbound,
                        },
                        RightJoyconStick = new JoyconConfigKeyboardStick<Key>
                        {
                            StickUp = Key.I,
                            StickDown = Key.K,
                            StickLeft = Key.J,
                            StickRight = Key.L,
                            StickButton = Key.H,
                        },
                    },
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 25)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 25.");

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 26)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 26.");

                configurationFileFormat.MemoryManagerMode = MemoryManagerMode.HostMappedUnsafe;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 27)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 27.");

                configurationFileFormat.EnableMouse = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 28)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 28.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = Key.F1,
                    Screenshot = Key.F8,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 29)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 29.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = Key.F1,
                    Screenshot = Key.F8,
                    ShowUI = Key.F4,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 30)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 30.");

                foreach (InputConfig config in configurationFileFormat.InputConfig)
                {
                    if (config is StandardControllerInputConfig controllerConfig)
                    {
                        controllerConfig.Rumble = new RumbleConfigController
                        {
                            EnableRumble = false,
                            StrongRumble = 1f,
                            WeakRumble = 1f,
                        };
                    }
                }

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 31)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 31.");

                configurationFileFormat.BackendThreading = BackendThreading.Auto;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 32)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 32.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = configurationFileFormat.Hotkeys.ToggleVsync,
                    Screenshot = configurationFileFormat.Hotkeys.Screenshot,
                    ShowUI = configurationFileFormat.Hotkeys.ShowUI,
                    Pause = Key.F5,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 33)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 33.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = configurationFileFormat.Hotkeys.ToggleVsync,
                    Screenshot = configurationFileFormat.Hotkeys.Screenshot,
                    ShowUI = configurationFileFormat.Hotkeys.ShowUI,
                    Pause = configurationFileFormat.Hotkeys.Pause,
                    ToggleMute = Key.F2,
                };

                configurationFileFormat.AudioVolume = 1;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 34)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 34.");

                configurationFileFormat.EnableInternetAccess = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 35)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 35.");

                foreach (InputConfig config in configurationFileFormat.InputConfig)
                {
                    if (config is StandardControllerInputConfig controllerConfig)
                    {
                        controllerConfig.RangeLeft = 1.0f;
                        controllerConfig.RangeRight = 1.0f;
                    }
                }

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 36)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 36.");

                configurationFileFormat.LoggingEnableTrace = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 37)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 37.");

                configurationFileFormat.ShowConsole = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 38)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 38.");

                configurationFileFormat.BaseStyle = "Dark";
                configurationFileFormat.GameListViewMode = 0;
                configurationFileFormat.ShowNames = true;
                configurationFileFormat.GridSize = 2;
                configurationFileFormat.LanguageCode = "en_US";

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 39)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 39.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = configurationFileFormat.Hotkeys.ToggleVsync,
                    Screenshot = configurationFileFormat.Hotkeys.Screenshot,
                    ShowUI = configurationFileFormat.Hotkeys.ShowUI,
                    Pause = configurationFileFormat.Hotkeys.Pause,
                    ToggleMute = configurationFileFormat.Hotkeys.ToggleMute,
                    ResScaleUp = Key.Unbound,
                    ResScaleDown = Key.Unbound,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 40)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 40.");

                configurationFileFormat.GraphicsBackend = GraphicsBackend.OpenGl;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 41)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 41.");

                configurationFileFormat.Hotkeys = new KeyboardHotkeys
                {
                    ToggleVsync = configurationFileFormat.Hotkeys.ToggleVsync,
                    Screenshot = configurationFileFormat.Hotkeys.Screenshot,
                    ShowUI = configurationFileFormat.Hotkeys.ShowUI,
                    Pause = configurationFileFormat.Hotkeys.Pause,
                    ToggleMute = configurationFileFormat.Hotkeys.ToggleMute,
                    ResScaleUp = configurationFileFormat.Hotkeys.ResScaleUp,
                    ResScaleDown = configurationFileFormat.Hotkeys.ResScaleDown,
                    VolumeUp = Key.Unbound,
                    VolumeDown = Key.Unbound,
                };
            }

            if (configurationFileFormat.Version < 42)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 42.");

                configurationFileFormat.EnableMacroHLE = true;
            }

            if (configurationFileFormat.Version < 43)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 43.");

                configurationFileFormat.UseHypervisor = true;
            }

            if (configurationFileFormat.Version < 44)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 44.");

                configurationFileFormat.AntiAliasing = AntiAliasing.None;
                configurationFileFormat.ScalingFilter = ScalingFilter.Bilinear;
                configurationFileFormat.ScalingFilterLevel = 80;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 45)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 45.");

                configurationFileFormat.ShownFileTypes = new ShownFileTypes
                {
                    NSP = true,
                    PFS0 = true,
                    XCI = true,
                    NCA = true,
                    NRO = true,
                    NSO = true,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 46)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 46.");

                configurationFileFormat.MultiplayerLanInterfaceId = "0";

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 47)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 47.");

                configurationFileFormat.WindowStartup = new WindowStartup
                {
                    WindowPositionX = 0,
                    WindowPositionY = 0,
                    WindowSizeHeight = 760,
                    WindowSizeWidth = 1280,
                    WindowMaximized = false,
                };

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 48)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 48.");

                configurationFileFormat.EnableColorSpacePassthrough = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 49)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 49.");

                if (OperatingSystem.IsMacOS())
                {
                    AppDataManager.FixMacOSConfigurationFolders();
                }

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 50)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 50.");

                configurationFileFormat.EnableHardwareAcceleration = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 51)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 51.");

                configurationFileFormat.RememberWindowState = true;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 52)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 52.");

                configurationFileFormat.AutoloadDirs = [];

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 53)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 53.");

                configurationFileFormat.EnableLowPowerPtc = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 54)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 54.");

                configurationFileFormat.DramSize = MemoryConfiguration.MemoryConfiguration4GiB;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 55)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 55.");

                configurationFileFormat.IgnoreApplet = false;

                configurationFileUpdated = true;
            }

            if (configurationFileFormat.Version < 56)
            {
                Ryujinx.Common.Logging.Logger.Warning?.Print(LogClass.Application, $"Outdated configuration version {configurationFileFormat.Version}, migrating to version 56.");

                configurationFileFormat.ShowTitleBar = !OperatingSystem.IsWindows();

                configurationFileUpdated = true;
            }

            Logger.EnableFileLog.Value = configurationFileFormat.EnableFileLog;
            Graphics.ResScale.Value = configurationFileFormat.ResScale;
            Graphics.ResScaleCustom.Value = configurationFileFormat.ResScaleCustom;
            Graphics.MaxAnisotropy.Value = configurationFileFormat.MaxAnisotropy;
            Graphics.AspectRatio.Value = configurationFileFormat.AspectRatio;
            Graphics.ShadersDumpPath.Value = configurationFileFormat.GraphicsShadersDumpPath;
            Graphics.BackendThreading.Value = configurationFileFormat.BackendThreading;
            Graphics.GraphicsBackend.Value = configurationFileFormat.GraphicsBackend;
            Graphics.PreferredGpu.Value = configurationFileFormat.PreferredGpu;
            Graphics.AntiAliasing.Value = configurationFileFormat.AntiAliasing;
            Graphics.ScalingFilter.Value = configurationFileFormat.ScalingFilter;
            Graphics.ScalingFilterLevel.Value = configurationFileFormat.ScalingFilterLevel;
            Logger.EnableDebug.Value = configurationFileFormat.LoggingEnableDebug;
            Logger.EnableStub.Value = configurationFileFormat.LoggingEnableStub;
            Logger.EnableInfo.Value = configurationFileFormat.LoggingEnableInfo;
            Logger.EnableWarn.Value = configurationFileFormat.LoggingEnableWarn;
            Logger.EnableError.Value = configurationFileFormat.LoggingEnableError;
            Logger.EnableTrace.Value = configurationFileFormat.LoggingEnableTrace;
            Logger.EnableGuest.Value = configurationFileFormat.LoggingEnableGuest;
            Logger.EnableFsAccessLog.Value = configurationFileFormat.LoggingEnableFsAccessLog;
            Logger.FilteredClasses.Value = configurationFileFormat.LoggingFilteredClasses;
            Logger.GraphicsDebugLevel.Value = configurationFileFormat.LoggingGraphicsDebugLevel;
            System.Language.Value = configurationFileFormat.SystemLanguage;
            System.Region.Value = configurationFileFormat.SystemRegion;
            System.TimeZone.Value = configurationFileFormat.SystemTimeZone;
            System.SystemTimeOffset.Value = configurationFileFormat.SystemTimeOffset;
            System.EnableDockedMode.Value = configurationFileFormat.DockedMode;
            EnableDiscordIntegration.Value = configurationFileFormat.EnableDiscordIntegration;
            CheckUpdatesOnStart.Value = configurationFileFormat.CheckUpdatesOnStart;
            ShowConfirmExit.Value = configurationFileFormat.ShowConfirmExit;
            IgnoreApplet.Value = configurationFileFormat.IgnoreApplet;
            RememberWindowState.Value = configurationFileFormat.RememberWindowState;
            ShowTitleBar.Value = configurationFileFormat.ShowTitleBar;
            EnableHardwareAcceleration.Value = configurationFileFormat.EnableHardwareAcceleration;
            HideCursor.Value = configurationFileFormat.HideCursor;
            Graphics.EnableVsync.Value = configurationFileFormat.EnableVsync;
            Graphics.EnableShaderCache.Value = configurationFileFormat.EnableShaderCache;
            Graphics.EnableTextureRecompression.Value = configurationFileFormat.EnableTextureRecompression;
            Graphics.EnableMacroHLE.Value = configurationFileFormat.EnableMacroHLE;
            Graphics.EnableColorSpacePassthrough.Value = configurationFileFormat.EnableColorSpacePassthrough;
            System.EnablePtc.Value = configurationFileFormat.EnablePtc;
            System.EnableLowPowerPtc.Value = configurationFileFormat.EnableLowPowerPtc;
            System.EnableInternetAccess.Value = configurationFileFormat.EnableInternetAccess;
            System.EnableFsIntegrityChecks.Value = configurationFileFormat.EnableFsIntegrityChecks;
            System.FsGlobalAccessLogMode.Value = configurationFileFormat.FsGlobalAccessLogMode;
            System.AudioBackend.Value = configurationFileFormat.AudioBackend;
            System.AudioVolume.Value = configurationFileFormat.AudioVolume;
            System.MemoryManagerMode.Value = configurationFileFormat.MemoryManagerMode;
            System.DramSize.Value = configurationFileFormat.DramSize;
            System.IgnoreMissingServices.Value = configurationFileFormat.IgnoreMissingServices;
            System.UseHypervisor.Value = configurationFileFormat.UseHypervisor;
            UI.GuiColumns.FavColumn.Value = configurationFileFormat.GuiColumns.FavColumn;
            UI.GuiColumns.IconColumn.Value = configurationFileFormat.GuiColumns.IconColumn;
            UI.GuiColumns.AppColumn.Value = configurationFileFormat.GuiColumns.AppColumn;
            UI.GuiColumns.DevColumn.Value = configurationFileFormat.GuiColumns.DevColumn;
            UI.GuiColumns.VersionColumn.Value = configurationFileFormat.GuiColumns.VersionColumn;
            UI.GuiColumns.TimePlayedColumn.Value = configurationFileFormat.GuiColumns.TimePlayedColumn;
            UI.GuiColumns.LastPlayedColumn.Value = configurationFileFormat.GuiColumns.LastPlayedColumn;
            UI.GuiColumns.FileExtColumn.Value = configurationFileFormat.GuiColumns.FileExtColumn;
            UI.GuiColumns.FileSizeColumn.Value = configurationFileFormat.GuiColumns.FileSizeColumn;
            UI.GuiColumns.PathColumn.Value = configurationFileFormat.GuiColumns.PathColumn;
            UI.ColumnSort.SortColumnId.Value = configurationFileFormat.ColumnSort.SortColumnId;
            UI.ColumnSort.SortAscending.Value = configurationFileFormat.ColumnSort.SortAscending;
            UI.GameDirs.Value = configurationFileFormat.GameDirs;
            UI.AutoloadDirs.Value = configurationFileFormat.AutoloadDirs ?? [];
            UI.ShownFileTypes.NSP.Value = configurationFileFormat.ShownFileTypes.NSP;
            UI.ShownFileTypes.PFS0.Value = configurationFileFormat.ShownFileTypes.PFS0;
            UI.ShownFileTypes.XCI.Value = configurationFileFormat.ShownFileTypes.XCI;
            UI.ShownFileTypes.NCA.Value = configurationFileFormat.ShownFileTypes.NCA;
            UI.ShownFileTypes.NRO.Value = configurationFileFormat.ShownFileTypes.NRO;
            UI.ShownFileTypes.NSO.Value = configurationFileFormat.ShownFileTypes.NSO;
            UI.LanguageCode.Value = configurationFileFormat.LanguageCode;
            UI.BaseStyle.Value = configurationFileFormat.BaseStyle;
            UI.GameListViewMode.Value = configurationFileFormat.GameListViewMode;
            UI.ShowNames.Value = configurationFileFormat.ShowNames;
            UI.IsAscendingOrder.Value = configurationFileFormat.IsAscendingOrder;
            UI.GridSize.Value = configurationFileFormat.GridSize;
            UI.ApplicationSort.Value = configurationFileFormat.ApplicationSort;
            UI.StartFullscreen.Value = configurationFileFormat.StartFullscreen;
            UI.ShowConsole.Value = configurationFileFormat.ShowConsole;
            UI.WindowStartup.WindowSizeWidth.Value = configurationFileFormat.WindowStartup.WindowSizeWidth;
            UI.WindowStartup.WindowSizeHeight.Value = configurationFileFormat.WindowStartup.WindowSizeHeight;
            UI.WindowStartup.WindowPositionX.Value = configurationFileFormat.WindowStartup.WindowPositionX;
            UI.WindowStartup.WindowPositionY.Value = configurationFileFormat.WindowStartup.WindowPositionY;
            UI.WindowStartup.WindowMaximized.Value = configurationFileFormat.WindowStartup.WindowMaximized;
            Hid.EnableKeyboard.Value = configurationFileFormat.EnableKeyboard;
            Hid.EnableMouse.Value = configurationFileFormat.EnableMouse;
            Hid.Hotkeys.Value = configurationFileFormat.Hotkeys;
            Hid.InputConfig.Value = configurationFileFormat.InputConfig ?? [];

            Multiplayer.LanInterfaceId.Value = configurationFileFormat.MultiplayerLanInterfaceId;
            Multiplayer.Mode.Value = configurationFileFormat.MultiplayerMode;

            if (configurationFileUpdated)
            {
                ToFileFormat().SaveConfig(configurationFilePath);

                Ryujinx.Common.Logging.Logger.Notice.Print(LogClass.Application, $"Configuration file updated to version {ConfigurationFileFormat.CurrentVersion}");
            }
        }

        private static GraphicsBackend DefaultGraphicsBackend()
        {
            // Any system running macOS or returning any amount of valid Vulkan devices should default to Vulkan.
            // Checks for if the Vulkan version and featureset is compatible should be performed within VulkanRenderer.
            if (OperatingSystem.IsMacOS() || VulkanRenderer.GetPhysicalDevices().Length > 0)
            {
                return GraphicsBackend.Vulkan;
            }

            return GraphicsBackend.OpenGl;
        }

        public static void Initialize()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Configuration is already initialized");
            }

            Instance = new ConfigurationState();

            Instance.System.EnableLowPowerPtc.Event += (_, evnt) => Optimizations.LowPower = evnt.NewValue;
        }
    }
}
