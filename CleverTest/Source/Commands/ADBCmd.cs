using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverTest.Source.Commands
{
    class ADBCmd
    {
        public static readonly string Install = "adb install {0}";
        public static readonly string Uninstall = "adb uninstall {0}";
        public static readonly string Push = "adb push {0} {1}"; //adb push 文件名 手机端SDCard路径
        public static readonly string StartActivity = "adb shell am start {0}/{1}"; //adb shell am start 包名/完整Activity路径


        //设备信息
        public static readonly string Brand = "adb shell getprop ro.product.brand";
        public static readonly string Model = "adb shell getprop ro.product.model";
        public static readonly string Name = "adb shell getprop ro.product.name";
        public static readonly string Sdk = "adb shell getprop ro.build.version.sdk";
        public static readonly string Release = "adb shell getprop ro.build.version.release";

        //For Test
        //adb push D:/android/workspace/git-CleverTest/CleverTest/CleverTest/bin/Debug/Resource/scrcpy-server /data/local/tmp/scrcpy-server.jar
        public static readonly int ServerPort = 27183;
        public static readonly string SendServer = "adb push {0}Resource/scrcpy-server /data/local/tmp/scrcpy-server.jar";
        public static readonly string Forward = "adb forward tcp:27183 localabstract:scrcpy";
        public static readonly string StartServer = "adb shell CLASSPATH=/data/local/tmp/scrcpy-server.jar app_process / com.genymobile.scrcpy.Server 1.14 info 0 12441600 20 -1 true 1080:1920:0:0 false true 0 false false -";
    }
}
